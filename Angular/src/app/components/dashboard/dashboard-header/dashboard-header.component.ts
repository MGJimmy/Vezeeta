import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss']
})
export class DashboardHeaderComponent implements OnInit {
  public get isUserLoggedIn(){
    return this._authenticationService.isLoggedIn();
  } 
  constructor(private _authenticationService:AuthenticationService) { }

  ngOnInit(): void {
  }
  logoutUser(){
    this._authenticationService.logout();
  }
}
