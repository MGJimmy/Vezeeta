using BL.AppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API.helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class SpecialtyController : ControllerBase
    {
        private SpecialtyAppService _specialtyAppService;
        private GeneralAppService _generalAppService;
        public SpecialtyController(SpecialtyAppService specialtyAppService, GeneralAppService generalAppService)
        {
            _specialtyAppService = specialtyAppService;
            _generalAppService = generalAppService;
        }
        // GET: api/<SpecialtyController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok (_specialtyAppService.GetAll());
        }

        // GET api/<SpecialtyController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_specialtyAppService.Get(id));
        }

        // POST api/<SpecialtyController>
        [HttpPost]
        public IActionResult Create(CreateSpecialtyDTO createSpecialtyDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
              
                CreateSpecialtyDTO result = _specialtyAppService.Insert(createSpecialtyDTO);
                _generalAppService.CommitTransaction();
                return Created("Specialty created", result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // PUT api/<SpecialtyController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateSpecialtyDTO updateSpecialtyDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {

              _specialtyAppService.Update(updateSpecialtyDTO);

                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // DELETE api/<SpecialtyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _specialtyAppService.Delete(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Deleted"});
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("count")]
        public IActionResult SpecialityCount()
        {
            return Ok(_specialtyAppService.CountEntity());
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetSpecialitiesByPage(int pageSize, int pageNumber)
        {
            return Ok(_specialtyAppService.GetPageRecords(pageSize, pageNumber));
        }
    }
}
