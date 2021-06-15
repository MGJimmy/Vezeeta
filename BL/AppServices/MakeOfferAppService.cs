﻿using AutoMapper;
using BL.Bases;
using BL.DTOs.MakeOfferDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class MakeOfferAppService:BaseAppService
    {
        public MakeOfferAppService(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public List<GetMakeOfferWithDoctorInfoDTO> GetAll()
        {
            var x = Mapper.Map<List<GetMakeOfferWithDoctorInfoDTO>>(TheUnitOfWork.MakeOfferRepo.GetAll());
            return x;
        }
        public List<GetMakeOfferDTO> GetAllByDoctorId(string id)
        {
            return Mapper.Map<List<GetMakeOfferDTO>>(TheUnitOfWork.MakeOfferRepo.GetAllByDoctorId(id));
        }
        public GetMakeOfferWithDoctorInfoDTO GetById(int id)
        {
            return Mapper.Map<GetMakeOfferWithDoctorInfoDTO>(TheUnitOfWork.MakeOfferRepo.GetById(id));
        }

        public CreateMakeOfferDTO create(string doctorId, CreateMakeOfferDTO offerDTO)
        {
            offerDTO.DoctorId = doctorId;
            var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
            var inserted = TheUnitOfWork.MakeOfferRepo.Insert(makeOffer);
            TheUnitOfWork.SaveChanges();
            offerDTO.Id = inserted.Id;
            return offerDTO;
        }

        public CreateMakeOfferDTO update(CreateMakeOfferDTO offerDTO)
        {
            var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
            TheUnitOfWork.MakeOfferRepo.Update(makeOffer);
            TheUnitOfWork.SaveChanges();
            return offerDTO;
        }
    }
}