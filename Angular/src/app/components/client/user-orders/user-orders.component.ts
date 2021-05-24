import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/_models/_interfaces/IOrder';
import { IOrderProduct } from 'src/app/_models/_interfaces/IOrderProduct';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.scss']
})
export class UserOrdersComponent implements OnInit {

  allOrders:IOrder[]; 
  orderDetailsList:IOrderProduct[]; 
  constructor(private _orderAppSeriv:OrderService, private authenticationService: AuthenticationService) { }
  pageSize:number=3;
  currentPageNumber:number=1
  errorMsg:string;
  OrdersCount:number;
  numberOfPages:number;
  userID:string;

  ngOnInit(): void {

    this.userID=this.authenticationService.getUserId();
      
     
    this._orderAppSeriv.getOrderCountForSpecficUser(this.userID).subscribe(
      data => {
        this.OrdersCount = data
        this.numberOfPages = Math.ceil(this.OrdersCount / this.pageSize)
      },
      error=>
      {
       this.errorMsg = error;
      })
      this.getSelectedPage(1);
  }
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._orderAppSeriv.getOrdersByPageForSpecficUser(this.userID,this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allOrders = data
        this.currentPageNumber = currentPageNumber;
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  orderDetails(id:number){
    this._orderAppSeriv.orderDetails(id).subscribe(
      data => {
        this.orderDetailsList = data;
      },
      error=>
      {
        this.errorMsg = error;
      }
    ) 
  }

}
