import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { IUserForReservation } from 'src/app/_models/_interfaces/IUserForReservation';
import { first } from 'rxjs/operators';
import { IUpdateUser } from 'src/app/_models/_interfaces/IUpdateUser';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { SpecilatyService } from 'src/app/_services/specilaty.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent implements OnInit {

  updateUserForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  user: IUpdateUser;
  returnUrl: string;
  TitlesDegree = ["professor", "teacher", "consultative", "specialist"];
  allSpecialty: ISpecialty[];



  constructor(
    private formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _authService: AuthenticationService,
    private _specialtyService: SpecilatyService
  ) { }

  ngOnInit(): void {
    this._specialtyService.getAllSpecialities().subscribe(data => {
      this.allSpecialty = data;
    }, error => console.error(error));


    this._authService.getCurrentUserForUpdate().subscribe(
      (data) => {
        console.log("User is :");
        console.log(data);
        this.user = data;
        if (!this.user.isDoctor) {
          // patient
          this.updateUserForm = this.formBuilder.group({
            fullName: [this.user.fullName, Validators.required],
            email: [this.user.email, Validators.required],
            phoneNumber: [this.user.phoneNumber, Validators.required],
          });
        } else {
          //doctor
          this.updateUserForm = this.formBuilder.group({
            fullName: [this.user.fullName, Validators.required],
            email: [this.user.email, Validators.required],
            phoneNumber: [this.user.phoneNumber, Validators.required],
            titleDegree: [this.user.titleDgree, Validators.required],
            specialtyId: [this.user.specialtyId, Validators.required],
            doctorInfo: [this.user.doctorInfo, Validators.required],
          });
        }

      }
    )




    //this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  get formFields() { return this.updateUserForm.controls; }

  onSubmit() {
    this.submitted = true;
    console.log("submit")
    if (this.updateUserForm.invalid) {
      this.error = "Form not vaild";
      return;
    }

    this.loading = true;
    if (!this.user.isDoctor) {
      //user
      this.user = {
        fullName: this.formFields.fullName.value,
        email: this.formFields.email.value,
        phoneNumber: this.formFields.phoneNumber.value,
        isDoctor: this.user.isDoctor
      }
    } else {
      // doctor
      this.user = {
        fullName: this.formFields.fullName.value,
        email: this.formFields.email.value,
        phoneNumber: this.formFields.phoneNumber.value,
        titleDgree: this.formFields.titleDegree.value,
        doctorInfo: this.formFields.doctorInfo.value,
        specialtyId: this.formFields.specialtyId.value,
        isDoctor: this.user.isDoctor
      }
    }



    this._authService.updateUser(this.user)
      .pipe(first())
      .subscribe(
        data => {

          console.log(this.user)
          console.log("send to api")
          //this.user = data
          //console.log("url " + this.returnUrl)
          this.loading = false;
          // this._router.navigate([this.returnUrl]);
          // this._router.navigate(["/"]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  onCancel() {
    console.log("Cancel")
    this._router.navigate(["/"]);
  }

}
