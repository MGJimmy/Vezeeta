using API.helpers;
using BL.AppServices;
using BL.DTOs.DoctorServiceDtos;
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
    public class DoctorServiceController : ControllerBase
    {
        private DoctorServiceAppService _doctorServiceAppService;
        private GeneralAppService _generalAppService;
        public DoctorServiceController(DoctorServiceAppService doctorServiceAppService, GeneralAppService generalAppService)
        {
            _doctorServiceAppService = doctorServiceAppService;
            _generalAppService = generalAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_doctorServiceAppService.GetAll());
        }
        [HttpGet("count/{byAdmin}")]
        public IActionResult DoctorServicesCount(bool byAdmin)
        {
            return Ok(_doctorServiceAppService.CountEntity(byAdmin));
        }
        [HttpGet("{pageSize}/{pageNumber}/{byAdmin}")]
        public IActionResult GetSpecialitiesByPage(int pageSize, int pageNumber,bool byAdmin)
        {
            return Ok(_doctorServiceAppService.GetPageRecords(pageSize, pageNumber, byAdmin));
        }

        // GET api/<DoctorServiceController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_doctorServiceAppService.GetById(id));
        }

        // POST api/<DoctorServiceController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(DoctorServiceDto doctorServiceDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
           
            if (_doctorServiceAppService.CheckExistsByName(doctorServiceDto.Name))
            {
                return BadRequest(new Response { Message="This Name Alreay Exist"});
            }
            

            try
            {
                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (userRole == UserRoles.Admin)
                {
                    doctorServiceDto = _doctorServiceAppService.Create(doctorServiceDto, true);
                }
                else
                {
                    doctorServiceDto = _doctorServiceAppService.Create(doctorServiceDto, false);
                }

                _generalAppService.CommitTransaction();
                return Created("Area created", doctorServiceDto);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message=ex.Message});
            }
        }

        // PUT api/<DoctorServiceController>/5
        [HttpPut]
        public IActionResult Update(DoctorServiceDto doctorServiceDto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
          
            try
            {
                _doctorServiceAppService.Update(doctorServiceDto);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Updated Successfully"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        // DELETE api/<DoctorServiceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _doctorServiceAppService.Delete(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
        }
        [HttpPut("acceptDoctorService/{id}")]
        public IActionResult acceptDoctorService(int id)
        {
            try
            {
                _doctorServiceAppService.acceptDoctorService(id);
               
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Doctor Service is accepted" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("rejectDoctorService/{id}")]
        public IActionResult rejectDoctorService(int id)
        {
            try
            {
                _doctorServiceAppService.rejectDoctorService(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Doctor Service is rejected" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertList")]
        public  IActionResult InsertList(List<DoctorServiceDto> _doctorServiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                List<DoctorServiceDto> NewServiceList;
                if (User.IsInRole("Admin"))
                {
                    NewServiceList = _doctorServiceAppService.InsertList(_doctorServiceDto, true);
                }
                else
                {
                    NewServiceList = _doctorServiceAppService.InsertList(_doctorServiceDto, false);
                }
                _generalAppService.CommitTransaction();
                return Created("created", NewServiceList);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
        }




    }
}
