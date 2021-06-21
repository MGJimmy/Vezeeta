import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmPasswordValidator } from 'src/app/Validators/ConfirmPassword';
import { UserPassword } from 'src/app/_models/_interfaces/IUserPassword';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  constructor(private fb:FormBuilder,private _authService:AuthenticationService,
    private _router:Router,
    private sharedDataService:DataSharedService) { }

  ngOnInit(): void {
  }
  ResetForm=this.fb.group({
    OldPassword:['',Validators.required],
    PasswordHash:['',[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]],  
    confirmPassword:['',[Validators.required,Validators.minLength(8)]],
  },{validator:[ConfirmPasswordValidator]}as any)
  get formFields() { return this.ResetForm.controls; }

  //newUser:IUser;
  user:UserPassword;
  PasswordCheck:boolean=false;
  //Oldpassword:any;
  ResetPassword()
  {
    this.user={
      "oldPassword":this.formFields.OldPassword.value,
      "newPassword":this.formFields.PasswordHash.value,
     }  
      console.log(this.user);
     this._authService.CheckPassword(this.user).subscribe(data=>{
      console.log(data);
      if(data){
        console.log("correct Password");
        this.PasswordCheck=false;
        this._authService.logout();
        this._router.navigate(["/login"]);
      }
    },(err:HttpErrorResponse)=>
    {
      console.log("not correct");
        this.PasswordCheck=true;
    })
    }

}
