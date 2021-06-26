import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ISuggestDoctor } from 'src/app/_models/_interfaces/ISuggestDoctor';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-suggestion-doctors',
  templateUrl: './suggestion-doctors.component.html',
  styleUrls: ['./suggestion-doctors.component.scss']
})
export class SuggestionDoctorsComponent implements OnInit {

  SuggestDoctors:ISuggestDoctor[]=[];
  constructor(private _doctorService:DoctorService,private _router: Router,private _auth:AuthenticationService) { }

  ngOnInit(): void {
    if(this._auth.isLoggedIn()){
      this._doctorService.ShowSuggestionDoctors().subscribe(data=>{
        this.SuggestDoctors=this.chunks(data,4);
      })
    }
    else{
      this._doctorService.ShowSuggestionDoctorsForGuest().subscribe(data=>{
        this.SuggestDoctors=this.chunks(data,4);
      })
    }
    
  }

  public createImgPath = (serverPath: string) => {
    return `http://localhost:57320/${serverPath}`;

  }
  chunks(array, size) {
    let results = [];
    results = [];
    while (array.length) {
    results.push(array.splice(0, size));
    }
      return results;
  }

  ShowDetails(DoctorId) {
    this._router.navigate(['ShowDoctorDetails', DoctorId]);
  }
}
