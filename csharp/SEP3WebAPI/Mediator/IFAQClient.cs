﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IFAQClient {
        Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync();
        Task<FAQ> GetFrequentlyAskedQuestionAsync(int id);
        Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq);
        Task DeleteFrequentlyAskedQuestionAsync(int id);
    }
}