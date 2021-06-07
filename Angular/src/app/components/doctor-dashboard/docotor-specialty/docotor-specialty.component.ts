import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { IDoctorWithSubSpecialty } from 'src/app/_models/_interfaces/IDoctorWithSubSpecialty';
import { IRegisterDoctor } from 'src/app/_models/_interfaces/IRegisterDoctor';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { ISubSpecialty } from 'src/app/_models/_interfaces/ISubSpeciality';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorSubspecialtyService } from 'src/app/_services/doctor-subspecialty.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';



@Component({
  selector: 'app-docotor-specialty',
  templateUrl: './docotor-specialty.component.html',
  styleUrls: ['./docotor-specialty.component.scss']
})
export class DocotorSpecialtyComponent implements OnInit {

  constructor(private _specialtyService:SpecilatyService,private _fb:FormBuilder,private _router:Router
    ,private _subSpecialtyService:SubSpecialityService,private _doctorService:DoctorService,
    private _sharedService:DataSharedService,private _doctorSubspecialty:DoctorSubspecialtyService) { }

 
  allSubSpecialtyAccepted:ISubSpecialty[];
  SubSpecialtySelected=new Array<ISubSpecialty>();
  NotAcceptByAdminSubSpecialtySelected=new Array<ISubSpecialty>();
  addNewSubSpecialty=new Array<ISubSpecialty>();
  currentDoctor:IRegisterDoctor;
  insertSubSpecialInputValue:string=""

  
  
  ngOnInit(): void {
    this.load();
  }
  state="insert";
  load(){
    this._doctorService.getCurrentDoctor().subscribe(data=>{
      this.currentDoctor=data;
      this._subSpecialtyService.getAllSubSpecialityBySpecialtyId(data.specialtyId,true).subscribe(data=>
        {this.allSubSpecialtyAccepted=data},error=>console.log(error));

      //to get subspecialty selected in past
      this._doctorSubspecialty.getDoctorSubSpecialty().subscribe(data=>{
        if(data.length !=0){
          this.SubSpecialtySelected=data.filter(i=>i.byAdmin==true);
          this.NotAcceptByAdminSubSpecialtySelected=data.filter(i=>i.byAdmin==false);
          this.state="update"
        }
      },error=>console.error(error));
    },error=>console.error(error));
  }


  add(value){
    this.insertSubSpecialInputValue='';
    this.addNewSubSpecialty.push({id:0,name:value, byAdmin: true, specialtyId: this.currentDoctor.specialtyId})
  }
  removefromNewSubSpecial(option){
    let subSpecial = this.addNewSubSpecialty.find(i=>i.id==option);
    let elementIndex =this.addNewSubSpecialty.indexOf(subSpecial);
    this.addNewSubSpecialty.splice(elementIndex,1)
  }
  
  saveDataToDoctor(){
    this.SubSpecialtySelected.push.apply(this.SubSpecialtySelected,this.NotAcceptByAdminSubSpecialtySelected);
          
    if(this.addNewSubSpecialty.length > 0){
      this._subSpecialtyService.addListOfSubSpecialty(this.addNewSubSpecialty).subscribe(
        data=>{
          this.SubSpecialtySelected.push.apply(this.SubSpecialtySelected,data);
          this.insertRelatedToState();
        },error=>console.error(error)
      )
    }else{
      this.insertRelatedToState();
    }

  }
  insertRelatedToState(){
    if(this.state=="insert"){
      this._doctorSubspecialty.insertDoctorSuubSpecialty(this.SubSpecialtySelected).subscribe(
        data=>{
        },error=>console.error(error)
      )
    }else{
      this._doctorSubspecialty.updateDoctorSuubSpecialty(this.SubSpecialtySelected).subscribe(
        data=>{
        },error=>console.error(error)
      )
    }
    this._router.navigate(["/doctorDashboard/attachment"]).then(()=>this._sharedService.IsDoctorSideBarChange.next(true))
  }
}
