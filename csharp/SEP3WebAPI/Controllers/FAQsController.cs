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

        [HttpPost]
        public async Task<ActionResult<FAQ>> AddFrequentlyAskedQuestionAsync([FromBody] FAQ faq) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            try {
                FAQ created = await service.AddFrequentlyAskedQuestionAsync(faq);
                return Created($"/{created.Id}", created);
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteFrequentlyAskedQuestionAsync([FromRoute] int id) {
            try {
                FAQ faq = await service.GetFrequentlyAskedQuestionAsync(id);
                if (faq == null) {
                    return NotFound($"No such FAQ found with id {id}");
                }
                
                await service.DeleteFrequentlyAskedQuestionAsync(id);
                return Ok();
            } catch (Exception e) {
                return StatusCode(500, e.Message);
            }
        }
    }
}