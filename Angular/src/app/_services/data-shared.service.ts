import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataSharedService {

  constructor() { }

  IsDoctorSideBarChange=new BehaviorSubject(false);


  
}
