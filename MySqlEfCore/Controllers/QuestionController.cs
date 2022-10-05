using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MySqlEfCore.Data;
using MySqlEfCore.Models;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MySqlEfCore.Controllers
{
    [ApiController]
    [Route("[controller]")] //default uri is /category

    public class QuestionController : Controller
    {
        private readonly MyDbContext _context;

        public QuestionController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/question")]
        public ActionResult<QuestionInfo> GetCurrentQuestion([FromQuery(Name = "id")]string gameIdString)
        {
            try
            {
                Guid gameId = Guid.Parse(gameIdString);

                QuizGame game =
                    (from g in _context.QuizGames
                     where g.QuizGameId == gameId
                     select g).First();

                if(game == null)
                {
                    throw new ArgumentException("Id does not exist");
                }

                int qNum = game.CurrentQuestionPosition;

                if(game.CurrentQuestionPosition >= game.QuizGameLength)
                {
                    throw new InvalidOperationException("End of quiz");
                }
                QuizGameQuestion gameQuestion =
                    (from gq in _context.QuizGameQuestions
                     where gq.QuizGameId == game.QuizGameId
                        && gq.QuestionPosition == game.CurrentQuestionPosition
                     select gq).First();

                Question question =
                    (from q in _context.Questions
                     where q.QuestionId == gameQuestion.QuestionId
                     select q).First();

                QuestionInfo result = new QuestionInfo();
                result.QuestionType = question.QuestionType;
                result.QuestionText = question.QuestionText;
                result.AnswerA = question.AnswerA;
                result.AnswerB = question.AnswerB;
                result.AnswerC = question.AnswerC;
                result.AnswerD = question.AnswerD;

                return Ok(result);
            }

            catch (Exception ex) when (ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
