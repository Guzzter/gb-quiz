using FluentValidation;
using GB.QuizAPI.Model;

namespace GB.QuizAPI;

public class UserAnswerRequestValidator : AbstractValidator<UserAnswer>
{
    /// <summary>
    /// Validates POST body
    /// </summary>
    /// <see href="https://docs.fluentvalidation.net/en/latest/start.html">Fluent Validation Docs</see>
    public UserAnswerRequestValidator()
    {
        RuleFor(x => x.QuestionId).NotNull().Length(36);
        RuleFor(x => x.Answers).NotEmpty().ForEach(x => x.InclusiveBetween(1, 10));
    }
}