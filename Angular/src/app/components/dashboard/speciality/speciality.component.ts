import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
import { environment } from 'src/environments/environment';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-speciality',
  templateUrl: './speciality.component.html',
  styleUrls: ['./speciality.component.scss']
})
export class SpecialityComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasSpecialities:boolean = false;
  private _specialityToUpdate:ISpecialty;
  allSpecialities:ISpecialty[]; 
  errorMsg:string;
  specialityForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  SpecialitiesCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; 
  public response = {dbPath: ''};

  // convenience getter for easy access to form fields
  get formFields() { return this.specialityForm.controls; }
  constructor(private _specialityService:SpecilatyService,
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  ngOnInit(): void {
    this.getSpecialitiesCount();
    this.specialityForm = this._formBuilder.group({
      name:['', Validators.required]
    });
    this.getSelectedPage(1);
  }

  private getSpecialitiesCount(){
    this._specialityService.getSpecialitiesCount().subscribe(
      data => {
        this.SpecialitiesCount = data
        this.numberOfPages = Math.ceil(this.SpecialitiesCount / this.pageSize)
        
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  private onAddSpecialtySubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.specialityForm.invalid) {
        return;
      }

    this.loading = true;
    let newSpecialty:ISpecialty = 
    {
      id:0 ,
      name : this.formFields.name.value,
      image: this.response.dbPath
     
    };
    this._specialityService.addNewSpecialty(newSpecialty)
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

  private onUpdateSpecialtySubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.specialityForm.invalid) {
        return;
      }

    this.loading = true;
    this._specialityToUpdate.name = this.formFields.name.value;
    if(this.response.dbPath != ''){ // if the user doesn't change the image 
    this._specialityToUpdate.image = this.response.dbPath;
  }
  console.log(this._specialityToUpdate)
  
    this._specialityService.updateSpecialty(this._specialityToUpdate.id, this._specialityToUpdate)
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
      this.onAddSpecialtySubmit();
    }else{
      this.onUpdateSpecialtySubmit()
    }
  }
  
  openAddSpecialtyModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateSpecialtyyModal(SpecialtyID){
    this.actionName = "Update";
    this._specialityService.getSpecialityById(SpecialtyID)
        .pipe(first())
        .subscribe(
            data => {
                this.specialityForm.setValue({
                  name: data.name
                }); 
                this._specialityToUpdate = data;
               
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteSpecialtyModal(SpecialtyID){
    //this._categoryToDeleteId = categoryId;
    this.confirmModal.pointerToFunction = this._specialityService.deleteSpecialty
    this.confirmModal.title = "Delete Specialty";
    this.confirmModal.itemId = SpecialtyID;
    this.confirmModal.message = "Are you sure to delete this Specialty";
    this.confirmModal.pageUrl = this._router.url;
    //this.confirmModal.entityName ="category";
  }

// pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._specialityService.getSpecialitiesByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allSpecialities = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasSpecialities = true;
        else
          this.hasSpecialities = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }

  // upload image
  public uploadFinished = (event) => { 
    this.response = event;
  }
  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }
}
