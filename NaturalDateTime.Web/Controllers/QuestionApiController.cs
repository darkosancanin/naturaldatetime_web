using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.DataAccess;
using System.Diagnostics;
using NaturalDateTime.Web.Services;

namespace NaturalDateTime.Web.Controllers
{
    public class QuestionApiController : ApiController
    {
        public IHttpActionResult Get(string q)
        {
            var userAgent = String.Empty;
            if(Request.Headers.UserAgent != null)
                userAgent = Request.Headers.UserAgent.ToString();
            var questionService = new QuestionService();
            var answer = questionService.GetAnswer(q, userAgent);
            var answerModel = new AnswerModel(answer);
            return Ok(new AnswerModel(answer));
        }
    }
}