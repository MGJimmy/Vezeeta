import { Component, OnInit } from '@angular/core';
import { IUserForReservation } from 'src/app/_models/_interfaces/IUserForReservation';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-client-header',
  templateUrl: './client-header.component.html',
  styleUrls: ['./client-header.component.scss']
})
export class ClientHeaderComponent implements OnInit {

  constructor(private _authenticationService:AuthenticationService) { }
  currentUser
  
  ngOnInit(): void {
    this.loadData()
  }
  loadData(){
    this._authenticationService.getCurrentUser().subscribe(data=>{
      this.currentUser=data
    })
  }
  Logout(){
     this._authenticationService.logout();
  }
  isLoggedIn(){
    return this._authenticationService.isLoggedIn();
  }


}
