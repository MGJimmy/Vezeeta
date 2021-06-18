import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IDoctor, _IdoctorFilter } from '../_models/_interfaces/IDoctorPresentaion';

@Injectable({
  providedIn: 'root'
})
export class DataSharedService {

  constructor() { }

  IsDoctorSideBarChange=new BehaviorSubject(false);

  convertToReservationContinuePage=new BehaviorSubject(ReservationContinueData);
  GoToReservationPage=new BehaviorSubject(ReservationData);
  GoToOfferDetailsPage=new BehaviorSubject(0);
  GoToReserveOfferComponent=new BehaviorSubject(ReserveOfferData)
  sendDataToSearchComponent=new BehaviorSubject(doctorFilter)
  sendSpecialtyIdToSideBarComponent=new BehaviorSubject(0);
  sendAllDocterAfterFilterToShow=new BehaviorSubject(allDoctorFiltered);
  sendSpecialtyIdFromHomePageToSearchComponent=new BehaviorSubject(0);

  currentLoginUserChange=new BehaviorSubject(false);
  
}
const allDoctorFiltered:IDoctor[]=[];
const doctorFilter:_IdoctorFilter = {
  specailtyid:null,
  cityId:null,
  areaId:null,
  name:null,
  title:[],
  fee:[],
  subspecails:[]
}
const ReservationData={
  dayShiftId:0,
  doctorName:"",
  date:""
}
const ReserveOfferData={
  dayShiftId:0,
  date:""
}
const ReservationContinueData={
  reserveId:0,
  doctorName:""
}

