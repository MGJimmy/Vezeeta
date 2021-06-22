import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { error } from 'selenium-webdriver';
import { IDoctorAttachment, IDoctorAttachmentGetOne } from 'src/app/_models/_interfaces/IDoctorAttachments';
import { DoctorAttachmentService } from 'src/app/_services/doctor-attachment.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-doctor-dashboard-attachment',
  templateUrl: './doctor-dashboard-attachment.component.html',
  styleUrls: ['./doctor-dashboard-attachment.component.scss']
})
export class DoctorDashboardAttachmentComponent implements OnInit {


  constructor(private _fb:FormBuilder,private _doctorAttachmentService:DoctorAttachmentService) { }

  isBinding:boolean=false;
  isAccepted:boolean=false;
  isRejected:boolean=false;
  doctorEnterAttachmentInPast:boolean=false;
  doctorAttachmentData:IDoctorAttachmentGetOne=null;

  ngOnInit(): void {
    this.load();
  }
  load(){
    this._doctorAttachmentService.getById().pipe(first())
    .subscribe(data=>{
      if(data != null){
        this.isBinding=data.isBinding;
        this.isAccepted=data.doctorIsAccepted;
        this.isRejected=data.rejected;
        this.doctorEnterAttachmentInPast=true;
        this.setImagePath(data);
      }else{
        this.doctorEnterAttachmentInPast=false;
      }
    },error=>{
      console.log(error);
    })
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
  
  uploadPersonImage=(event)=>{
    this.PersonalIdImage.setValue({
      Image:[event]
    })
  }
  uploadDoctorSyndicateIdImage=(event)=>{
    this.DoctorSyndicateIdImage.setValue({
      Image:[event]
    })
  }
  uploadOpenClinicPermissionImage=(event)=>{
    this.OpenClinicPermissionImage.setValue({
      Image:[event]
    })
  }
  
  saveAttachment(){
    let doctorAttach:IDoctorAttachment={
      doctorId:"1",
      PersonalIdImage:this.PersonalIdImage.get("Image").value[0].dbPath,
      DoctorSyndicateIdImage:this.DoctorSyndicateIdImage.get("Image").value[0].dbPath,
      OpenClinicPermissionImage:this.OpenClinicPermissionImage.get("Image").value[0].dbPath,
      isBinding:true
    }
    this._doctorAttachmentService.InsertAttachment(doctorAttach)
      .pipe(first())
      .subscribe(data=>{
        console.log("docotor added")
        this.load();
      },error=>{console.error(error)})
    
  }
  UpdateAttachment(){
    let doctorAttach:IDoctorAttachment={
      doctorId:this.doctorAttachmentData.doctorId,
      PersonalIdImage:this.PersonalIdImage.get("Image").value[0].dbPath,
      DoctorSyndicateIdImage:this.DoctorSyndicateIdImage.get("Image").value[0].dbPath,
      OpenClinicPermissionImage:this.OpenClinicPermissionImage.get("Image").value[0].dbPath,
      isBinding:true
    }
    this._doctorAttachmentService.UpdateAttachment(doctorAttach).pipe(first())
    .subscribe(data=>{
      console.log("docotor updated")
      this.load();
    },error=>{console.error(error)})
  
  }
  

  
  get PersonalIdImage(){ return this.formAttachment.get("PersonalIdImage"); }
  get DoctorSyndicateIdImage(){ return this.formAttachment.get("DoctorSyndicateIdImage"); }
  get OpenClinicPermissionImage(){ return this.formAttachment.get("OpenClinicPermissionImage"); }
  setImagePath(data){   
    this.doctorAttachmentData=data; 
    this.doctorAttachmentData.personalIdImage=`http://localhost:57320/${this.doctorAttachmentData.personalIdImage}`
    this.doctorAttachmentData.doctorSyndicateIdImage=`http://localhost:57320/${this.doctorAttachmentData.doctorSyndicateIdImage}`
    this.doctorAttachmentData.openClinicPermissionImage=`http://localhost:57320/${this.doctorAttachmentData.openClinicPermissionImage}`
    
  }
} 
