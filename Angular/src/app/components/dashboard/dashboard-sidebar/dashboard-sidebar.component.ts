import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-sidebar',
  templateUrl: './dashboard-sidebar.component.html',
  styleUrls: ['./dashboard-sidebar.component.scss']
})
export class DashboardSidebarComponent implements OnInit {

  constructor(private _router:Router, private _activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
  }
  navigateToCategories():void{
    this._router.navigate(['categories'],{relativeTo:this._activatedRoute});    
  }
  navigateToOrders():void{

    this._router.navigate(['orders'],{relativeTo:this._activatedRoute});  
  }
  navigateToProducts():void{
    this._router.navigate(['products'],{relativeTo:this._activatedRoute});   
  }
  navigateToUsers(): void {
    this._router.navigate(['users'],{relativeTo:this._activatedRoute});   
  }

  navigateToColors(): void {
    
    this._router.navigate(['colors'],{relativeTo:this._activatedRoute});   
  }
}
