import { Identifiers } from '@angular/compiler';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IDoctorService } from 'src/app/_models/_interfaces/IDoctorService';
import { DoctorServicesService } from 'src/app/_services/doctor-services.service';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-manage-doctor-services',
  templateUrl: './manage-doctor-services.component.html',
  styleUrls: ['./manage-doctor-services.component.scss']
})
export class ManageDoctorServicesComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasDoctorServices:boolean = false;
  private _DoctorServiceToUpdate:IDoctorService;
  allDoctorServices:IDoctorService[]; 
  errorMsg:string;
  DoctorServiceForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  DoctorServicesCount:number;
  byAdmin:boolean=true;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; 
  public response: {dbPath: ''};

  // convenience getter for easy access to form fields
  get formFields() { return this.DoctorServiceForm.controls; }
  constructor(private _DoctorServicesService:DoctorServicesService,
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  ngOnInit(): void {
    this.getDoctorServicesCount(this.byAdmin);
    this.DoctorServiceForm = this._formBuilder.group({
      name:['', Validators.required],
   
    });
    this.getSelectedPage(1,this.byAdmin);
  }

  private getDoctorServicesCount(byAdmin:boolean){
    this._DoctorServicesService.getDoctorServicesCount(byAdmin).subscribe(
      data => {
        this.DoctorServicesCount = data
        this.numberOfPages = Math.ceil(this.DoctorServicesCount / this.pageSize)
        
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  private onAddDoctorServiceSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.DoctorServiceForm.invalid) {
        return;
      }

    this.loading = true;
    let newDoctorService:IDoctorService = 
    {
      id:0 ,
      name : this.formFields.name.value,
      byAdmin:true

    };
    this._DoctorServicesService.addNewDoctorService(newDoctorService)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
                console.log("added")
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }

  private onUpdateDoctorServiceSubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.DoctorServiceForm.invalid) {
        return;
      }

    this.loading = true;
    this._DoctorServiceToUpdate.name = this.formFields.name.value;
  
  
    this._DoctorServicesService.updateDoctorService( this._DoctorServiceToUpdate)
        // .pipe(first())
        .subscribe(
            () => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
                console.log("updated");
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }

  onAddOrUpdateSubmit(){
    if(this.actionName == "Add"){
      this.onAddDoctorServiceSubmit();
    }else{
      this.onUpdateDoctorServiceSubmit()
    }
  }
  
  openAddDoctorServiceModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateDoctorServiceModal(DoctorServiceid){
    this.actionName = "Update";
    this._DoctorServicesService.getDoctorServiceById(DoctorServiceid)
        .pipe(first())
        .subscribe(
            data => {
              console.log(data);
                this.DoctorServiceForm.setValue({
                  name: data.name
                }); 
                this._DoctorServiceToUpdate = data;
               
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteDoctorServiceModal(DoctorServiceid){
  
    this.confirmModal.pointerToFunction = this._DoctorServicesService.deleteDoctorService
    this.confirmModal.title = "Delete Doctor Service";
    this.confirmModal.itemId = DoctorServiceid;
    this.confirmModal.message = "Are you sure to delete this Service";
    this.confirmModal.pageUrl = this._router.url;
  
  }

// pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number,byAdmin:boolean){
    this._DoctorServicesService.getDoctorServicesByPage(this.pageSize,currentPageNumber,byAdmin).subscribe(
      data => {
        this.allDoctorServices = data
        this.currentPageNumber = currentPageNumber;
        console.log(this.allDoctorServices)
        if(data.length != 0)
          this.hasDoctorServices = true;
        else
          this.hasDoctorServices = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  StatusChanged(filterBy:string){
     if(filterBy==="ByAdmin")
       this.byAdmin=true;
     else
      this.byAdmin=false;

      this.getDoctorServicesCount(this.byAdmin);
      this.getSelectedPage(this.currentPageNumber,this.byAdmin);
  }
  openAcceptDoctorServicetModal(id:string){
   
    this.confirmModal.pointerToFunction = this._DoctorServicesService.acceptDoctorService
    this.confirmModal.title = "Confirmation";
    this.confirmModal.itemId = id;
    this.confirmModal.message = "Are you sure to accept this Doctor Service";
    this.confirmModal.pageUrl = this._router.url;
  }
  openRejectDoctorServiceModal(id:string){
   
    this.confirmModal.pointerToFunction = this._DoctorServicesService.rejectDoctorService
    this.confirmModal.title = "Confirmation";
    this.confirmModal.itemId = id;
    this.confirmModal.message = "Are you sure to reject this Doctor Service";
    this.confirmModal.pageUrl = this._router.url;
  }

}
