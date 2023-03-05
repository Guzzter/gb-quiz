using System.Collections.Generic;

namespace GB.QuizAPI.Model;

public class Question
{
    public string Text { get; set; }
    public List<AnswerOption> AnswerOptions { get; set; }
    public string Id { get; internal set; }
}