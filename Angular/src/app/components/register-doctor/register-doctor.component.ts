import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ConfirmPasswordValidator } from 'src/app/Validators/ConfirmPassword';
import { IRegisterDoctor } from 'src/app/_models/_interfaces/IRegisterDoctor';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
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
  allSpecialty:ISpecialty[];
  public response = {dbPath: ''};
  TitlesDegree=["professor","teacher","consultative","specialist"];
  returnUrl: any;
  constructor
    (private formBuilder: FormBuilder,
      private _route: ActivatedRoute,
      private _router: Router,
      private _authService: AuthenticationService,private _specialtyService:SpecilatyService,
      private _sharedDataService:DataSharedService
    ) { }

  ngOnInit(): void {
    this._specialtyService.getAllSpecialities().subscribe(data=>{
      this.allSpecialty=data;
    },error=>console.error(error));
    this.registerDoctorForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      username: ['', [Validators.required,Validators.pattern("[^' ']+")]],
      PasswordHash: ['',[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*_=+-]).{8,}$")]],
      confirmPassword: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      doctorInfo: ['', Validators.required],
      titleDegree: ['', Validators.required],
      specialtyId:['',Validators.required]
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
    let newDoctor: IRegisterDoctor = {
      fullName: this.formFields.fullName.value,
      userName: this.formFields.username.value,
      passwordHash: this.formFields.PasswordHash.value,
      confirmPassword: this.formFields.confirmPassword.value,
      email: this.formFields.email.value,
      image: this.response.dbPath,
      phoneNumber: this.formFields.phoneNumber.value,
      titleDegree: this.formFields.titleDegree.value,
      doctorInfo: this.formFields.doctorInfo.value,
      specialtyId:this.formFields.specialtyId.value
    }
    console.error(newDoctor)
    
    this._authService.register(newDoctor)
      .pipe(first())
      .subscribe(
        data => {
      //this._router.navigate(["login"]);
      this._authService.login(this.formFields.username.value, this.formFields.PasswordHash.value)
        .pipe(first())
        .subscribe(
            data => {
                this._router.navigate(['/doctorDashboard/doctorSpecialty']);                
                this._sharedDataService.currentLoginUserChange.next(true)
            },
            error => {
                this.error = error;
                this.loading = false;
                console.log(error);
            });
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
    if(this.response.dbPath !='')
    {
      return `${environment.apiUrl}/${this.response.dbPath}`;
    }
    else{return '';}
  }
}

