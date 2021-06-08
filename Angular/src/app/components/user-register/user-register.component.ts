import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IRegisterUser } from 'src/app/_models/_interfaces/IRegisterUser';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent implements OnInit {

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
    let newUser: IRegisterUser = {
      fullName: this.formFields.fullName.value,
      userName: this.formFields.username.value,
      passwordHash: this.formFields.password.value,
      confirmPassword: this.formFields.confirmPassword.value,
      email: this.formFields.email.value,
      image: this.response.dbPath,
      phoneNumber: this.formFields.phoneNumber.value,
      
    }
    this._authService.registerUser(newUser)
      .pipe(first())
      .subscribe(
        data => {
          this._router.navigate(["login"]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
  public uploadFinished = (event) => { 
    this.response = event;
  }
  public createImgPath = () => {
    return `${environment.apiUrl}/${this.response.dbPath}`;
  }
}