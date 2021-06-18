import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { feelimit, _IdoctorFilter } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { ISubSpecialty } from 'src/app/_models/_interfaces/ISubSpeciality';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';

@Component({
  selector: 'app-filter-doctorside-bar',
  templateUrl: './filter-doctorside-bar.component.html',
  styleUrls: ['./filter-doctorside-bar.component.scss']
})
export class FilterDoctorsideBarComponent implements OnInit {

  
  SpecailtyId: any=null;
  doctorfilter: _IdoctorFilter = {
    specailtyid:1,
    title: [],
    fee: [],
    subspecails:[],
    name:"",
    areaId:null,
    cityId:null
  };
  allSubSpecialty: ISubSpecialty[];
  titlesDegrees=["professor","teacher","consultative","specialist"];

/********************************** */

  constructor(private activeRoute: ActivatedRoute, private _router: Router,
    private _dataSharedService: DataSharedService,
    private _doctorService: DoctorService, 
    private _subSpecialityService: SubSpecialityService) {
      
      _dataSharedService.sendSpecialtyIdToSideBarComponent.subscribe(data=>{
        if(data!=0){
          this.SpecailtyId=data;
          if(data==null){
            this.allSubSpecialty=null;
          }else{
            this._subSpecialityService.getAllSubSpecialityBySpecialtyId(this.SpecailtyId, true).subscribe(data => {
              this.allSubSpecialty= data;
              
            })
          }
          
        }
      })
    }

  ngOnInit(): void {

    
    
  }

  LoadData(){
    // this._doctorService.ShowSpecailtyDoctorswithFilter(this.doctorfilter).subscribe(data => {
    //   console.log(data);
    //   this.DoctorsList = data;
    //   this.specialtyName = data[0].specialty.name;
    //   this.NumberOfDoctors = data.length;
    //   this.DoctorsList.forEach(element => {
    //     element.presentDaysWork = this.chunks(this.loadDays(element.workingDays), 3);

    //   });
    //   this.oldlist = this.DoctorsList;

    // }, err => {
    //   console.log("Error");
    // })
    
    this._dataSharedService.sendDataToSearchComponent.next(this.doctorfilter);

  }


  titlecheck(event){

    if (event.target.checked) {
      this.doctorfilter.title.push(event.target.value);
    }
    else {
      this.doctorfilter.title = this.doctorfilter.title.filter(e => e !== event.target.value);
    }
    this.LoadData();
  }
  moneycheck(event) {
    if (event.target.value == 1) {
      let feeLimt:feelimit={
        MiniMoney:0,
        MaxMoney:50
      }
      if (event.target.checked) {
        this.doctorfilter.fee.push(feeLimt);
      }
      else {
        // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
        this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
      }
    }

    else if (event.target.value == 2) {

      let feeLimt:feelimit={
        MiniMoney:50,
        MaxMoney:100
      }
      if (event.target.checked) {
        this.doctorfilter.fee.push(feeLimt);
      }
      else {
        // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
        this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
      }
    }

    else if (event.target.value == 3) {
      let feeLimt:feelimit={
        MiniMoney:100,
        MaxMoney:200
      }
      if (event.target.checked) {
        this.doctorfilter.fee.push(feeLimt);
      }
      else {
        // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
        this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
      }
    }

    else if (event.target.value == 4) {
      let feeLimt:feelimit={
        MiniMoney:200,
        MaxMoney:300
      }
      if (event.target.checked) {
        this.doctorfilter.fee.push(feeLimt);
      }
      else {
        // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
        this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
      }
    }
    else if (event.target.value == 5) {
      let feeLimt:feelimit={
        MiniMoney:300,
        MaxMoney:1000
      }
      if (event.target.checked) {
        this.doctorfilter.fee.push(feeLimt);
      }
      else {
        // this.doctorfilter.fee = this.doctorfilter.fee.filter(e => e !== feeLimt);
        this.Removefee(feeLimt.MiniMoney,feeLimt.MaxMoney);
      }
    }
    this.LoadData();

  }

  subSpecailtycheck(event)
  {
    if (event.target.checked) {
      this.doctorfilter.subspecails.push(event.target.value);
    }
    else{
      this.doctorfilter.subspecails = this.doctorfilter.subspecails.filter(e => e !== event.target.value);
    }
    this.LoadData();

  }
  
  Removefee(min: number,max:number) {
    this.doctorfilter.fee.forEach((value,index)=>{
        if(value.MiniMoney==min,value.MaxMoney==max) this.doctorfilter.fee.splice(index,1);
    });
  }
}
