import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { IDoctorAttachment } from 'src/app/_models/_interfaces/IDoctorAttachments';
import { DoctorAttachmentService } from 'src/app/_services/doctor-attachment.service';

@Component({
  selector: 'app-doctor-dashboard-attachment',
  templateUrl: './doctor-dashboard-attachment.component.html',
  styleUrls: ['./doctor-dashboard-attachment.component.scss']
})
export class DoctorDashboardAttachmentComponent implements OnInit {

  constructor(private _fb:FormBuilder,private _doctorAttachmentService:DoctorAttachmentService) { }

  ngOnInit(): void {
  }

  formAttachment=this._fb.group({
    PersonalIdImage:this._fb.group({
      Image:["",Validators.required]
    }),
    DoctorSyndicateIdImage:this._fb.group({
      Image:["",Validators.required]
    }),
    OpenClinicPermissionImage:this._fb.group({
      Image:["",Validators.required]
    })
  });
  // "",[Validators.required]


  
  // (onUploadFinished)="uploadFinished($event)"

  // createImgPath 
  // public uploadFinished = (event) => { 
  //   this.response = event;
  // }

  uploadPersonImage=(event)=>{
    console.error("1")
    this.PersonalIdImage.setValue({
      Image:[event]
    })
    console.log(this.PersonalIdImage.get("Image").value[0].dbPath)
  }
  uploadDoctorSyndicateIdImage=(event)=>{
    console.error("2")
    this.DoctorSyndicateIdImage.setValue({
      Image:[event]
    })
  }
  uploadOpenClinicPermissionImage=(event)=>{
    console.error("3")
    this.OpenClinicPermissionImage.setValue({
      Image:[event]
    })
  }
  
  saveAttachment(){
    let doctorAttach:IDoctorAttachment={
      doctorId:"1",
      personalIdImage:this.PersonalIdImage.get("Image").value[0].dbPath,
      doctorSyndicateIdImage:this.DoctorSyndicateIdImage.get("Image").value[0].dbPath,
      openClinicPermissionImage:this.OpenClinicPermissionImage.get("Image").value[0].dbPath,
    }
    console.log(doctorAttach)
    this._doctorAttachmentService.InsertAttachment(doctorAttach)
      // .pipe(first())
      .subscribe(data=>{console.log(data)},error=>{console.error(error)})
    
  }
  

  
  get PersonalIdImage(){ return this.formAttachment.get("PersonalIdImage"); }
  get DoctorSyndicateIdImage(){ return this.formAttachment.get("DoctorSyndicateIdImage"); }
  get OpenClinicPermissionImage(){ return this.formAttachment.get("OpenClinicPermissionImage"); }
} 
