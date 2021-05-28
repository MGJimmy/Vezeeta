import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { ISubSpecialty } from 'src/app/_models/_interfaces/ISubSpeciality';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-sub-speciality',
  templateUrl: './sub-speciality.component.html',
  styleUrls: ['./sub-speciality.component.scss']
})
export class SubSpecialityComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  subSpecialityList: ISubSpecialty[];
  SpecialityList: ISpecialty[];
  hasSubSpecialities: boolean = false;
  SubSpecialitiesCount: number;
  pageSize: number = 8;
  currentPageNumber: number = 1;
  numberOfPages: number;
  errorMsg: any;
  SubspecialityForm : FormGroup;
  actionName: string;
  loading = false;
  private _SubspecialityToUpdate: ISubSpecialty;
  submitted: boolean=false;
  IsAccept: boolean=false;
  IsNameExist: boolean=false;



  constructor(private subspecialityService: SubSpecialityService,private specilatyService:SpecilatyService,
    private _formBuilder: FormBuilder,private _router:Router) { }

  ngOnInit(): void {
    this.getSubSpecialitiesCount();
    this.getSelectedPage(1);
    this.SubspecialityForm = this._formBuilder.group({
      name:['', Validators.required],
      specialtyId:['',Validators.required]
    });
    this.specilatyService.getAllCategories().subscribe(
      data => {
        this.SpecialityList = data
      },
      error => {
        this.errorMsg = error;
      }
    )
    
    

  }
  get formFields() { return this.SubspecialityForm.controls; }
  // pagination
  counter(i: number) {
    return new Array(i);
  }
  private getSubSpecialitiesCount() {
    this.subspecialityService.getSubSpecialitiesCount().subscribe(
      data => {
        this.SubSpecialitiesCount = data
        this.numberOfPages = Math.ceil(this.SubSpecialitiesCount / this.pageSize)
      },
      error => {
        this.errorMsg = error;
      }
    )
  }
  getSelectedPage(currentPageNumber: number) {
    this.subspecialityService.getSubSpecialitiesByPage(this.pageSize, currentPageNumber).subscribe(
      data => {
        this.subSpecialityList = data;
        console.log(this.subSpecialityList);
        this.currentPageNumber = currentPageNumber;
        if (data.length != 0)
          this.hasSubSpecialities = true;
        else
          this.hasSubSpecialities = false;
      },
      error => {
        this.errorMsg = error;
      }
    )
  }

  openAddSubSpecialtyModal() {
    this.formFields.name.setValue("");
    this.formFields.specialtyId.setValue("");
    this.actionName = "Add";
  }
  openUpdateSubSpecialtyyModal(SubSpecialID) {
    this.actionName = "Update";
    this.subspecialityService.getSubSpecialityById(SubSpecialID)
        .pipe(first())
        .subscribe(
            data => {
                this.SubspecialityForm.setValue({
                  name: data.name,
                  specialtyId:data.specialtyId
                }); 
                this._SubspecialityToUpdate = data;
               
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  onAddOrUpdateSubmit(){
    if(this.actionName == "Add"){
      this.onAddSubSpecialtySubmit();
    }else{
      this.onUpdateSubSpecialtySubmit()
    }
  }

AcceptSubSpecial(SubSpecialID){
    this.subspecialityService.getSubSpecialityById(SubSpecialID)
    .subscribe(
        data => {
            this._SubspecialityToUpdate = data;
            console.log(this._SubspecialityToUpdate);
            this.IsAccept=true;
           this.onUpdateSubSpecialtySubmit();
        },
        error => {
            this.errorMsg = error;
            this.loading = false;
        });
  }
  onUpdateSubSpecialtySubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if(!this.IsAccept){
      if (this.SubspecialityForm.invalid) {
        return;
      }
    
      this._SubspecialityToUpdate.name = this.formFields.name.value;
      this._SubspecialityToUpdate.specialtyId = this.formFields.specialtyId.value;
    
    }
    this.loading = true;
    this._SubspecialityToUpdate.byAdmin =true;
   
  
    this.subspecialityService.updateSubSpecialty(this._SubspecialityToUpdate.id,this._SubspecialityToUpdate)
        //.pipe(first())
        .subscribe(
            () => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
                console.log("updated")
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
                console.log("error")
            });
  }
  onAddSubSpecialtySubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.SubspecialityForm.invalid) {
        return;
      }
      this.loading = true;

    let newSubSpecialty:ISubSpecialty = 
    {
      name : this.formFields.name.value,
      byAdmin:true,
      specialtyId:this.formFields.specialtyId.value,
    };

    this.subspecialityService.addNewSubSpecialty(newSubSpecialty)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
                this.IsNameExist=true;
                console.log(error);
            });

    }

  openDeleteSubSpecialtyModal(SubSpecialtyID) {
    this.confirmModal.pointerToFunction = this.subspecialityService.deleteSubSpecialty;
    this.confirmModal.title = "Delete SubSpecialty";
    this.confirmModal.itemId = SubSpecialtyID;
    this.confirmModal.message = "Are you sure to delete this SubSpecialty";
    this.confirmModal.pageUrl = this._router.url;
  }
  
 

}
