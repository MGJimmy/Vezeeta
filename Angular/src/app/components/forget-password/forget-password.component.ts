import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit {

  forgetForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  notify='';
  error = '';

  constructor(
      private formBuilder: FormBuilder,
      private router: Router,private route: ActivatedRoute,
      private authenticationService: AuthenticationService
  ) { 
      
  }
  // convenience getter for easy access to form fields
  get formFields() { return this.forgetForm.controls; }

  ngOnInit(): void {
      //redirect to home if already logged in
      if (this.authenticationService.isLoggedIn()) { 
        this.router.navigate(['/']);
        }
      this.forgetForm = this.formBuilder.group({
          email: ['', Validators.required]
      });

      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.forgetForm.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.forgetPassword(this.formFields.email.value)
        .pipe(first())
        .subscribe(
            data => {
              this.error='';
              this.notify="Check your email to reset your password.";
              setTimeout(() => {
                this.router.navigate([this.returnUrl]);
              }, 5000);
                
            },
            error => {
                this.error = "this email didn't signup in the websit";
                this.loading = false;
            });
}

}
