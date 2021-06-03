import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
  selector: 'app-client-header',
  templateUrl: './client-header.component.html',
  styleUrls: ['./client-header.component.scss']
})
export class ClientHeaderComponent implements OnInit {

  constructor(private _authenticationService:AuthenticationService) { }

  ngOnInit(): void {
  }
  Logout(){
     this._authenticationService.logout();
  }

}
