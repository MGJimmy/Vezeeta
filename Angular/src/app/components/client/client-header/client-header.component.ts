import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserRoles } from 'src/app/_models/_enums/UserRoles';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-client-header',
  templateUrl: './client-header.component.html',
  styleUrls: ['./client-header.component.scss']
})
export class ClientHeaderComponent implements OnInit {
  openSearch:boolean = false;
  isLoggedIn:boolean = false;
  showMiniCart : boolean = false;
  constructor(private _router:Router, 
    private _authenticationService:AuthenticationService ) { }

  ngOnInit(): void {
  }
  openSearchBar(){
    this.openSearch = true;
  }
  closeSearchBar(){
    this.openSearch = false;
  }
  goToSearchPage(searchKey){
    this._router.navigate([`/search-results/${searchKey}`])
  }
  isUserLoggedIn():boolean{
    return this._authenticationService.isLoggedIn();
  }
  isUserAdmin():boolean{
    let role = this._authenticationService.getRole();
    return (role == UserRoles.Admin) ? true : false
  }
  logoutUser(){
    this._authenticationService.logout();
  }
}
