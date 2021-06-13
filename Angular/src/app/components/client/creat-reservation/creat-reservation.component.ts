import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IDayShift } from 'src/app/_models/_interfaces/IDayShift';
import { IDoctorDetails } from 'src/app/_models/_interfaces/IDoctorDetails';
import { IUserForReservation } from 'src/app/_models/_interfaces/IUserForReservation';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DayshiftService } from 'src/app/_services/dayshift.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { IReservation } from 'src/app/_models/_interfaces/IReservation';
import { ReservationService } from 'src/app/_services/reservation.service';
import { environment } from 'src/environments/environment';
import { DataSharedService } from 'src/app/_services/data-shared.service';


@Component({
  selector: 'app-creat-reservation',
  templateUrl: './creat-reservation.component.html',
  styleUrls: ['./creat-reservation.component.scss']
})
export class CreatReservationComponent implements OnInit {
  
  constructor(private _router:Router,private _doctorService:DoctorService,private _reserveService:ReservationService,
    private _dayshiftService:DayshiftService, private _fb:FormBuilder,private _auth:AuthenticationService
    ,private _sharedDateServes:DataSharedService)
    {
      _sharedDateServes.GoToReservationPage.subscribe(data=>{
        if(data.dayShiftId!=0,data.doctorName!=""){
          console.log("in shared data");
          console.error(data)
          this.doctorName=data.doctorName;
          this.dayShiftId=data.dayShiftId;
          this.dateOfReserve=data.date;
          this.load();
        }
      })
    }

  doctorName:string="bahy";
  dayShiftId:number=1;
  dateOfReserve;
  
  doctorDetails:IDoctorDetails;
  dayShift:IDayShift;
  patient:IUserForReservation;
  
  ngOnInit(): void {
    
  }
  load(){
    this._doctorService.getDoctorByName(this.doctorName).subscribe(data=>{
      this.doctorDetails=data;
      this.doctorDetails.image=environment.apiUrl+"/"+this.doctorDetails.image;
    },error=>{console.error(error)});

    this._dayshiftService.GetById(this.dayShiftId).subscribe(data=>{
      this.dayShift=data;
      this.dayShift.to=this.tConvert(this.dayShift.to);
      this.dayShift.from=this.tConvert(this.dayShift.from);
    },error=>{console.error(error)});

    this.loadDataToFor()
  }
  reservationData=this._fb.group({
    UserName:['',Validators.required],
    Phone:['',Validators.required],
    Email:['']
  })

  loadDataToFor(){
    this._auth.getCurrentUser().subscribe(data=>{
      this.patient=data;
      this.reservationData.setValue({
        UserName:data.fullName,
        Phone:data.phoneNumber,
        Email:data.email
      })
    },error=>{console.error(error)});
  }

  empty(e){
    if(e.target.checked){
      this.reservationData.setValue({
        UserName:'',
        Phone:'',
        Email:''
      })
    }else{
      this.loadDataToFor()
    }
  }


  submit(){
    const newReserve:IReservation={
      userName:this.reservationData.get("UserName").value,
      phone:this.reservationData.get("Phone").value,
      email:this.reservationData.get("Email").value,
      dayShiftId:this.dayShiftId,
      doctorId:this.doctorDetails.userId,
      date:"2021-06-09T18:07:13",

    }
    this._reserveService.createReservation(newReserve).subscribe((data)=>{
      this._router.navigate(["/ReversationContinue"]).then(()=>this._sharedDateServes.convertToReservationContinuePage.next({reserveId:data,doctorName:this.doctorDetails.userUserName}));
    },error=>{console.error(error)})
  }











  // getDate(){
  //   var today = new Date();
  //   var dd = String(today.getDate()).padStart(2, '0');
  //   var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
  //   var yyyy = today.getFullYear();

  //   let today2 = mm + '/' + dd + '/' + yyyy;
  //   return today2;
  // }
  tConvert = (time) => {
    // Check correct time format and split into components
    time = time.toString ().match (/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
  
    if (time.length > 1) { // If time format correct
      time = time.slice (1);  // Remove full string match value
      time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
      time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    return time.join (''); // return adjusted time or original string
  }
}
