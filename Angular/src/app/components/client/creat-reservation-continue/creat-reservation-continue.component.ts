import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { IDayShift } from 'src/app/_models/_interfaces/IDayShift';
import { IDoctorWithClinic } from 'src/app/_models/_interfaces/IDoctorWithClinic';
import { IReservation } from 'src/app/_models/_interfaces/IReservation';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DayshiftService } from 'src/app/_services/dayshift.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { ReservationService } from 'src/app/_services/reservation.service';

@Component({
  selector: 'app-creat-reservation-continue',
  templateUrl: './creat-reservation-continue.component.html',
  styleUrls: ['./creat-reservation-continue.component.scss']
})
export class CreatReservationContinueComponent implements OnInit {
 
  constructor(private _sharedDataServices:DataSharedService,private _dayShiftServices:DayshiftService
    ,private _doctorService:DoctorService,private _fb:FormBuilder,private _roter:Router,
    private _reserveService:ReservationService) {
      _sharedDataServices.convertToReservationContinuePage.subscribe(data=>{
        if(data.reserveId!=0&&data.doctorName!=""){
          console.log(data)
          _reserveService.GetToShowInContinuePage(data.reserveId).subscribe(data=>{
            this.dayName= new Date(data.date).toDateString();
            this._dayShiftServices.GetById(data.dayShiftId).subscribe(data=>{
              console.log(data)
              this.dayShift=data;
              data.from =this.tConvert(data.from);
              data.to=this.tConvert(data.to);
            })
            this.reserve=data;
          })
          _doctorService.getDoctorWithClinicDetails(data.doctorName).subscribe(data=>{
            this.clinicDetails=data;
          })
        }
      })
  }

  reserve:IReservation;
  clinicDetails:IDoctorWithClinic;
  dayShift:IDayShift;
  dayName:string;
  ngOnInit(): void {
  }

  reserveContinue=this._fb.group({
    age:[],
    gender:[],
    symptom:[]
  })

  submit(){
    this.reserve.age=this.reserveContinue.get("age").value;
    this.reserve.gender=this.reserveContinue.get("gender").value;
    this.reserve.symptoms=this.reserveContinue.get("symptom").value;
    this._reserveService.update(this.reserve).subscribe(data=>{
      this._roter.navigate([""])
    },error=>console.error(error))


  }






  tConvert = (time) => {
    // Check correct time format and split into components
    time = time.toString ().match (/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
    if (time.length > 1) { // If time format correct
      time = time.slice (1);  // Remove full string match value
      time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
      time[0] = +time[0] % 12 || 12; // Adjust hours
      time.splice(3,1)
    }
    return time.join (''); // return adjusted time or original string
  }
}
