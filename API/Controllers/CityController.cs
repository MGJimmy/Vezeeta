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
        public IActionResult GetAll()
        {
            return Ok(_cityAppService.GetAll());
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            return Ok(_cityAppService.Get(id));
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
        public IActionResult Update(int id, UpdateCityDTO updateCityDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _cityAppService.Update(updateCityDTO);

                _generalAppService.CommitTransaction();

                return Ok("city updated");
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }

        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cityAppService.Delete(id);

                _generalAppService.CommitTransaction();

                return Ok("city deleted");
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }
    }
}
