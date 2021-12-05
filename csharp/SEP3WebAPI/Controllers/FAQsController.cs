using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Models;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class FAQsController : ControllerBase {
        private IRestService service;

        public FAQsController(IRestService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<FAQ>>> GetFrequentlyAskedQuestionsAsync() {
            try {
                IList<FAQ> faqs = await service.GetFrequentlyAskedQuestionsAsync();
                return Ok(faqs);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}