using API.helpers;
using BL.AppServices;
using BL.DTOs.SubOfferDto;
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
    public class SubOfferController : ControllerBase
    {
        GeneralAppService _generalAppService;
        SubOfferAppService _subOfferAppService;
        public SubOfferController(GeneralAppService generalAppService, SubOfferAppService subOfferAppService)
        {
            _subOfferAppService = subOfferAppService;
            _generalAppService = generalAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_subOfferAppService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_subOfferAppService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(SubOfferDto subOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var create = _subOfferAppService.Insert(subOfferDTO);
                _generalAppService.CommitTransaction();
                return Created("created", create);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(SubOfferDto subOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _subOfferAppService.Update(subOfferDTO);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "updated" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _subOfferAppService.Delete(id);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Deleted" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{pageSize}/{pageNumber}")]
        public IActionResult GetByPage(int pageSize, int pageNumber)
        {
            return Ok(_subOfferAppService.GetPageRecords(pageSize, pageNumber));
        }

        [HttpGet("count")]
        public IActionResult GetCitiesCount()
        {
            return Ok(_subOfferAppService.CountEntity());
        }

    }
}
