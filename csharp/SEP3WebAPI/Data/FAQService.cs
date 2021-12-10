using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class FAQService :IFAQService{
        
        private IFAQClient client;

        public FAQService(IFAQClient client) {
            this.client = client;
        }
        
        public async Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync() {
            return await client.GetFrequentlyAskedQuestionsAsync();
        }

        public async Task<FAQ> GetFrequentlyAskedQuestionAsync(int id) {
            return await client.GetFrequentlyAskedQuestionAsync(id);
        }

        public async Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq) {
            return await client.AddFrequentlyAskedQuestionAsync(faq);
        }

        public async Task DeleteFrequentlyAskedQuestionAsync(int id) {
            await client.DeleteFrequentlyAskedQuestionAsync(id);
        }
    }
}