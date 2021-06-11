import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataSharedService {

  constructor() { }

  IsDoctorSideBarChange=new BehaviorSubject(false);

  convertToReservationContinuePage=new BehaviorSubject(ReservationContinueData);
  
}
const ReservationContinueData={
  reserveId:0,
  doctorName:""
}
