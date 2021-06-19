import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataSharedService } from 'src/app/_services/data-shared.service';

@Component({
  selector: 'app-user-info-side-bar',
  templateUrl: './user-info-side-bar.component.html',
  styleUrls: ['./user-info-side-bar.component.scss']
})
export class UserInfoSideBarComponent implements OnInit {

  constructor(private _route:Router,private _sharedServices:DataSharedService) {
   
   }
   urlRouteName:string
  ngOnInit(): void {
    this.load();
  }

  load(){
    this.urlRouteName=this._route.url.replace('/MyInformation/',"");
    console.log(this.urlRouteName);
  }

}
