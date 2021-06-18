using API.helpers;
using BL.AppServices;
using BL.DTOs.OfferDto;
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
    public class OfferController : ControllerBase
    {
        GeneralAppService _generalAppService;
        OfferAppService _offerAppService;
        public OfferController(GeneralAppService generalAppService,OfferAppService offerAppService)
        {
            _offerAppService = offerAppService;
            _generalAppService = generalAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_offerAppService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("withSubOffer")]
        public IActionResult GetAllWithSubOffer()
        {
            try
            {
                return Ok(_offerAppService.GetAllWithSubOffer());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("WithMakeOfferCount")]
        public IActionResult GetAllWithMakeOfferCount()
        {
            try
            {
                return Ok(_offerAppService.GetAllWithCountOfMakeOfferRelated());
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
                return Ok(_offerAppService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(OfferDTO offerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var create = _offerAppService.Insert(offerDTO);
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
        public IActionResult Update(OfferDTO offerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _offerAppService.Update(offerDTO);
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
                _offerAppService.Delete(id);
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
            return Ok(_offerAppService.GetPageRecords(pageSize, pageNumber));
        }

        [HttpGet("count")]
        public IActionResult GetCount()
        {
            return Ok(_offerAppService.CountEntity());
        }

    }
}
