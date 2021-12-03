using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3UI.Data {
    public class FAQService : IFAQService {
        private readonly IRestService restService;
        
        public FAQService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync() {
            return await restService.GetAsync<IList<FAQ>>("faqs");
        }

        public async Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq) {
            return await restService.PostAsync<FAQ, FAQ>(faq, "faqs");
        }

        public async Task DeleteFrequentlyAskedQuestionAsync(int id) {
            await restService.DeleteAsync($"faqs/{id}");
        }
    }
}