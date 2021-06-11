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
    public class ClincServicesController : ControllerBase
    {
        private ClinicServicesAppServices _clinicServicesAppServices;
        private GeneralAppService _generalAppService;
        public ClincServicesController(ClinicServicesAppServices clinicServicesAppServices, GeneralAppService generalAppService)
        {
            _clinicServicesAppServices = clinicServicesAppServices;
            _generalAppService = generalAppService;
        }
        // GET: api/<ClincServicesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_clinicServicesAppServices.GetAll());
        }
        [HttpGet("getAccepted")]
        public IActionResult GetServicesAcceptedByAdmin()
        {
            return Ok(_clinicServicesAppServices.GetServicesAcceptedByAdmin());
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetByPage( int pageSize , int pageNumber ) {

            return Ok(_clinicServicesAppServices.GetPageRecords(pageSize, pageNumber));
        }

        // GET api/<ClincServicesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_clinicServicesAppServices.Get(id));
        }

        // POST api/<ClincServicesController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(ClinicServiceDto clinicServiceDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (userRole == UserRoles.Admin)
                    clinicServiceDto.ByAdmin = true;
                else
                    clinicServiceDto.ByAdmin = false;
                if (_clinicServicesAppServices.CheckClinicServicesExistsByName(clinicServiceDto.Name))
                {
                    return BadRequest( new Response() { Message = "This service is Added before" });
                }

                ClinicServiceDto result = _clinicServicesAppServices.Insert(clinicServiceDto);

                _generalAppService.CommitTransaction();
                return Created("created", result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        [HttpPost("addList")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddList(List<ClinicServiceDto> clinicServiceDtos)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                foreach (var clinicServiceDto in clinicServiceDtos)
                {
                    if (userRole == UserRoles.Admin)
                        clinicServiceDto.ByAdmin = true;
                    else
                        clinicServiceDto.ByAdmin = false;
                }

                var insertedClinicServices = _clinicServicesAppServices.InsertList(clinicServiceDtos);

                _generalAppService.CommitTransaction();
                return Created("created", insertedClinicServices);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }



        // PUT api/<ClincServicesController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ClinicServiceDto clinicServiceDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {

                _clinicServicesAppServices.Update(clinicServiceDto);

                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Updated"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // DELETE api/<ClincServicesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                
                _clinicServicesAppServices.Delete(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Deleted"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("count")]
        public IActionResult GetClincServiceCount()
        {
            return Ok(_clinicServicesAppServices.CountEntity());
        }
    }
}
