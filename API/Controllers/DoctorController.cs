using API.helpers;
using AutoMapper;
using BL.AppServices;
using BL.DTOs;
using BL.DTOs.ClinicImagesDto;
using BL.DTOs.Doctor_DoctorServiceDto;
using BL.DTOs.DoctorDTO;
using BL.DTOs.DoctorServiceDtos;
using BL.DTOs.ReversationDto;
using BL.DTOs.UserDto;
using BL.DTOs.WorkingDayDTO;
using BL.StaticClasses;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorAppService _doctorAppService;
        AccountAppService _accountAppService;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        Doctor_DoctorServiceAppService _doctor_DoctorServiceAppService;
        DoctorSubSpecializationAppService _doctorSubSpecialization;
        private ClinicAppService _clinicAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        private AreaAppService _areaAppService;
        private CityAppService _cityAppService;
        WorkingDayAppService _workingDayAppService;
        DayShiftAppService _dayShiftAppService;
        RatingAppService _ratingAppService;
        ReservationAppService _reservationAppService;
        public DoctorController(
             DoctorAppService doctorAppService, 
             AccountAppService accountAppService,
             GeneralAppService generalAppService,
             IHttpContextAccessor httpContextAccessor,
             Doctor_DoctorServiceAppService doctor_DoctorServiceAppService,
             DoctorSubSpecializationAppService doctorSubSpecialization,
             ClinicAppService clinicAppService,
             ClinicImagesAppService clinicImagesAppService,
             AreaAppService areaAppService,
             CityAppService cityAppService,
             WorkingDayAppService workingDayAppService,
             DayShiftAppService dayShiftAppService,
             RatingAppService ratingAppService,
             ReservationAppService reservationAppService)
        {
            _doctorAppService = doctorAppService;
            _accountAppService = accountAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
            _doctor_DoctorServiceAppService = doctor_DoctorServiceAppService;
            _doctorSubSpecialization = doctorSubSpecialization;
            _clinicAppService = clinicAppService;
            _clinicImagesAppService = clinicImagesAppService;
            _areaAppService = areaAppService;
            _cityAppService = cityAppService;
            _workingDayAppService = workingDayAppService;
            _dayShiftAppService = dayShiftAppService;
            _ratingAppService = ratingAppService;
            _reservationAppService = reservationAppService;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Doctor doctor = _doctorAppService.GetById(doctorId);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        [HttpGet("GetWithClinicForReservetionCart/{name}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetWithClinicForReservetionCart(string name)
        {
            try
            {
                GetDoctorWithClinicForReservetionCartDTO doctor = _doctorAppService.GetWithClinicForReservetionCart(name);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        [HttpGet("doctorByName/{doctorName}")]
        public IActionResult GetDoctorWithName(string doctorName)
        {
            try
            {
                var doctor = _doctorAppService.GetByName(doctorName);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> RegisterDoctor(CreateDoctorDTO registerDoctorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isUsernameExist = await _accountAppService.checkUsernameExist(registerDoctorDTO.UserName);
            if (isUsernameExist)
                return BadRequest(new Response { Message = "Username already exist" });
            bool isEmailExist = await _accountAppService.checkEmailExist(registerDoctorDTO.Email);
            if (isEmailExist)
                return BadRequest(new Response { Message = "Email already exist" });
            try
            {
                registerDoctorDTO.IsDoctor = true;
                ApplicationUserIdentity registerUser = await _accountAppService.Register(registerDoctorDTO);
                await _accountAppService.AssignToRole(registerUser.Id, UserRoles.Doctor);
                _doctorAppService.Create(registerUser.Id, registerDoctorDTO);
                _generalAppService.CommitTransaction();
                 return Ok(new Response { Message = "Doctor created successfully" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }

        }

        
        [HttpPost("addServices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddServiceForDoctor(List<CreateDoctor_DoctorService> doctorservives)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _doctor_DoctorServiceAppService.InsertList(doctorservives,DoctorId);
                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }
        [HttpGet("Myservices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult myServices()
        {
            var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var services = _doctor_DoctorServiceAppService.GetDoctorServices(DoctorId);
            return Ok(services);
        }

        [HttpPut("UpdateDoctorServices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UpdateDoctorServices(List<CreateDoctor_DoctorService> NewdoctorservivesList)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _doctor_DoctorServiceAppService.UpdateServicesList(NewdoctorservivesList, DoctorId);
                 _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }


        

        //[HttpPost("assignSpecialty")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> InsertSpecialtyToDoctor(SpecialtyDTO specialtDto)
        //{
        //    try
        //    {
        //        var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        _doctorAppService.InsertSpecialtyToDoctor(doctorId, specialtDto);
        //        _generalAppService.CommitTransaction();
        //        return Ok("created");
        //    }
        //    catch (Exception ex)
        //    {
        //        _generalAppService.RollbackTransaction();
        //        return BadRequest(new Response { Message = ex.Message });
        //    }
        //}
        //[HttpPost("assignSubSpecialty")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> InsertSubSpecialtyToDoctor(List<SupSpecailization> subSpecialtDto)
        //{
        //    try
        //    {
        //        var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        _doctorAppService.EmptySubSpecialtyInDoctor(doctorId);
        //        //_generalAppService.CommitTransaction();
        //        _doctorAppService.InsertSubSpecialtyToDoctor(doctorId, subSpecialtDto);
        //        _generalAppService.CommitTransaction();
        //        return Ok(new Response { Message="subspecialty inserted to doctor" });
        //    }
        //    catch (Exception ex)
        //    {
        //        _generalAppService.RollbackTransaction();
        //        return BadRequest(new Response { Message = ex.Message });
        //    }
        //}

        [HttpGet("GetAllWhere/{SpecailtyID}")]
        public IActionResult GetAllDoctorsInSpecailty(int SpecailtyID)
        {
            List<GetDoctorDto> doctors=this._doctorAppService.GetAllDoctorWhere(SpecailtyID);
            foreach(var doctor in doctors)
            {
                var _services = _doctor_DoctorServiceAppService.GetDoctorServices(doctor.UserId);
                var _subSpecails = _doctorSubSpecialization.GetSubSpecialtyByDoctorId(doctor.UserId);
                var _clinic=_clinicAppService.GetByStringId(doctor.UserId);

                double doctorAverageRate = _ratingAppService.getPublicRateForDoctor(doctor.UserId);


                doctor.services = _services;
                doctor.subspecails = _subSpecails;
                doctor.clinic = _clinic;
                doctor.clinicAreaName = _areaAppService.GetById(_clinic.AreaId).Name;
                doctor.clinicCityName = _cityAppService.Get(_clinic.CityId).Name;
                IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(doctor.UserId);
                doctor.workingDays = workingDaysDTOs.ToList();
                doctor.averageRate = doctorAverageRate;



            }
           

            return Ok(doctors);
        
        }

        [HttpGet("DoctorDetails/{Doctor_ID}")]
        public IActionResult DoctorDetails(string Doctor_ID)
        {
            GetDoctorDto doctor = this._doctorAppService.GetDoctorDetails(Doctor_ID);
            
                var _services = _doctor_DoctorServiceAppService.GetDoctorServices(doctor.UserId);
                var _subSpecails = _doctorSubSpecialization.GetSubSpecialtyByDoctorId(doctor.UserId);
                var _clinic = _clinicAppService.GetByStringId(doctor.UserId);


                doctor.services = _services;
                doctor.subspecails = _subSpecails;
                doctor.clinic = _clinic;
                doctor.clinicAreaName = _areaAppService.GetById(_clinic.AreaId).Name;
                doctor.clinicCityName = _cityAppService.Get(_clinic.CityId).Name;
                IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(doctor.UserId);
                doctor.workingDays = workingDaysDTOs.ToList();
                
                IEnumerable<GetClinicImageDto> clinicImagesDto =_clinicImagesAppService.GetAllWhere(doctor.UserId);
                doctor.Clinic_Images = clinicImagesDto.ToList();


            return Ok(doctor);

        }

        //[HttpGet("search/{pageSize}/{pageNumber}")]
        //public IActionResult SearchForDoctor(int pageSize, int pageNumber, int? specialtyId, int? cityId, int? areaId, string name)
        //{
        //    return Ok(_doctorAppService.SearchForDoctor(pageSize, pageNumber, specialtyId, cityId, areaId, name));
        //}

        [HttpPost("FilterDoctors")]//{specailtyid}/{titles}
        public IActionResult FilterDoctors(FilterDoctorDto filterdoctorDto)//int specailtyid,List<string> titles
        {

            List<GetDoctorDto> doctors = this._doctorAppService.filterDoctors(filterdoctorDto);
            foreach (var doctor in doctors)
            {
                var _services = _doctor_DoctorServiceAppService.GetDoctorServices(doctor.UserId);
                var _subSpecails = _doctorSubSpecialization.GetSubSpecialtyByDoctorId(doctor.UserId);
                var _clinic = _clinicAppService.GetByStringId(doctor.UserId);
                double doctorAverageRate = _ratingAppService.getPublicRateForDoctor(doctor.UserId);

                doctor.services = _services;
                doctor.subspecails = _subSpecails;
                doctor.clinic = _clinic;
                doctor.clinicAreaName = _areaAppService.GetById(_clinic.AreaId).Name;
                doctor.clinicCityName = _cityAppService.Get(_clinic.CityId).Name;
                IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(doctor.UserId);
                doctor.workingDays = workingDaysDTOs.ToList();
                doctor.averageRate = doctorAverageRate;


            }

           /* if (filterdoctorDto.fee.Count > 0)
            {
                foreach (var doctor in doctors.ToList())
                {
                    bool del = true;
                   foreach (var fee in filterdoctorDto.fee)
                    {
                        if (fee.MiniMoney <= doctor.clinic.Fees && doctor.clinic.Fees < fee.MaxMoney)
                        {
                            del = false;
                            break;
                        }
                    }
                   if(del)
                    doctors.Remove(doctor);
                }
                
            }
           
            if (filterdoctorDto.subspecails.Count > 0)
            {
                foreach (var doctor in doctors.ToList())
                {
                    bool del = true;
                    foreach (var sub in filterdoctorDto.subspecails)
                    {
                        foreach (var item in doctor.subspecails)
                        {
                            if (item.Name == sub)
                            {
                                del = false;
                                break;
                            }
                        }
                        if (del==false)
                            break;
                    }
                    if (del)
                        doctors.Remove(doctor);
                }

            }*/


            return Ok(doctors);

        }




        [HttpGet("GetSuggestionDoctors")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetSuggestionDoctors()
        {
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<string> DoctorsIds = _reservationAppService.GetLast3doctorIdsReservedbyPatientforSuggestion(UserId);

                if (DoctorsIds.Count == 0)
                    return Ok(_doctorAppService.GetSuggestiondoctorsTopRated());

                return Ok(_doctorAppService.GetSuggestiondoctorsRelatedToSpecailties(DoctorsIds));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSuggestionDoctorsforGuest")]
        public IActionResult GetSuggestionDoctorsforGuest()
        {
            try
            {
                return Ok(_doctorAppService.GetSuggestiondoctorsTopRated());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("checkDoctorAccoutIsAccept")]
        public IActionResult checkAcceptinationOfDoctorAccount()
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var isDoctorAcceptDTO = _doctorAppService.checkAcceptinationOfDoctorAccount(doctorId);
                _generalAppService.CommitTransaction();
                return Ok(isDoctorAcceptDTO);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

    }
}
