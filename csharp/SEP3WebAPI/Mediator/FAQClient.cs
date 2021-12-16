using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class FAQClient : IFAQClient {

        private IClient client;

        public FAQClient(IClient client) {
            this.client = client;
        }
        
        public async Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync() {
            Message req = new FAQMessage() {
                Type = "getAll",
            };
            return ((FAQMessage) client.Send(req)).Faqs;
        }
        
        public async Task<FAQ> GetFrequentlyAskedQuestionAsync(int id) {
            Message req = new FAQMessage() {
                Type = "get",
                Faqs = new List<FAQ> {
                    new() {
                    Id = id
                    }
                }
            };
            return ((FAQMessage) client.Send(req)).Faqs[0];
        }
        
        public async Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq) {
            Message req = new FAQMessage() {
                Type = "add",
                Faqs = new List<FAQ> {faq}
            };
            return ((FAQMessage) client.Send(req)).Faqs[0];
        }
        
        public async Task DeleteFrequentlyAskedQuestionAsync(int id) {
            Message req = new FAQMessage() {
                Type = "delete",
                Faqs = new List<FAQ> {
                    new() {
                        Id = id
                    }
                }
            };
            client.Send(req);
        }
    }
}