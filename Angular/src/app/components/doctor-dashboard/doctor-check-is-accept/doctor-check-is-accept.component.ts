import { Component, OnInit } from '@angular/core';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-doctor-check-is-accept',
  templateUrl: './doctor-check-is-accept.component.html',
  styleUrls: ['./doctor-check-is-accept.component.scss']
})
export class DoctorCheckIsAcceptComponent implements OnInit {

  constructor(private _doctorService:DoctorService) { }

  ngOnInit(): void {
    this._doctorService.checkDoctorAccoutIsAccept().subscribe(data=>{
      this.isAcceptDoctor=data.acceptState;
    })
  }
  printData=[];
  isAcceptDoctor:boolean=false;

  check(){
    this._doctorService.checkDoctorAccoutIsAccept().subscribe(data=>{
      console.log(data)
      this.isAcceptDoctor=data.acceptState;
      this.printData=data.errorDetails;
    })

  }

}
