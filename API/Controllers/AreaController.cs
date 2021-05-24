using BL.AppServices;
using BL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: api/<AreaController>
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetAll(int pageSize, int pageNumber)
        {
            return Ok(_areaAppServices.GetAll(pageSize, pageNumber));
        }



        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_areaAppServices.GetById(id));
        }

        // POST api/<AreaController>
        [HttpPost]
        public IActionResult Post(AreaDTO areaDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_areaAppServices.CheckExistsByName(areaDTO))
                {
                    return BadRequest("The Area Is Already Exist");
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            try
            {
                AreaDTO newAreaDTO;
                if (User.IsInRole("Admin"))
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
        public ActionResult Put(int id, AreaDTO areaDTO)
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
    }
}
