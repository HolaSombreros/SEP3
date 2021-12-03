using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IFAQClient {
        Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync();
    }
}