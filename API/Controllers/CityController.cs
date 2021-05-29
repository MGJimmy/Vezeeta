using API.helpers;
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
                bool isExist = _cityAppService.CheckCityExistByName(createCityDTO.Name);
                if (isExist)
                    return BadRequest(new Response { Message = "City name already exist" });
                CreateCityDTO result = _cityAppService.Insert(createCityDTO);

                _generalAppService.CommitTransaction();

                return Created("city created", result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(new Response { Message = ex.Message });

            }
        }

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

                return Ok(new Response { Message="City updated"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(new Response { Message =ex.Message });

            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cityAppService.Delete(id);

                _generalAppService.CommitTransaction();

                return Ok(new Response { Message = "City deleted successfully" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(new Response { Message = ex.Message });

            }
        }
        [HttpGet("count")]
        public IActionResult GetCitiesCount()
        {
            return Ok( _cityAppService.CountEntity() );
        }
        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetCitiesByPage(int pageSize, int pageNumber)
        {
            return Ok(_cityAppService.GetPageRecords(pageSize, pageNumber));
        }
    }
}
