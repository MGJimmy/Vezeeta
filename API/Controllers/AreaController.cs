using BL.AppServices;
using BL.DTOs;
using BL.DTOs.AreaDTO;
using BL.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private AreaAppService _areaAppServices;
        private GeneralAppService _generalAppService;
        public AreaController(AreaAppService areaAppServices, GeneralAppService generalAppService)
        {
            _areaAppServices = areaAppServices;
            _generalAppService = generalAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_areaAppServices.GetAll());
        }

        [HttpGet]
        [Route("/api/GetAllWhere/{Cityid}")]
        public IActionResult GetAllwhere(int Cityid)
        {
            return Ok(_areaAppServices.GetAllWhere(Cityid));
        }

        [HttpGet]
        [Route("/api/AreaWithCity")]
        public IActionResult GetAllWithCity()
        {
            return Ok(_areaAppServices.GetAllWithCity());
        }


        [HttpGet]
        [Route("/api/AreaWithCity/{pageSize}/{pageNumber}")]
        public IActionResult GetByPage(int pageSize, int pageNumber)
        {
            return Ok(_areaAppServices.GetPageRecords(pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/AreaWithCity/Admin/{pageSize}/{pageNumber}")]
        public IActionResult GetNotAcceptByPage(int pageSize, int pageNumber)
        {
            return Ok(_areaAppServices.GetNotAcceptPageRecords(pageSize, pageNumber));
        }


        [HttpGet]
        [Route("/api/AreaWithCity/Admin")]
        public IActionResult GetAllNotAccepted()
        {
            return Ok(_areaAppServices.GetAllNotAcceptedWithCity());
        }


        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_areaAppServices.GetById(id));
        }

        // POST api/<AreaController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Post(CreateAreaDTO areaDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_areaAppServices.CheckExistsByName(areaDTO.Name))
                {
                    return BadRequest("The Area Is Already Exist");
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            try
            {
                CreateAreaDTO newAreaDTO;
                string userRole = HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (userRole == UserRoles.Admin)
                {
                    newAreaDTO = _areaAppServices.Insert(areaDTO, true);
                }
                else
                {
                    newAreaDTO = _areaAppServices.Insert(areaDTO, false);
                }
                _generalAppService.CommitTransaction();
                return Created("Area created",newAreaDTO);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AreaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, UpdateAreaDTO areaDTO)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            if (id != areaDTO.ID)
                return BadRequest();
            try
            {
                _areaAppServices.Update(areaDTO);
                _generalAppService.CommitTransaction();
                return Ok();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AreaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _areaAppServices.Delete(id);
                _generalAppService.CommitTransaction();
                return Ok();
            }
            catch (Exception ex2)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex2.Message);
            }
        }

        [HttpGet("count")]
        public IActionResult AreaAcceptedCount()
        {
            return Ok(_areaAppServices.CountOfAccept());
        }
        [HttpGet("/api/area/admin/count")]
        public IActionResult AreaNotAcceptedCount()
        {
            return Ok(_areaAppServices.CountOfNotAccept());
        }
    }
}
