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
        private SitecoreContentHubDeveloperExam _quizRepo = new SitecoreContentHubDeveloperExam();

        public QuizFunctions(ILogger<QuizFunctions> log)
        {
            _logger = log;
        }

        [FunctionName("GetRandomQuestion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetRandomQuestion(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("GetRandomQuestion");

            return new OkObjectResult(_quizRepo.GetRandomQuestion());
        }

        [FunctionName("ValidateAnswer")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "answer" })]
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

            return new OkObjectResult(_quizRepo.ValidateAnswer(userAnswerReq.Value));
        }
    }
}