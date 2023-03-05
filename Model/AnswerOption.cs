namespace GB.QuizAPI.Model;

public class AnswerOption
{
    public int Id { get; internal set; }
    public string Text { get; internal set; }
    public bool? IsCorrect { get; internal set; }
}