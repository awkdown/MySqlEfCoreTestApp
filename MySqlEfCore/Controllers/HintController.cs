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
    [Route("[controller]")] //default uri is /position

    public class HintController : Controller
    {
        private readonly MyDbContext _context;

        public HintController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/texthint")]
        public ActionResult<AnswerResponseInfo> GetTextHint([FromQuery(Name = "id")] string gameIdString)
        {
            try
            {
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                if (game == null)
                {
                    throw new ArgumentException("Id does not exist");
                }

                int qNum = game.CurrentQuestionPosition;

                if (game.CurrentQuestionPosition >= game.QuizGameLength)
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

                gameQuestion.QuestionHintUsed = true;
                _context.SaveChanges();

                AnswerResponseInfo result = new AnswerResponseInfo();
                result.Result = question.Hint;

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

        [HttpGet("/api/positionhint")]
        public ActionResult<PositionInfo> GetPositionHint([FromQuery(Name = "id")] string gameIdString)
        {
            try
            {
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                if (game == null)
                {
                    throw new ArgumentException("Id does not exist");
                }

                int qNum = game.CurrentQuestionPosition;

                if (game.CurrentQuestionPosition >= game.QuizGameLength)
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

                gameQuestion.LocationHintUsed = true;
                _context.SaveChanges();

                PositionInfo result = new PositionInfo();
                result.Latitude = (double)question.QuestionLatitude; // cast in case null
                result.Longitude = (double)question.QuestionLongitude; // cast in case null

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
