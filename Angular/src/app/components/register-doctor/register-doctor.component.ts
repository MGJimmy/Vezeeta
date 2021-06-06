import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IRegisterDoctor } from 'src/app/_models/_interfaces/IRegisterDoctor';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register-doctor',
  templateUrl: './register-doctor.component.html',
  styleUrls: ['./register-doctor.component.scss']
})
export class RegisterDoctorComponent implements OnInit {

  registerDoctorForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  public response = {dbPath: ''};
  constructor
    (private formBuilder: FormBuilder,
      private _route: ActivatedRoute,
      private _router: Router,
      private _authService: AuthenticationService
    ) { }

  ngOnInit(): void {
    this.registerDoctorForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      doctorInfo: ['', Validators.required],
      titleDegree: ['', Validators.required],
    });
  }

  get formFields() { return this.registerDoctorForm.controls; }

  onSubmit() {
    this.submitted = true;
    console.log("in")
    if (this.registerDoctorForm.invalid) {
      this.error = "Form not vaild";
      return;
    }

    this.loading = true;
    let newDoctor: IRegisterDoctor = {
      fullName: this.formFields.fullName.value,
      userName: this.formFields.username.value,
      passwordHash: this.formFields.password.value,
      confirmPassword: this.formFields.confirmPassword.value,
      email: this.formFields.email.value,
      image: this.response.dbPath,
      phoneNumber: this.formFields.phoneNumber.value,
      titleDegree: this.formFields.titleDegree.value,
      doctorInfo: this.formFields.doctorInfo.value,
      
    }
    this._authService.register(newDoctor)
      .pipe(first())
      .subscribe(
        data => {
          this._router.navigate(["login"]);
        },
        error => {
          console.log(error);
          this.error = error.body;
          this.loading = false;
        });
  }
  public uploadFinished = (event) => { 
    this.response = event;
  }
  public createImgPath = () => {
    if(this.response.dbPath !='')
    {
      return `${environment.apiUrl}/${this.response.dbPath}`;
    }
    else{return '';}
  }
}

