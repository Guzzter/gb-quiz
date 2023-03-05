using GB.QuizAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GB.QuizAPI.Repository;

public class SitecoreContentHubDeveloperPrep : BaseQuizRepo, IQuiz
{
    public string GetId()
    {
        return "64d3f786-8ce5-4860-b8ee-ce531f4756ae";
    }

    public override string GetName()
    {
        return "Sitecore Content Hub Developer Prep";
    }

    public override List<Question> GetAllQuestions()
    {
        return new List<Question>() {
            new Question() {
                Id = "0e263194-be3f-4cc3-a400-c4a4fccf3d72",
                Text = "Which entity definition is used to store the metadata obtained from a media file added to the DAM?",
                AnswerOptions = new List<AnswerOption>()
                {
                    new AnswerOption() { Id = 1, Text = "M.Asset", IsCorrect = true },
                    new AnswerOption() { Id = 2, Text = "M.File", },
                    new AnswerOption() { Id = 3, Text = "M.AssetType", },
                    new AnswerOption() { Id = 4, Text = "None of these", }
                }
            },
            new Question() {
                Id = "705cf99f-ded1-41de-8f2f-0ae0226fd768",
                Text = "What is the status of assets that are in the Trash?",
                AnswerOptions = new List<AnswerOption>()
                {
                    new AnswerOption() { Id = 1, Text = "Deleted", IsCorrect = true },
                    new AnswerOption() { Id = 2, Text = "Archived", },
                    new AnswerOption() { Id = 3, Text = "Rejected", },
                    new AnswerOption() { Id = 4, Text = "None of these", }
                }
            },
            new Question() {
                Id = "3a620c53-d31a-4bf1-a535-469354d7f80e",
                Text = "Which search type is ideal for filtering entities based on categories located in related taxonomies or option lists (entity data)?",
                AnswerOptions = new List<AnswerOption>()
                {
                    new AnswerOption() { Id = 1, Text = "Faceted search filters", IsCorrect = true },
                    new AnswerOption() { Id = 2, Text = "Advanced search", },
                    new AnswerOption() { Id = 3, Text = "Full text search", },
                    new AnswerOption() { Id = 4, Text = "Saved Search", }
                }
            },
            new Question() {
                Id = "7e62be82-b47d-4a83-baa9-7fe386ad7a10",
                Text = "Which search type is best suited locating a string of text within entity data that has been indexed?",
                AnswerOptions = new List<AnswerOption>()
                {
                    new AnswerOption() { Id = 1, Text = "Full text search", IsCorrect = true },
                    new AnswerOption() { Id = 2, Text = "Faceted search filter", },
                    new AnswerOption() { Id = 3, Text = "Advanced search filter", },
                    new AnswerOption() { Id = 4, Text = "Saved Selection", }
                }
            }
        };
    }
}