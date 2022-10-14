using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MySqlEfCore.Data;
using MySqlEfCore.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MySqlEfCore.Controllers
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private readonly MyDbContext _context;

        public GameController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/game")]
        public ActionResult<Guid> AddGame([FromQuery(Name = "playerName")]   string playerString,
                                          [FromQuery(Name = "categoryName")] string categoryString,
                                          [FromQuery(Name = "appID")]        string appString,
                                          [FromQuery(Name = "quizLength")]   string lengthString)//[FromBody] NewQuizInfo credentials)
        {
            try
            {
                // make a new quiz for this player to play
                //////////////////////////////////////////////
                QuizGame quiz = new QuizGame();

                // app id, player name and quiz length come from the player supplied object
                quiz.PlayerName = playerString; //credentials.PlayerName;
                quiz.AppId = appString; // credentials.AppID;

                // Should we check that this length is really valid???
                //
                //  *** YESSS!!!!! *** DO IT
                //
                quiz.QuizGameLength = int.Parse(lengthString); // credentials.QuizLength;

                quiz.CurrentQuestionPosition = 0;
                quiz.Score = 0;
                quiz.PlayerLatitude  = 63.445012; // The centre of Hell!
                quiz.PlayerLongitude = 10.905281; //

                // find category ID from category name
                quiz.CategoryId =
                    (from c in _context.Categories
                     where c.CategoryName == categoryString // credentials.CategoryName
                     select c.CategoryId).FirstOrDefault();

                _context.QuizGames.Add(quiz);
                _context.SaveChanges();

                // build the quiz question set
                ///////////////////////////////////////

                // grab *all* the questions from the category
                List<Question> allQuestionsList =
                (from q in _context.Questions
                 where q.CategoryId == quiz.CategoryId
                 select q).ToList();

                // special case for TH - look for one ending "finally" and set flag.
                int questionCount = allQuestionsList.Count;
                int LastQuestionIndex = allQuestionsList.FindIndex(p => p.QuestionText.StartsWith("Finally"));
                bool specialLastQuestion = (LastQuestionIndex != -1);

                // swap the last question to the back end of the list
                if(specialLastQuestion)
                    allQuestionsList.Swap(LastQuestionIndex, questionCount - 1);

                // shuffle the list up to but not including the special last question (if there is one)
                // otherwise shuffle the whole list
                Random rnd = new Random();
                int shuffleLimit = specialLastQuestion ? questionCount - 1 : questionCount - 2;
                int shuffleSwaps = rnd.Next(questionCount);

                for(int shuffle = 0; shuffle < shuffleSwaps; shuffle++)
                {
                    allQuestionsList.Swap(rnd.Next(shuffleLimit), rnd.Next(shuffleLimit));
                }

                // take the required number of questions from the shuffled list
                List<Question> questionList = allQuestionsList.GetRange(0, quiz.QuizGameLength);

                // replace the lst slected question with the special one if necessary
                if (specialLastQuestion)
                    questionList[quiz.QuizGameLength - 1] = allQuestionsList[questionCount - 1];

                // build the game question objects in selected order
                int questionIdx = 0;
                List<QuizGameQuestion> questionSet = new List<QuizGameQuestion>();
                foreach(Question q in questionList)
                {
                    QuizGameQuestion question = new QuizGameQuestion();
                    question.QuestionPosition = questionIdx;
                    question.QuizGameId = quiz.QuizGameId;
                    question.QuestionId = q.QuestionId;
                    questionIdx++;
                    questionSet.Add(question);
                }

                _context.QuizGameQuestions.AddRange(questionSet);
                _context.SaveChanges();

                //credentials.
                //_context.QuizGames.Add(credentials);
                //_context.SaveChanges();
                return Ok(quiz.QuizGameId);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
