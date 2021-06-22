using API.helpers;
using BL.AppServices;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DoctorAttachmentController : ControllerBase
    {
        private DoctorAttachmentAppService _doctorAttachmentAppService;
        private DoctorAppService _doctorAppService;
        private GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        public DoctorAttachmentController(
            DoctorAttachmentAppService doctorAttachmentAppService
            , DoctorAppService doctorAppService,
            GeneralAppService generalAppService,
              IHttpContextAccessor httpContextAccessor)
        {
            _doctorAttachmentAppService = doctorAttachmentAppService;
            _doctorAppService = doctorAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("getOne")]
        public IActionResult GetOne()
        {
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

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
        public IActionResult GetDoctorAttachmentsPage(int pageSize, int pageNumber)
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
                var user =_httpContextAccessor.HttpContext.User.Claims;
               
                
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                doctorDto.DoctorId = userID; //"625a57bf-8c7d-4f34-b98f-92df9f2f86ad";    //change after login story
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
                _doctorAttachmentAppService.changeBindingAndRejectedStatus(id,false);
                //_doctorAppService.activateDoctor(id);
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
                _doctorAttachmentAppService.changeBindingAndRejectedStatus(id,true);
                //_doctorAppService.deactivateDoctor(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "attachments is rejected" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }


        
    }
}
