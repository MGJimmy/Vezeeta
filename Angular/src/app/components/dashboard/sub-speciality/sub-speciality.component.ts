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
  @ViewChild(ConfirmModalComponent) confirmModal: ConfirmModalComponent;
  @ViewChild('modalConfirmCloseBtn')modalConfirmCloseBtn;
  subSpecialityList: ISubSpecialty[];
  SpecialityList: ISpecialty[];
  hasSubSpecialities: boolean = false;
  SubSpecialitiesCount: number;

  errorMsg: any;
  SubspecialityForm: FormGroup;
  actionName: string;
  loading = false;
  private _SubspecialityToUpdate: ISubSpecialty;
  submitted: boolean = false;
  IsAccept: boolean = false;
  IsNameExist: boolean = false;

  pageSize: number = 8;
  currentPageNumber: number = 1;
  numberOfPages: number;
  countOfSubSpecail: number;

  constructor(private subspecialityService: SubSpecialityService, private specilatyService: SpecilatyService,
    private _formBuilder: FormBuilder, private _router: Router) { }

  ngOnInit(): void {
    
    this.GetNumberOfPageOfAcceptedSubSpecailiztion();
    this.GetPageingOfAcceptedSubSpecailiztion(this.currentPageNumber);

    this.SubspecialityForm = this._formBuilder.group({
      name: ['', Validators.required],
      specialtyId: ['', Validators.required]
    });
    this.specilatyService.getAllSpecialities().subscribe(
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
            specialtyId: data.specialtyId
          });
          this._SubspecialityToUpdate = data;

        },
        error => {
          this.errorMsg = error;
        });
  }
  onAddOrUpdateSubmit() {
    if (this.actionName == "Add") {
      this.onAddSubSpecialtySubmit();
    } else {
      this.onUpdateSubSpecialtySubmit()
    }
  }

  AcceptSubSpecial(SubSpecialID) {
    this.subspecialityService.getSubSpecialityById(SubSpecialID)
      .subscribe(
        data => {
          this._SubspecialityToUpdate = data;
          console.log(this._SubspecialityToUpdate);
          this.IsAccept = true;
          this.onUpdateSubSpecialtySubmit();
        },
        error => {
          this.errorMsg = error;
        });
  }

  onUpdateSubSpecialtySubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (!this.IsAccept) {
      if (this.SubspecialityForm.invalid) {
        return;
      }

      this._SubspecialityToUpdate.name = this.formFields.name.value;
      this._SubspecialityToUpdate.specialtyId = this.formFields.specialtyId.value;

    }
    // this.loading = true;
    this._SubspecialityToUpdate.byAdmin = true;


    this.subspecialityService.updateSubSpecialty(this._SubspecialityToUpdate.id, this._SubspecialityToUpdate)
      //.pipe(first())
      .subscribe(
        () => {
          if (!this.acceptedSubSpecailiztion) {
            if (--this.countOfSubSpecail % this.pageSize == 0) {
              this.GetNumberOfPageOfNotAcceptedSubSpecailiztion();
              if (this.numberOfPages == this.currentPageNumber)
                this.currentPageNumber--;
            }
          }
          this.IsAccept=false;
          this.pageChange(this.currentPageNumber);
          this.addOrUpdateModelCloseBtn.nativeElement.click();
          
        },
        error => {
          this.errorMsg = error;
          // this.loading = false;
          console.log("error")
        });
  }

onAddSubSpecialtySubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.SubspecialityForm.invalid) {
      return;
    }
    // this.loading = true;

    let newSubSpecialty: ISubSpecialty =
    {
      name: this.formFields.name.value,
      specialtyId: this.formFields.specialtyId.value,
    };

    this.subspecialityService.addNewSubSpecialty(newSubSpecialty)
      .pipe(first())
      .subscribe(
        data => {
          if (!this.acceptedSubSpecailiztion) {                                     //this.acceptedArea
            if (this.countOfSubSpecail++ % this.pageSize == 0) {
              this.GetNumberOfPageOfAcceptedSubSpecailiztion();                   //GetNumberOfPageOfAcceptedArea
              if (this.numberOfPages == this.currentPageNumber)
                this.currentPageNumber++;
            }
          }
          this.pageChange(this.currentPageNumber);
          this.addOrUpdateModelCloseBtn.nativeElement.click();
        
        },
        error => {
          this.errorMsg = error;
          // this.loading = false;
          this.IsNameExist = true;
          console.log(error);
        });

  }
  deleteSubspecailId:number;
  openConfirmDelete(id){
    this.deleteSubspecailId=id;
  }
  closeConfirmDelete(){
    this.deleteSubspecailId=null;
  }
  confirmDelete(){  
    this.subspecialityService.deleteSubSpecialty(this.deleteSubspecailId).subscribe(()=>{
      if(--this.countOfSubSpecail %this.pageSize ==0){
        if(this.numberOfPages == this.currentPageNumber)
          this.currentPageNumber--;
        this.numberOfPages=Math.ceil(this.countOfSubSpecail / this.pageSize)
      }
      this.pageChange(this.currentPageNumber);
      this.modalConfirmCloseBtn.nativeElement.click();
    })
  }

  acceptedSubSpecailiztion: boolean = true;

  pageChange(pageNumber) {
    if (this.acceptedSubSpecailiztion)
      this.GetPageingOfAcceptedSubSpecailiztion(pageNumber);
    else
      this.GetPageingOfNotAcceptedSubSpecailiztion(pageNumber);
  }

  GetNumberOfPageOfAcceptedSubSpecailiztion() {
    this.subspecialityService.countOfAcceptSubspecialization().subscribe(data => {
      this.countOfSubSpecail = data,
        this.numberOfPages = Math.ceil(this.countOfSubSpecail / this.pageSize)
    });
  }
  GetNumberOfPageOfNotAcceptedSubSpecailiztion() {
    this.subspecialityService.countOfNotAcceptSubspecialization().subscribe(data => {
      this.countOfSubSpecail = data,
        this.numberOfPages = Math.ceil(this.countOfSubSpecail / this.pageSize)
    });
  }
  GetPageingOfAcceptedSubSpecailiztion(currentPage: number) {
    this.subspecialityService.getAllByPage(this.pageSize, currentPage).subscribe(data => {
      console.log(data);
      this.subSpecialityList = data;
      this.currentPageNumber = currentPage;
      if (data.length != 0)
        this.hasSubSpecialities = true;
      else
        this.hasSubSpecialities = false;
    })
  }
  GetPageingOfNotAcceptedSubSpecailiztion(currentPage: number) {
    this.subspecialityService.getAllByPageOfNotAccepted(this.pageSize, currentPage).subscribe(data => {
      console.log(data);
      this.subSpecialityList = data;
      this.currentPageNumber = currentPage;
      if (data.length != 0)
        this.hasSubSpecialities = true;
      else
        this.hasSubSpecialities = false;
    })
  }
  //acceptOr not
  switchState(show) {
    if (show == "accept") {
      this.acceptedSubSpecailiztion = true
      this.GetNumberOfPageOfAcceptedSubSpecailiztion()
      this.GetPageingOfAcceptedSubSpecailiztion(1)
    } else {
      this.acceptedSubSpecailiztion = false
      this.GetNumberOfPageOfNotAcceptedSubSpecailiztion()
      this.GetPageingOfNotAcceptedSubSpecailiztion(1)
    }
  }

}
