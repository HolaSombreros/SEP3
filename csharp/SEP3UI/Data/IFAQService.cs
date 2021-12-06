using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3UI.Data {
    public interface IFAQService {
        Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync();
        Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq);
        Task DeleteFrequentlyAskedQuestionAsync(int id);
    }
}