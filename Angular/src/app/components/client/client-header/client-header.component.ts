import { Component, OnInit } from '@angular/core';
import { UserRoles } from 'src/app/_models/_enums/UserRoles';
import { IUserForReservation } from 'src/app/_models/_interfaces/IUserForReservation';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';

@Component({
  selector: 'app-client-header',
  templateUrl: './client-header.component.html',
  styleUrls: ['./client-header.component.scss']
})
export class ClientHeaderComponent implements OnInit {

  constructor(private _authenticationService:AuthenticationService,private _dataSharedService:DataSharedService) 
  {
    _dataSharedService.currentLoginUserChange.subscribe(data=>{
      if(data==true){
        this.loadData();
      }
    })
  }
  currentUser
  
  ngOnInit(): void {
    this.loadData()
  }
  loadData(){
    if(this.isLoggedIn()){
      this._authenticationService.getCurrentUser().subscribe(data=>{
        this.currentUser=data
      })
    }
   
  }
  Logout(){
    this._authenticationService.logout();
  }
  isLoggedIn(){
    return this._authenticationService.isLoggedIn();
  }
  
  isUserAdmin():boolean{
    let role = this._authenticationService.getRole();
    return (role == UserRoles.Admin) ? true : false
  }
  isUserDoctor():boolean{
    let role = this._authenticationService.getRole();
    return (role == UserRoles.Doctor) ? true : false
  }
  isUserPatient():boolean{
    let role = this._authenticationService.getRole();
    return (role == UserRoles.User) ? true : false
  }

  changeSearchSpecialtyId(){
    this._dataSharedService.sendSpecialtyIdFromHomePageToSearchComponent.next(0);
  }

}
