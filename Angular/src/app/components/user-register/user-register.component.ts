import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ConfirmPasswordValidator } from 'src/app/Validators/ConfirmPassword';
import { IRegisterUser } from 'src/app/_models/_interfaces/IRegisterUser';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';
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
  returnUrl: any;
  constructor
    (private formBuilder: FormBuilder,
      private _route: ActivatedRoute,
      private _router: Router,
      private _authService: AuthenticationService,
      private _sharedDataService:DataSharedService
    ) { }

  ngOnInit(): void {
    this.registerDoctorForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      username: ['',Validators.required],
      PasswordHash: ['',[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]],
      confirmPassword: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
     
    },{validator:[ConfirmPasswordValidator]}as any);
    this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
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
          //this._router.navigate(["login"]);
          this._authService.login(this.formFields.username.value, this.formFields.password.value)
        .pipe(first())
        .subscribe(
            data => {
              this._router.navigate([this.returnUrl]);
              this._sharedDataService.currentLoginUserChange.next(true)
            },
            error => {
                this.error = error;
                this.loading = false;
                console.log(error);
            });
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
