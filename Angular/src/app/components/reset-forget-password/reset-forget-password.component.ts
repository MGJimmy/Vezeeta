import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ConfirmPasswordValidator } from 'src/app/Validators/ConfirmPassword';
import { IResetForgetPassword } from 'src/app/_models/_interfaces/IResetForgetPassword';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-reset-forget-password',
  templateUrl: './reset-forget-password.component.html',
  styleUrls: ['./reset-forget-password.component.scss']
})
export class ResetForgetPasswordComponent implements OnInit {

  resetForm: FormGroup;
  loading = false;
  submitted = false;
  notify='';
  error = '';
  email;
  token;

  constructor(
      private formBuilder: FormBuilder,
      private router: Router,private _activateroute: ActivatedRoute,
      private authenticationService: AuthenticationService
  ) { 
    this._activateroute.queryParamMap.subscribe((params)=>{
      this.email=params.get('email');
      this.token=params.get('token');
    })
  }
  // convenience getter for easy access to form fields
  get formFields() { return this.resetForm.controls; }

  ngOnInit(): void {
      //redirect to home if already logged in
      if (this.authenticationService.isLoggedIn()) { 
        this.router.navigate(['/']);
        }
      this.resetForm = this.formBuilder.group({
        PasswordHash: ['', [Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]],
        confirmPassword: ['', [Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]]
      },{validator:[ConfirmPasswordValidator]}as any);

  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.resetForm.invalid) {
        return;
    }
    const resetPassword:IResetForgetPassword={
      email:this.email,
      password:this.formFields.PasswordHash.value,
      token:this.token
    };
    
    this.loading = true;
    
    this.authenticationService.resetForgetPassword(resetPassword)
      .pipe(first())
      .subscribe(
            data => {
              this.router.navigate(["/login"]);
            },
            error => {
                this.error = "this email didn't signup in the websit";
                this.loading = false;
            }
      );
  }

}
