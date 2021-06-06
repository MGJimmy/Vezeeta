import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-dashboard-sildbar',
  templateUrl: './doctor-dashboard-sildbar.component.html',
  styleUrls: ['./doctor-dashboard-sildbar.component.scss']
})
export class DoctorDashboardSildbarComponent implements OnInit {

  constructor(private _route:Router) { }
  urlRouteName:string
  ngOnInit(): void {
    this.urlRouteName=this._route.url.replace('/doctorDashboard/',"");    
  }
}
