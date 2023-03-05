using GB.QuizAPI.Model;
using System.Collections.Generic;

namespace GB.QuizAPI.Repository
{
    public interface IQuiz
    {
        public string GetId();

        public string GetName();

        public List<Question> GetAllQuestions();

        public Quiz GetQuiz(int questionCount);

        public Question GetRandomQuestion();

        public ValidateResult ValidateAnswer(UserAnswer answer);
    }
}