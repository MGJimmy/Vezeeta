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
        [HttpGet("isAccepted")]
        public IActionResult GetDoctorAttachment(bool isAcepted)
        {
            return Ok(_doctorAttachmentAppService.GetDoctorAttachment(isAcepted));
        }

        // GET api/<DoctorAttachmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorAttachmentController>
        [HttpPost]
        public IActionResult Post( DoctorAttachmentDto doctorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                doctorDto.DoctorId = "3b9a541e-b4dd-4906-ba4c-728119e9f00b";    //change after login story
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorAttachmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
