﻿using BL.AppServices;
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

        // GET api/<ClincServicesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_clinicServicesAppServices.Get(id));
        }

        // POST api/<ClincServicesController>
        [HttpPost]
        public IActionResult Create(ClinicServiceDto clinicServiceDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {

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
                return Ok("updated");
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