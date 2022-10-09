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
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private readonly MyDbContext _context;

        public GameController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpPost("/api/game")]
        public ActionResult<Guid> AddGame([FromBody] NewQuizInfo credentials)
        {
            try
            {
                // make a new quiz for this player to play
                //////////////////////////////////////////////
                QuizGame quiz = new QuizGame();

                // app id, player name and quiz length come from the player supplied object
                quiz.PlayerName = credentials.PlayerName;
                quiz.AppId = credentials.AppID;

                // Should we check that this length is really valid???
                //
                //  *** YESSS!!!!! *** DO IT
                //
                quiz.QuizGameLength = credentials.QuizLength;

                quiz.CurrentQuestionPosition = 0;
                quiz.Score = 0;
                quiz.PlayerLatitude  = 63.445012; // The centre of Hell!
                quiz.PlayerLongitude = 10.905281; //

                // find category ID from category name
                quiz.CategoryId =
                    (from c in _context.Categories
                     where credentials.CategoryName == c.CategoryName
                     select c.CategoryId).FirstOrDefault();

                _context.QuizGames.Add(quiz);
                _context.SaveChanges();

                // build the quiz question set
                ///////////////////////////////////////

                // grab a random list of questions
                Random rnd = new Random();
                List<Question> questionList =
                    (from q in _context.Questions
                     where q.CategoryId == quiz.CategoryId
                     //orderby rnd.Next()
                     select q).Take(quiz.QuizGameLength).ToList();

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
