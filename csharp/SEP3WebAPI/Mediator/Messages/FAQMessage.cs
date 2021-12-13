using System.Collections.Generic;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator.Messages {
    public class FAQMessage : Message {
        public IList<FAQ> FAQs { get; set; }
        public int Id { get; set; }
        public FAQ FAQ { get; set; }

        public FAQMessage() : base("faq") {
        }
    }
}