using API.helpers;
using BL.AppServices;
using BL.DTOs;
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
        private DoctorAppService _doctorAppService;
        private GeneralAppService _generalAppService;
        public DoctorAttachmentController(DoctorAttachmentAppService doctorAttachmentAppService, DoctorAppService doctorAppService, GeneralAppService generalAppService)
        {
            _doctorAttachmentAppService = doctorAttachmentAppService;
            _doctorAppService = doctorAppService;
            _generalAppService = generalAppService;
        }
        [HttpGet("getOne")]
        public IActionResult GetOne()
        {
            string id = "ffeaa154-8a29-426b-bfde-8fbffe1361d4";    // will change in future
            return Ok(_doctorAttachmentAppService.GetById(id));
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
        // POST api/<DoctorAttachmentController>
        [HttpPost]
        public IActionResult Post( DoctorAttachmentDto doctorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                doctorDto.DoctorId = "ffeaa154-8a29-426b-bfde-8fbffe1361d4";    //change after login story
                DoctorAttachmentDto doctor = _doctorAttachmentAppService.Insert(doctorDto);
                _generalAppService.CommitTransaction();
                return Created("attachment send", doctorDto);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response() { Message = ex.Message });
            }
        }

        // PUT api/<DoctorAttachmentController>/5
        [HttpPut]
        public IActionResult Put(DoctorAttachmentDto attachmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _doctorAttachmentAppService.Update(attachmentDto);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "attachment are updated" });
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        [HttpPut("acceptAttachments/{id}")]
        public IActionResult acceptAttachment(string id)
        {
            try
            {
                _doctorAttachmentAppService.changeBindingStatus(id);
                _doctorAppService.activateDoctor(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="attachments is accepted"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("rejecteAttachments/{id}")]
        public IActionResult rejecteAttachment(string id)
        {
            try
            {
                _doctorAttachmentAppService.changeBindingStatus(id);
                _doctorAppService.deactivateDoctor(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "attachments is rejected" });
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
