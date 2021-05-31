import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { IDoctorAttachment } from 'src/app/_models/_interfaces/IDoctorAttachments';
import { DoctorAttachmentService } from 'src/app/_services/doctor-attachment.service';
import { environment } from 'src/environments/environment';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-doctor-attachments',
  templateUrl: './doctor-attachments.component.html',
  styleUrls: ['./doctor-attachments.component.scss']
})
export class DoctorAttachmentsComponent implements OnInit {
  
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasDoctorAttachments:boolean = false;
  errorMsg:string;
  loading = false;
  actionName:string;
  doctorAttachmentsCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; 
  isAccepted:boolean = false;
  currtentImageUrlToPreview:string;
  public response: {dbPath: ''};

  _doctorAttachments:IDoctorAttachment[];
  constructor( private _doctorAttachmentService:DoctorAttachmentService, private _router:Router) { }

  ngOnInit(): void {
    this.getDoctorAttachmentCount();
    
    this.getSelectedPage(this.currentPageNumber);
  }
   getDoctorAttachmentCount(){
    this._doctorAttachmentService.getDoctorAttachmentCount().subscribe(
      data => {
        this.doctorAttachmentsCount = data
        this.numberOfPages = Math.ceil(this.doctorAttachmentsCount / this.pageSize)
        
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  getSelectedPage(currentPageNumber:number){
    this._doctorAttachmentService.getDoctorAttachmentByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this._doctorAttachments = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasDoctorAttachments = true;
        else
          this.hasDoctorAttachments = false;

      },
      error=>
      {
        this.errorMsg=error;
      }
    ) 
  }
  public createImgPath = (serverPath: string) => {
    console.log(`${environment.apiUrl}/${serverPath}`);
    return `${environment.apiUrl}/${serverPath}`;
  }
  counter(i: number) {
    return new Array(i);
  }
  StatusChanged(selectedStatus:string){
    //  if(selectedStatus =="Accepted")
    //    this.isAccepted = true;
    //  else if(selectedStatus=="NotAccepted")
    //    this.isAccepted = false;
    // this.getDoctorAttachmentCount(this.isAccepted);
    // this.getSelectedPage(this.currentPageNumber,this.isAccepted);

  }
  openImage(imgSrc:string){
    // console.log(document.getElementById('imagepreview'))
    this.currtentImageUrlToPreview=imgSrc;
    
  }
  openAcceptAttachmentModal(doctorID:string){
    console.log(doctorID);
    this.confirmModal.pointerToFunction = this._doctorAttachmentService.acceptDoctorAttachment
    this.confirmModal.title = "Delete Category";
    this.confirmModal.itemId = doctorID;
    this.confirmModal.message = "Are you sure to accept these attachments";
    this.confirmModal.pageUrl = this._router.url;
  }

}
