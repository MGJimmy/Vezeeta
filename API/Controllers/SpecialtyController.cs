using BL.AppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTOs;

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
        public IEnumerable<SpecialtyDTO> Get()
        {
            return _specialtyAppService.GetAll();
        }

        // GET api/<SpecialtyController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_specialtyAppService.Get(1));
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpecialtyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
