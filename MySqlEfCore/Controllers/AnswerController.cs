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

        [HttpGet("/api/answer")]
        public ActionResult<AnswerResponseInfo> PostAnswer([FromQuery(Name = "id")] string gameIdString, // [FromBody] AnswerInfo guess
                                                           [FromQuery(Name = "answer")] string answerString )
        {
            try
            {
                AnswerResponseInfo responseInfo = new AnswerResponseInfo();

                // Get the status of this game
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                bool moveToNext = false;

                // If the player has passed, skip to the results
                if (answerString == "Pass")
                {
                    moveToNext = true;
                    responseInfo.Result = "passed";
                }
                else
                {
                    QuizGameQuestion gameQuestion = (from gq in _context.QuizGameQuestions
                                                     where gq.QuizGameId == Guid.Parse(gameIdString)
                                                           && gq.QuestionPosition == game.CurrentQuestionPosition
                                                     select gq).First();

                    Question question = (from q in _context.Questions
                                         where q.QuestionId == gameQuestion.QuestionId
                                         select q).First();

                    // Check for correct location
                    bool locationOK = true;
                    if (question.UseLatLong)
                    {
                        // casting parameters because question lat/long could theoretically be null
                        double distanceFromTarget = Distance(game.PlayerLatitude, game.PlayerLongitude,
                                                             (double)question.QuestionLatitude, (double)question.QuestionLongitude);

                        int radius = (from cv in _context.Controls
                                      select cv.GPSRadius).First();

                        locationOK = (distanceFromTarget <= radius);
                    }

                    // Set up response and give points for correct answer
                    if ((question.CorrectAnswer == answerString) && locationOK)
                    {
                        game.Score += 3;

                        //Apply penalties for using hints after giving points
                        if (gameQuestion.LocationHintUsed)
                            game.Score--;
                        if (gameQuestion.QuestionHintUsed)
                            game.Score--;

                        moveToNext = true;
                        responseInfo.Result = "correct";
                    }
                    else
                    {
                        responseInfo.Result = "incorrect";
                    }

                } // end if not passed.

                // move on if the answer was correct (or passed)
                if (moveToNext)
                    game.CurrentQuestionPosition++;

                _context.SaveChanges();

                return Ok(responseInfo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            // returns the distance in metres between two pairs of lat/lon coordinates.

            // this code uses Pythagoras' theorem on an equirectangular projection of the Earth
            // accurate enough over small distances, and relatively efficient

            double x = (Deg2rad(lon2) - Deg2rad(lon1)) * Math.Cos((Deg2rad(lat1) + Deg2rad(lat2)) / 2);
            double y = (Deg2rad(lat2) - Deg2rad(lat1));
            double dist = Math.Sqrt(x * x + y * y) * 6371; // approx. avg. radius of earth = 6371km
            return (dist * 1000); // convert from km to m before returning
        }

        private static double Deg2rad(double deg)
        {
            // converts angles in degrees to radians

            return (deg * Math.PI / 180.0);
        }

    }
}
