using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3UI.Data {
    public interface IFAQService {
        Task<IList<FAQ>> GetFrequentlyAskedQuestions();
    }
}