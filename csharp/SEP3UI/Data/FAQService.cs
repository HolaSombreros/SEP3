using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3UI.Data {
    public class FAQService : IFAQService {
        private readonly IRestService restService;
        
        public FAQService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<IList<FAQ>> GetFrequentlyAskedQuestions() {
            return await restService.GetAsync<IList<FAQ>>("faqs");
        }
    }
}