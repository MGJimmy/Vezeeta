using BL.AppServices;
using BL.DTOs;
using DAL.Models;
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
    public class CityController : ControllerBase
    {
        private CityAppService _cityAppService;
        private GeneralAppService _generalAppService;
        public CityController(CityAppService cityAppService, GeneralAppService generalAppService)
        {
            _cityAppService = cityAppService;
            _generalAppService = generalAppService;
        }
        // GET: api/<CityController>
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public string GetByID(int id)
        {
            return "value";
        }

        // POST api/<CityController>
        [HttpPost]
        public IActionResult Create(CreateCityDTO createCityDTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                 CreateCityDTO result = _cityAppService.Insert(createCityDTO);

                _generalAppService.CommitTransaction();

                return Created("city created", result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public void Update(int id, UpdateCityDTO updateCityDTO)
        {
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
