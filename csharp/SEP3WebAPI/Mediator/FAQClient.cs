﻿using System.Collections.Generic;
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
                Service = "faq"
            };
            return ((FAQMessage) client.Send(req)).FAQs;
        }
        
        public async Task<FAQ> GetFrequentlyAskedQuestionAsync(int id) {
            Message req = new FAQMessage() {
                Type = "get",
                Service = "faq",
                Id = id
            };
            return ((FAQMessage) client.Send(req)).FAQ;
        }
        
        public async Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq) {
            Message req = new FAQMessage() {
                Type = "add",
                Service = "faq",
                FAQ = faq
            };
            return ((FAQMessage) client.Send(req)).FAQ;
        }
        
        public async Task DeleteFrequentlyAskedQuestionAsync(int id) {
            Message req = new FAQMessage() {
                Type = "delete",
                Service = "faq",
                Id = id
            };
            client.Send(req);
        }
    }
}