import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IClinicServices } from 'src/app/_models/_interfaces/IClinicService';
import { ClincServicesService } from 'src/app/_services/clinc-services.service';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-clinic-services',
  templateUrl: './clinic-services.component.html',
  styleUrls: ['./clinic-services.component.scss']
})
export class ClinicServicesComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasClinicServices:boolean = false;
  private _clinicServiceToUpdate:IClinicServices;
  allClinicServices:IClinicServices[]; 
  errorMsg:string;
  clinicServiceForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  CliniceServicesCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; 
  public response: {dbPath: ''};
  
  get formFields() { return this.clinicServiceForm.controls; }

  constructor(private _cliincServicesServie : ClincServicesService , 
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  
  ngOnInit(): void {
    this.getSpecialitiesCount();
    this.clinicServiceForm = this._formBuilder.group({
      name:['', Validators.required]
    });
    this.getSelectedPage(1);
  }

  private getSpecialitiesCount(){
    this._cliincServicesServie.getClnicServicesCount().subscribe(
      data => {
        this.CliniceServicesCount = data;
        this.numberOfPages = Math.ceil(this.CliniceServicesCount / this.pageSize);    
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }

  private onAddClinicServiceSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.clinicServiceForm.invalid) {
        return;
      }

    this.loading = true;
    let newCliicService:IClinicServices = 
    {
      id:0 ,
      name : this.formFields.name.value,
     
    };
    this._cliincServicesServie.addNewClinicService(newCliicService)
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
                console.log(error)
            });
  }

  private onUpdateClincServicesSubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.clinicServiceForm.invalid) {
        return;
      }

    this.loading = true;
    this._clinicServiceToUpdate.name = this.formFields.name.value;
  
  
    this._cliincServicesServie.updateClinicService(this._clinicServiceToUpdate.id, this._clinicServiceToUpdate)
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
      this.onAddClinicServiceSubmit();
    }else{
      this.onUpdateClincServicesSubmit()
    }
  }

  openAddClinicServicesModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateClinicServicesModal(clinicServiceID){
    this.actionName = "Update";
    this._cliincServicesServie.getClinicServicesById(clinicServiceID)
        .pipe(first())
        .subscribe(
            data => {
                this.clinicServiceForm.setValue({
                  name: data.name
                }); 
                this._clinicServiceToUpdate = data;
               
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }

  openDeleteClinicServiceModal(ClincSericeID){
    this.confirmModal.pointerToFunction = this._cliincServicesServie.deleteClinicService
    this.confirmModal.title = "Delete This Services";
    this.confirmModal.itemId = ClincSericeID;
    this.confirmModal.message = "Are you sure to delete this Services";
    this.confirmModal.pageUrl = this._router.url;
   
  }

  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._cliincServicesServie.getClinicServicesByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allClinicServices = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasClinicServices = true;
        else
          this.hasClinicServices = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }


}
