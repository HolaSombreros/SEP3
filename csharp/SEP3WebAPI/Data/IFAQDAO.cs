using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Data {
    public interface IFAQDAO {
        Task<IList<FAQ>> GetFrequentlyAskedQuestions();
    }
}