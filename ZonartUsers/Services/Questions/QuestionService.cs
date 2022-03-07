using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZonartUsers.Data;
using ZonartUsers.Models.Questions;
using ZonartUsers.Models.Users;

namespace ZonartUsers.Services.Questions
{
    using static WebConstants.Cache;
    public class QuestionService : IQuestionService
    {
        private readonly ZonartUsersDbContext data;
        private readonly IMemoryCache cache;

        public QuestionService(ZonartUsersDbContext data, IMemoryCache cache)
        {
            this.cache = cache;
            this.data = data;
        }

        public void Add(string text, string answer)
        {
            this.data.Questions.Add(new Data.Models.Question { Text = text, Answer = answer });
            this.data.SaveChanges();
        }

        public bool Delete(int questionId)
        {
            var questionData = this.data.Questions
                .FirstOrDefault(t => t.Id == questionId);

            if (questionData == null)
            {
                return false;
            }

            this.data.Questions.Remove(questionData);
            this.data.SaveChanges();

            return true;
        }

        public bool Edit(int questionId, string text, string answer)
        {
            var questionData = this.data.Questions
                .FirstOrDefault(t => t.Id == questionId);

            if (questionData == null)
            {
                return false;
            }

            questionData.Text = text;
            questionData.Answer = answer;

            this.data.SaveChanges();

            return true;
        }

        public List<QuestionsListingViewModel> GetLatestQuestions()
        {
            var latestQuestions = this.cache.Get<List<QuestionsListingViewModel>>(QuestionsCacheKey);

            if (latestQuestions == null)
            {
                latestQuestions = this.data.Questions
                    .Select(q => new QuestionsListingViewModel
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Answer = q.Answer
                    })
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

                this.cache.Set(QuestionsCacheKey, latestQuestions, cacheOptions);
            }

            return latestQuestions;
        }

        public EditQuestionModel GetQuestionById(int id)
        {
            return this.data.Questions
                .Where(q => q.Id == id)
                .Select(q => new EditQuestionModel
                {
                    Id = id,
                    Text = q.Text,
                    Answer = q.Answer
                })
                .FirstOrDefault();
        }
    }
}
