using GB.QuizAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GB.QuizAPI.Repository
{
    public static class EnumerableHelper<E>
    {
        private static Random r;

        static EnumerableHelper()
        {
            r = new Random();
        }

        public static T Random<T>(IEnumerable<T> input)
        {
            return input.ElementAt(r.Next(input.Count()));
        }
    }

    public static class EnumerableExtensions
    {
        private static Random r = new Random();

        public static T Random<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<Question> RemoveAnswers(this IEnumerable<Question> input)
        {
            input.ToList().ForEach(q => q.AnswerOptions.ForEach(a => a.IsCorrect = false));
            return input;
        }
    }
}