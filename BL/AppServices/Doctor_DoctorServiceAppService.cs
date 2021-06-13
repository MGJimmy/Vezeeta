using AutoMapper;
using BL.Bases;
using BL.DTOs.Doctor_DoctorServiceDto;
using BL.DTOs.DoctorServiceDtos;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class Doctor_DoctorServiceAppService : BaseAppService
    {
       
        public Doctor_DoctorServiceAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
            
        }
        public IEnumerable<GetDoctor_DoctorService> GetAll()
        {
            return Mapper.Map<IEnumerable<GetDoctor_DoctorService>>(TheUnitOfWork.Doctor_DoctorServiceRepo.GetAll());
        }

        public List<CreateDoctor_DoctorService> InsertList(List<CreateDoctor_DoctorService> doctorservicesDtos ,string DoctorId)
        {
            if (doctorservicesDtos == null)
                throw new ArgumentNullException();

            for (int i = 0; i < doctorservicesDtos.Count; i++)
            {
                doctorservicesDtos[i].doctorID = DoctorId;
            }

            List<Doctor_DoctorService> doctorservices = Mapper.Map<List<Doctor_DoctorService>>(doctorservicesDtos);

            TheUnitOfWork.Doctor_DoctorServiceRepo.InsertList(doctorservices);
            TheUnitOfWork.SaveChanges();
           
            return doctorservicesDtos;

        }
       
        public IEnumerable<DoctorServiceDto> GetDoctorServices(string doctorId)
        {
            //return Mapper.Map<IEnumerable<GetDoctor_DoctorServiceWithService>>(TheUnitOfWork.Doctor_DoctorServiceRepo.GetAllWherewithService(ds=>ds.doctorID==doctorId));
            var xx = TheUnitOfWork.Doctor_DoctorServiceRepo.GetAllWherewithService(ds => ds.doctorID == doctorId);
            IEnumerable<GetDoctor_DoctorServiceWithService> result = Mapper.Map<IEnumerable<GetDoctor_DoctorServiceWithService>>(xx);

            List<DoctorService> ds = new List<DoctorService>();
            foreach (var item in result)
            {
                ds.Add(item.service);
            }

            return Mapper.Map<List<DoctorServiceDto>>(ds);
        }

        public void UpdateServicesList(List<CreateDoctor_DoctorService> _NewdoctorservicesList, string DoctorId)
        {
            if (_NewdoctorservicesList == null)
                throw new ArgumentNullException();

             List<Doctor_DoctorService> _oldDoctorServicesList =TheUnitOfWork.Doctor_DoctorServiceRepo.GetWhere(ds => ds.doctorID == DoctorId).ToList();
             List<CreateDoctor_DoctorService> newExistList=new List<CreateDoctor_DoctorService>();

            for (int i = 0; i < _NewdoctorservicesList.Count; i++)
            {
                _NewdoctorservicesList[i].doctorID = DoctorId;

                for(int j = 0; j< _oldDoctorServicesList.Count; j++)
                {
                    if (_NewdoctorservicesList[i].serviceID == _oldDoctorServicesList[j].serviceID)
                    {
                        newExistList.Add(_NewdoctorservicesList[i]);
                        _NewdoctorservicesList.RemoveAt(i);
                        i--;
                        break;
                     
                    }
                   
                }
            }
            //new 1    //old 2 3 4   // exist 2 3
            List<Doctor_DoctorService> ListToDelete = new List<Doctor_DoctorService>();
            for (int i = 0; i < _oldDoctorServicesList.Count; i++)
            {
                bool exist = false;
                for (int j = 0; j < newExistList.Count; j++)
                {
                    if (_oldDoctorServicesList[i].serviceID == newExistList[j].serviceID)
                    {
                        exist = true;
                        break;
                    }

                }
                if (exist == false)
                {
                    ListToDelete.Add(_oldDoctorServicesList[i]);
                    //_oldDoctorServicesList.RemoveAt(i);
                    //Doctor_DoctorService docSer = Mapper.Map<Doctor_DoctorService>(_oldDoctorServicesList[i]);
                    //TheUnitOfWork.Doctor_DoctorServiceRepo.Delete(docSer);
                    //TheUnitOfWork.SaveChanges();
                }
            }
           

            List<Doctor_DoctorService> doctorservices = Mapper.Map<List<Doctor_DoctorService>>(_NewdoctorservicesList);


            

            TheUnitOfWork.Doctor_DoctorServiceRepo.InsertList(doctorservices);
            TheUnitOfWork.Doctor_DoctorServiceRepo.DeleteList(ListToDelete);
            TheUnitOfWork.SaveChanges();

        }



    }
}
