import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,private _dataSharedService:DataSharedService,
      private authenticationService: AuthenticationService
  ) { 
      
  }
  // convenience getter for easy access to form fields
  get formFields() { return this.loginForm.controls; }

  ngOnInit(): void {
      //redirect to home if already logged in
      if (this.authenticationService.isLoggedIn()) { 
        this.router.navigate(['/']);
        }
      this.loginForm = this.formBuilder.group({
          username: ['', Validators.required],
          password: ['', Validators.required]
      });

      // get return url from route parameters or default to '/'
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.login(this.formFields.username.value, this.formFields.password.value)
        .pipe(first())
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
                this._dataSharedService.currentLoginUserChange.next(true)
            },
            error => {
                this.error = error;
                this.loading = false;
                console.log(error);
            });
}

}
