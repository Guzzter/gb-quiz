using GB.QuizAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace GB.QuizAPI.Repository;

public abstract class BaseQuizRepo
{
    public abstract List<Question> GetAllQuestions();

    public Question GetRandomQuestion()
    {
        var questions = GetAllQuestions();
        questions.ForEach(q => q.AnswerOptions.Shuffle());
        questions.RemoveAnswers();
        return questions.Random();
    }

    public ValidateResult ValidateAnswer(UserAnswer answer)
    {
        var questions = GetAllQuestions();
        var question = questions.SingleOrDefault(q => q.Id == answer.QuestionId);
        if (question == null)
        {
            return new ValidateResult()
            {
                Text = "Could not find question",
                Score = 0
            };
        }

        var goodPossibleAnswers = question.AnswerOptions.Where(a => a.IsCorrect.HasValue && a.IsCorrect.Value == true).Select(a => a.Text);
        var goodGivenAnswers = question.AnswerOptions.Where(a => answer.Answers.Contains(a.Id) && a.IsCorrect.HasValue && a.IsCorrect.Value == true);
        var wrongGivenAnswers = question.AnswerOptions.Where(a => answer.Answers.Contains(a.Id) && (!a.IsCorrect.HasValue || a.IsCorrect.Value == false));

        if (wrongGivenAnswers.Count() == 0 && goodPossibleAnswers.Count() == goodGivenAnswers.Count())
        {
            return new ValidateResult()
            {
                Text = "Correct.",
                Score = 100
            };
        }

        if (goodGivenAnswers.Count() > 0)
        {
            return new ValidateResult()
            {
                Text = $"Partially correct. {goodGivenAnswers.Count()} good answer(s): {string.Join(", ", goodGivenAnswers.Select(a => $"'{a.Text}'"))}. {wrongGivenAnswers.Count()} wrong answer(s): {string.Join(", ", wrongGivenAnswers.Select(a => $"'{a.Text}'"))}. ",
                Score = 100 * goodPossibleAnswers.Count() / answer.Answers.Count()
            };
        }

        return new ValidateResult()
        {
            Text = $"Incorrect. {goodPossibleAnswers.Count()} good answer(s): {string.Join(", ", goodPossibleAnswers.Select(x => $"'{x}'"))}",
            Score = 0,
        };
    }
}