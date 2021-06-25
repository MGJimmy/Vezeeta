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
  selector: 'app-register-admin',
  templateUrl: './register-admin.component.html',
  styleUrls: ['./register-admin.component.scss']
})
export class RegisterAdminComponent implements OnInit {

  registerAdminForm: FormGroup;
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
      this.registerAdminForm = this.formBuilder.group({
        fullName: ['', Validators.required],
        username: ['', [Validators.required,Validators.pattern("[^' ']+")]],
        PasswordHash: ['',[ Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]],
        confirmPassword: ['', Validators.required],
        email: ['', Validators.required],
        phoneNumber: ['', Validators.required],
       
      },{validator:[ConfirmPasswordValidator]}as any);
      this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
    }

    get formFields() { return this.registerAdminForm.controls; }

    onSubmit() {
      this.submitted = true;
      if (this.registerAdminForm.invalid) {
        this.error = "Form not vaild";
        return;
      }
  
      this.loading = true;
      let newUser: IRegisterUser = {
        fullName: this.formFields.fullName.value,
        userName: this.formFields.username.value,
        passwordHash: this.formFields.PasswordHash.value,
        confirmPassword: this.formFields.confirmPassword.value,
        email: this.formFields.email.value,
        image: this.response.dbPath,
        phoneNumber: this.formFields.phoneNumber.value,
        
      }
      this._authService.registerAdmin(newUser)
        .pipe(first())
        .subscribe(
          data => {
            this._authService.login(this.formFields.username.value, this.formFields.PasswordHash.value)
          .subscribe(
              data => {
                this._router.navigate([this.returnUrl]);
                this._sharedDataService.currentLoginUserChange.next(true);
              }
              );
          },
          error => {
            this.error = error.error.message;
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
