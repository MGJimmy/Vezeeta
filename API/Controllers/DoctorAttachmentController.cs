using API.helpers;
using BL.AppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAttachmentController : ControllerBase
    {
        private DoctorAttachmentAppService _doctorAttachmentAppService;
        private GeneralAppService _generalAppService;
        public DoctorAttachmentController(DoctorAttachmentAppService doctorAttachmentAppService, GeneralAppService generalAppService)
        {
            _doctorAttachmentAppService = doctorAttachmentAppService;
            _generalAppService = generalAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_doctorAttachmentAppService.GetAll());
        }
        // GET: api/<DoctorAttachmentController>
        [HttpGet("{isAccepted}")]
        public IActionResult GetDoctorAttachment(bool isAccepted)
        {
           
            var t= _doctorAttachmentAppService.GetDoctorAttachment(isAccepted);
            return Ok(t);
        }
        [HttpGet("count")]
        public IActionResult DoctorAttachmentCount()
        {
            return Ok(_doctorAttachmentAppService.CountEntity());
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetSpecialitiesByPage(int pageSize, int pageNumber)
        {
            return Ok(_doctorAttachmentAppService.GetPageRecords(pageSize, pageNumber));
        }
        [HttpPut("{id}")]
        public IActionResult acceptAttachment(string id)
        {
            try
            {
                _doctorAttachmentAppService.acceptAttachments(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="attachments is accepted"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }


        // GET api/<DoctorAttachmentController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<DoctorAttachmentController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<DoctorAttachmentController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DoctorAttachmentController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
