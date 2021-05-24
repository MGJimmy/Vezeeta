import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { IOrderDetails } from 'src/app/_models/_interfaces/IOrderDetails';
import { IPayment } from 'src/app/_models/_interfaces/IPayment';
import { IProductCartDetails } from 'src/app/_models/_interfaces/IProductCartDetails';
import { OrderService } from 'src/app/_services/order.service';
import { PaymentService } from 'src/app/_services/payment.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(private _orderService:OrderService, private _formBuilder: FormBuilder ,private _paymentService:PaymentService) { }
  totalOrderPrice:number;
  quantites:number[]=[];
  orderDetails:IOrderDetails={productCartDetails:[],totalOrderPrice:0};
  productsCartDetails:IProductCartDetails[]=[];
  paymentForm:FormGroup;
  errMsg:string;
  get formFields() { return this.paymentForm.controls; }

  ngOnInit(): void {

    this.totalOrderPrice= parseInt( sessionStorage.getItem("totalOrderPrice"))
    this.orderDetails.totalOrderPrice=this.totalOrderPrice;
    
    //bulid payment form
    this.paymentForm = this._formBuilder.group({
      cardOwnerName:['', Validators.required],
      CardNumber:['', [Validators.required,Validators.minLength(16),Validators.maxLength(16),Validators.pattern("[0-9]+")]],
      ExperationDate:['', Validators.required],
      cvc:['',[Validators.required,Validators.minLength(3),Validators.maxLength(3),Validators.pattern("[0-9]+")]],
    });

  }
  checkOut(){
   this.productsCartDetails =  JSON.parse(sessionStorage.getItem("productsCartDetails"))
   this.orderDetails = {totalOrderPrice:this.totalOrderPrice,productCartDetails:this.productsCartDetails};
   this._orderService.makeOrder(this.orderDetails)
        .pipe(first())
        .subscribe(
            data => {
               
            },
            error => {
             
            });
            

  }
  addPayment(){
    let newPayment:IPayment = 
    {
      id:0,
       cardOwnerName : this.formFields.cardOwnerName.value,
      CardNumber : this.formFields.CardNumber.value,
      cvc: this.formFields.cvc.value,
      ExperationDate: this.formFields.ExperationDate.value

    };

    this._paymentService.addNewPayment(newPayment)
    .pipe(first())
    .subscribe(
        data => {
           alert("Payment Add")
        },
        error => {
          this.errMsg=error;
        });
  }

}
