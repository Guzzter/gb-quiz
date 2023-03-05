using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GB.QuizAPI.Extensions;
using GB.QuizAPI.Model;
using GB.QuizAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GB.QuizAPI
{
    public class QuizFunctions
    {
        private readonly ILogger<QuizFunctions> _logger;
        private readonly List<IQuiz> _quizRepos;

        public QuizFunctions(ILogger<QuizFunctions> log)
        {
            _logger = log;
            _quizRepos = new List<IQuiz>()
            {
               new SitecoreContentHubDeveloperPrep(),
               new DutchQuiz()
            };
        }

        [FunctionName("GetQuizList")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetQuizList(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("GetQuizList");

            return new OkObjectResult(_quizRepos.ToDictionary(q => q.GetId(), q => q.GetName()));
        }

        [FunctionName("GetQuiz")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter("quizid", Description = "Quiz ID", Required = true, In = ParameterLocation.Query, Type = typeof(string))]
        [OpenApiParameter("questions", Description = "Amount of questions", Required = false, In = ParameterLocation.Query, Type = typeof(int))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetQuiz(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("GetQuiz");
            int questions;
            if (!int.TryParse(req.Query["questions"], out questions))
            {
                questions = 9999;
            }

            var quizId = req.Query["quizid"];
            var quiz = _quizRepos.FirstOrDefault(q => q.GetId().Equals(quizId));
            if (quiz == null)
            {
                return new BadRequestObjectResult(new
                {
                    Field = "id",
                    Error = "Could not find quiz with id=" + quizId
                });
            }

            return new OkObjectResult(quiz.GetQuiz(questions));
        }

        [FunctionName("GetRandomQuestion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter("quizid", Description = "Quiz ID", Required = true, In = ParameterLocation.Query, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetRandomQuestion(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("GetRandomQuestion");

            var quizId = req.Query["quizid"];
            var quiz = _quizRepos.FirstOrDefault(q => q.GetId().Equals(quizId));
            if (quiz == null)
            {
                return new BadRequestObjectResult(new
                {
                    Field = "id",
                    Error = "Could not find quiz with id=" + quizId
                });
            }

            return new OkObjectResult(quiz.GetRandomQuestion());
        }

        [FunctionName("ValidateAnswer")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserAnswer), Description = "Answer given by user", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ValidateAnswer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("ValidateAnswer");

            var userAnswerReq = await req.GetJsonBody<UserAnswer, UserAnswerRequestValidator>();
            if (!userAnswerReq.IsValid)
            {
                var errors = string.Empty;
                if (userAnswerReq.Errors?.Any() == true)
                    errors = string.Join(',', userAnswerReq.Errors);

                _logger.LogError($"Invalid batch request. Validation failed. Errors: {errors}");
                return userAnswerReq.ToBadRequest();
            }

            var quiz = _quizRepos.FirstOrDefault(q => q.GetId().Equals(userAnswerReq.Value.QuizId));
            if (quiz == null)
            {
                return new BadRequestObjectResult(new
                {
                    Field = "id",
                    Error = "Could not find quiz with id=" + userAnswerReq.Value.QuizId
                });
            }

            return new OkObjectResult(quiz.ValidateAnswer(userAnswerReq.Value));
        }
    }
}