using System.Collections.Generic;

namespace GB.QuizAPI.Model;

public class Quiz
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int QuestionsSelectedCount { get; set; }
    public int QuestionsAvailableCount { get; set; }
    public bool RandomQuestions { get; set; }
    public List<Question> Questions { get; set; }
}