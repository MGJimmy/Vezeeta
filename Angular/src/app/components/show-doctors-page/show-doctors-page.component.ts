import { DatePipe } from '@angular/common';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Days, feelimit, IDoctor, IdoctorDayWork, _DayShiftsforDoctor, _IdoctorFilter, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { ISubSpecialty } from 'src/app/_models/_interfaces/ISubSpeciality';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';

@Component({
  selector: 'app-show-doctors-page',
  templateUrl: './show-doctors-page.component.html',
  styleUrls: ['./show-doctors-page.component.scss'],
  providers: [DatePipe]
})
export class ShowDoctorsPageComponent implements OnInit {
  



  constructor(private activeRoute: ActivatedRoute, private _router: Router,
    private _dataSharedService: DataSharedService, private fb: FormBuilder,
    private _doctorService: DoctorService, private datePipe: DatePipe,
    private _subSpecialityService: SubSpecialityService) {
    // this.activeRoute.params.subscribe(params =>
    //   this.SpecailtyId = params['id']
    // );
    // this.doctorfilter.specailtyid=this.SpecailtyId;
  }

  ngOnInit(): void {
   
    //this.LoadData();
    // this._doctorService.ShowSpecailtyDoctorswithFilter(this.doctorfilter).subscribe(data=>{
    //   console.log(data);
    // })


    // this._subSpecialityService.getAllSubSpecialityBySpecialtyId(this.SpecailtyId, true).subscribe(data => {
    //   this.allSubSpecialty= data;
    //   console.log(this.allSubSpecialty);
    // })

  }

  

 

// titlecheck(event){

//   if (event.target.checked) {
//     this.doctorfilter.title.push(event.target.value);
//   }
//   else {
//     this.doctorfilter.title = this.doctorfilter.title.filter(e => e !== event.target.value);
//   }
//   this.LoadData();
// }


  // moneycheck(event) {
  //   console.log(event.target.value);
  //   if (event.target.value == 1) {
  //     let feeLimt:feelimit={
  //       MiniMoney:0,
  //       MaxMoney:50
  //     }
  //     if (event.target.checked) {
  //       this.doctorfilter.fee.push(feeLimt);
  //     }
  //     else {
  //       // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
  //       this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
  //     }
  //   }

  //   else if (event.target.value == 2) {

  //     let feeLimt:feelimit={
  //       MiniMoney:50,
  //       MaxMoney:100
  //     }
  //     if (event.target.checked) {
  //       this.doctorfilter.fee.push(feeLimt);
  //     }
  //     else {
  //       // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
  //       this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
  //     }
  //   }

  //   else if (event.target.value == 3) {
  //     let feeLimt:feelimit={
  //       MiniMoney:100,
  //       MaxMoney:200
  //     }
  //     if (event.target.checked) {
  //       this.doctorfilter.fee.push(feeLimt);
  //     }
  //     else {
  //       // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
  //       this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
  //     }
  //   }

  //   else if (event.target.value == 4) {
  //     let feeLimt:feelimit={
  //       MiniMoney:200,
  //       MaxMoney:300
  //     }
  //     if (event.target.checked) {
  //       this.doctorfilter.fee.push(feeLimt);
  //     }
  //     else {
  //       // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
  //       this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
  //     }
  //   }
  //   else if (event.target.value == 5) {
  //     let feeLimt:feelimit={
  //       MiniMoney:300,
  //       MaxMoney:1000
  //     }
  //     if (event.target.checked) {
  //       this.doctorfilter.fee.push(feeLimt);
  //     }
  //     else {
  //       // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
  //       this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
  //     }
  //   }

  //   console.log(this.doctorfilter);
  //   this.LoadData();

  // }

  // subSpecailtycheck(event)
  // {
  //   if (event.target.checked) {
  //     this.doctorfilter.subspecails.push(event.target.value);
  //   }
  //   else{
  //     this.doctorfilter.subspecails = this.doctorfilter.subspecails.filter(e => e !== event.target.value);
  //   }
  //   this.LoadData();

  // }



  // Removefee(min: number,max:number) {
  //   this.doctorfilter.fee.forEach((value,index)=>{
  //       if(value.MiniMoney==min,value.MaxMoney==max) this.doctorfilter.fee.splice(index,1);
  //   });
  // } 


}
