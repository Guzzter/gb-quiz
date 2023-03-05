using System.Collections.Generic;

namespace GB.QuizAPI.Model;

public class UserAnswer
{
    public string QuizId { get; set; }
    public string QuestionId { get; set; }
    public List<int> Answers { get; set; }
}