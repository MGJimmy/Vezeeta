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
    public class SupSpecializationController : ControllerBase
    {
        private SupSpecializationAppService _supSpecializationAppService;
        private GeneralAppService _generalAppService;
        public SupSpecializationController(SupSpecializationAppService supSpecializationAppService, GeneralAppService generalAppService)
        {
            _supSpecializationAppService = supSpecializationAppService;
            _generalAppService = generalAppService;
        }
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

        // POST api/<SupSpecializationController>
        [HttpPost]
        public IActionResult Create(SupSpecailizationDto createSupSpecailizationDtoDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SupSpecailizationDto result = _supSpecializationAppService.Insert(createSupSpecailizationDtoDTO);
                _generalAppService.CommitTransaction();
                return Created("SupSpecialization created", result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // PUT api/<SupSpecializationController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, SupSpecailizationDto SupSDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {

                _supSpecializationAppService.Update(SupSDTO);

                _generalAppService.CommitTransaction();
                return Ok("SupSpecialize updated");
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
                return Ok("deleted");
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
    }
}
