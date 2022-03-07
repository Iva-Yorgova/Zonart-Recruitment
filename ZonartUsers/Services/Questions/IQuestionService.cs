

using System.Collections.Generic;
using ZonartUsers.Models.Questions;
using ZonartUsers.Models.Users;

namespace ZonartUsers.Services.Questions
{
    public interface IQuestionService
    {
        bool Edit(
            int questionId,
            string text,
            string answer);

        void Add(
            string text,
            string answer);

        bool Delete(
            int questionId);

        List<QuestionsListingViewModel> GetLatestQuestions();

        EditQuestionModel GetQuestionById(int id);
    }
}
