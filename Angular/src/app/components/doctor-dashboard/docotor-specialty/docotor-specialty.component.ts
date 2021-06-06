import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { IDoctorWithSubSpecialty } from 'src/app/_models/_interfaces/IDoctorWithSubSpecialty';
import { IRegisterDoctor } from 'src/app/_models/_interfaces/IRegisterDoctor';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { ISubSpecialty } from 'src/app/_models/_interfaces/ISubSpeciality';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';



@Component({
  selector: 'app-docotor-specialty',
  templateUrl: './docotor-specialty.component.html',
  styleUrls: ['./docotor-specialty.component.scss']
})
export class DocotorSpecialtyComponent implements OnInit {

  constructor(private _specialtyService:SpecilatyService,private _fb:FormBuilder,
    private _subSpecialtyService:SubSpecialityService,private _doctorService:DoctorService) { }

 
  allSubSpecialtyAccepted:ISubSpecialty[];
  SubSpecialtySelected=new Array<ISubSpecialty>();
  addNewSubSpecialty=new Array<ISubSpecialty>();
  currentDoctor:IRegisterDoctor;

  
  
  ngOnInit(): void {
    this.load();
  }
  asds:ISubSpecialty;
  load(){
    this._doctorService.getCurrentDoctor().subscribe(data=>{
      this.currentDoctor=data;
      this._subSpecialtyService.getAllSubSpecialityBySpecialtyId(data.specialtyId,true).subscribe(data=>
        {this.allSubSpecialtyAccepted=data},error=>console.log(error));
      //to get subspecialty selected in past
      this._doctorService.getCurrentDoctorWithSubSpecialty().subscribe(data=>{
        this.SubSpecialtySelected=data;
      },error=>console.error(error));
      
    },error=>console.error(error));
  }

  

  // onSubSpecialSelect(option){
  //   console.log(option)
  //   let subSpecial = this.allSubSpecialtyAccepted.find(i=>i.id==option);
  //   this.SubSpecialtySelected.push(subSpecial)
  //   console.log(this.SubSpecialtySelected)
  // }
  /***************** */
  

  insertSubSpecialValue:string=""
  add(value){
    this.insertSubSpecialValue='';
    this.addNewSubSpecialty.push({id:0,name:value, byAdmin: true, specialtyId: this.currentDoctor.specialtyId})
  }
  removefromNewSubSpecial(option){
    let subSpecial = this.addNewSubSpecialty.find(i=>i.id==option);
    let elementIndex =this.addNewSubSpecialty.indexOf(subSpecial);
    this.addNewSubSpecialty.splice(elementIndex,1)
  }
  saveDataToDoctor(){

    
   // let newSubSpecialtyAddedToDatabase=null
    if(this.addNewSubSpecialty.length > 0){
      this._subSpecialtyService.addListOfSubSpecialty(this.addNewSubSpecialty).subscribe(
        data=>{
          console.log(data);
          this._doctorService.assignSuubSpecialtyToDoctor(data).subscribe(
            data=>{
              console.log(data);
            },error=>console.error(error)
          )
        },error=>console.error(error)
      )
    }

    // this._doctorService.assignSpecialtyToDoctor(this.specialtySelected).subscribe(
    //   data=>{
    //     console.log(data);
    //   },error=>console.error(error)
    // )
    
    this._doctorService.assignSuubSpecialtyToDoctor(this.SubSpecialtySelected).subscribe(
      data=>{
        console.log(data);
      },error=>console.error(error)
    )
  }
}
