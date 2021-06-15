using BL.AppServices;
using BL.DTOs.MakeOfferImageDTO;
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
    public class MakeOfferImageController : ControllerBase
    {
        GeneralAppService _generalAppService;
        MakeOfferImageAppService _makeOfferImageAppService;
        public MakeOfferImageController(GeneralAppService generalAppService, MakeOfferImageAppService makeOfferImageAppService)
        {
            _generalAppService = generalAppService;
            _makeOfferImageAppService = makeOfferImageAppService;
        }

        [HttpPost]
        public IActionResult Create(List<CreateMakeOfferImageDTO> dto)
        {
            try
            {
                _makeOfferImageAppService.CreateList(dto);
                _generalAppService.CommitTransaction();
                return Created("created", null);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
    }
}
