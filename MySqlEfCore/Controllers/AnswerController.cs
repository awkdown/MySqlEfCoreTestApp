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

    public class AnswerController : Controller
    {
        private readonly MyDbContext _context;

        public AnswerController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpPost("/api/answer")]
        public ActionResult<AnswerResponseInfo> PostAnswer([FromQuery(Name = "id")] string gameIdString, [FromBody] AnswerInfo guess)
        {
            try
            {
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                bool isCorrect = true;

                if (guess.Answer != "Pass")
                {
                    QuizGameQuestion quizQuestion = (from qq in _context.QuizGameQuestions
                                                     where qq.QuizGameId == Guid.Parse(gameIdString)
                                                           && qq.QuestionPosition == game.CurrentQuestionPosition
                                                     select qq).First();

                    string correctAnswer = (from q in _context.Questions
                                            where q.QuestionId == quizQuestion.QuestionId
                                            select q.CorrectAnswer).First();

                    isCorrect = (correctAnswer == guess.Answer);

                    // don't give points if the player has passed
                    if (isCorrect)
                        game.Score += 2;
                }

                AnswerResponseInfo responseInfo = new AnswerResponseInfo();
                responseInfo.Result = isCorrect ? "correct" : "incorrect";
                if (guess.Answer == "Pass")
                    responseInfo.Result = "passed";

                if (isCorrect)
                {
                    // move on to the next question
                    game.CurrentQuestionPosition++;
                    _context.SaveChanges();
                }
                return Ok(responseInfo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }

}
