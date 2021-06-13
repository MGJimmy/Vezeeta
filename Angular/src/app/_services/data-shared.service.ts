import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataSharedService {

  constructor() { }

  IsDoctorSideBarChange=new BehaviorSubject(false);

  convertToReservationContinuePage=new BehaviorSubject(ReservationContinueData);
  GoToReservationPage=new BehaviorSubject(ReservationData);
  
}
const ReservationData={
  dayShiftId:0,
  doctorName:"",
  date:""
}
const ReservationContinueData={
  reserveId:0,
  doctorName:""
}

