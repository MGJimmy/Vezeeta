import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataSharedService } from 'src/app/_services/data-shared.service';

@Component({
  selector: 'app-doctor-dashboard-sildbar',
  templateUrl: './doctor-dashboard-sildbar.component.html',
  styleUrls: ['./doctor-dashboard-sildbar.component.scss']
})
export class DoctorDashboardSildbarComponent implements OnInit {

  constructor(private _route:Router,private _sharedServices:DataSharedService) { 
    this._sharedServices.IsDoctorSideBarChange.subscribe(data=>{
      if(data==true){
        this.load();
      }
    })
  }
  urlRouteName:string
  ngOnInit(): void {
    this.load()
  }
  load(){
    this.urlRouteName=this._route.url.replace('/doctorDashboard/',"");
    console.log(this.urlRouteName)
  }
}
