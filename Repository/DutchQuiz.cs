using GB.QuizAPI.Model;
using System;
using System.Collections.Generic;

namespace GB.QuizAPI.Repository;

internal class DutchQuiz : BaseQuizRepo, IQuiz
{
    public override List<Question> GetAllQuestions()
    {
        return new List<Question>() {
            new Question() {
                Id = "0e263194-be3f-4cc3-a400-c4a4fccf3d72",
                Text = "Which city used to be part of the Netherlands",
                AnswerOptions = new List<AnswerOption>()
                {
                    new AnswerOption() { Id = 1, Text = "Paris" },
                    new AnswerOption() { Id = 2, Text = "New York", IsCorrect = true },
                    new AnswerOption() { Id = 3, Text = "Berlin", },
                    new AnswerOption() { Id = 4, Text = "Cologne", }
                }
            }
        };
    }

    public string GetId()
    {
        return "e84a7152-95d4-42f2-9bcb-ced509c31c35";
    }

    public override string GetName()
    {
        return "Dutch Fun Facts";
    }
}