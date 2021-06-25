using API.helpers;
using BL.AppServices;
using BL.DTOs;
using BL.StaticClasses;
using Microsoft.AspNetCore.Authorization;
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
    public class SupSpecializationController : ControllerBase
    {
        private SupSpecializationAppService _supSpecializationAppService;
        private GeneralAppService _generalAppService;
        public SupSpecializationController(SupSpecializationAppService supSpecializationAppService, GeneralAppService generalAppService)
        {
            _supSpecializationAppService = supSpecializationAppService;
            _generalAppService = generalAppService;
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetByPage(int pageSize, int pageNumber)
        {
            return Ok(_supSpecializationAppService.GetPageRecords(pageSize, pageNumber));
        }

        ///
        [HttpGet]
        [Route("/api/NotAcceptedSubSpecail/Admin/{pageSize}/{pageNumber}")]
        public IActionResult GetNotAcceptByPage(int pageSize, int pageNumber)
        {
            return Ok(_supSpecializationAppService.GetNotAcceptPageRecords(pageSize, pageNumber));
        }


        //[HttpGet]
        //[Route("/api/NotAcceptedSubSpecail/Admin")]
        //public IActionResult GetAllNotAccepted()
        //{
        //    return Ok(_supSpecializationAppService.GetAllNotAcceptedWithCity());
        //}

        [HttpGet("count")]
        public IActionResult SubspecailAcceptedCount()
        {
            return Ok(_supSpecializationAppService.CountOfAccept());
        }
        [HttpGet("/api/SubspecailNotAccepted/admin/count")]
        public IActionResult SubspecailNotAcceptedCount()
        {
            return Ok(_supSpecializationAppService.CountOfNotAccept());
        }

        //
        //[HttpGet("count")]
        //public IActionResult SubSpecialityCount()
        //{
        //    return Ok(_supSpecializationAppService.CountEntity());
        //}

        // GET: api/<SupSpecializationController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_supSpecializationAppService.GetAll());
        }

        // GET api/<SupSpecializationController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_supSpecializationAppService.Get(id));
        }

        [Route("api/Admin")]
        [HttpGet]
        public IActionResult GetAllNotAccepted()
        {
            return Ok(_supSpecializationAppService.GetAllNotAccepted());
        }
        [HttpGet("getWhere/{specialId}/{byAdmin}")]
        public IActionResult GetAllWhere(int specialId,bool byAdmin)
        {
            return Ok(_supSpecializationAppService.GetWhere(i => i.specialtyId == specialId&& i.ByAdmin== byAdmin));
        }


        // POST api/<SupSpecializationController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(SupSpecailization createSupSpecailizationDtoDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_supSpecializationAppService.CheckExistsByName(createSupSpecailizationDtoDTO))
                {
                    return BadRequest("The SupSpecialize Is Already Exist");
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            try
            {
                SupSpecailization newSupDTO;

                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (userRole == UserRoles.Admin)
                {
                    newSupDTO= _supSpecializationAppService.Insert(createSupSpecailizationDtoDTO, true);
                }
                else
                {
                    newSupDTO = _supSpecializationAppService.Insert(createSupSpecailizationDtoDTO, false);
                }
                _generalAppService.CommitTransaction();
                return Created("SubSpecail created", newSupDTO);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("insertList")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult InsertList(List<SupSpecailization> supSpecailizations)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                List<SupSpecailization> newSup; 
                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (userRole == UserRoles.Admin)
                {
                    newSup = _supSpecializationAppService.InsertList(supSpecailizations, true);
                }
                else
                {
                    newSup = _supSpecializationAppService.InsertList(supSpecailizations, false);
                }
                _generalAppService.CommitTransaction();
                return Created("created", newSup);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        // PUT api/<SupSpecializationController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, SupSpecailization SupSDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (id != SupSDTO.ID)
            {
                return BadRequest();
            }
            try
            {
                _supSpecializationAppService.Update(SupSDTO);
                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // DELETE api/<SupSpecializationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _supSpecializationAppService.Delete(id);
                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
    }
}
