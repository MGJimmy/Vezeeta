import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IProduct } from 'src/app/_models/_interfaces/IProduct';
import { IProductCartDetails } from 'src/app/_models/_interfaces/IProductCartDetails';
import { IProductCartVM } from 'src/app/_models/_interfaces/IProductCartVM';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  productsCart: IProductCartVM[] = [];
  totalOrderPrice: number = 0;
  //productsCartDetails:IProductCartDetails[]=[];
  constructor(private _cartService: CartService, private _router: Router) { }

  ngOnInit(): void {
    this._cartService.getAll()
      .pipe(first())
      .subscribe(
        data => {
          this.productsCart = data
          //initialze  array
          // for (const product of this.products) {
          //     let productCartDetailsObj:IProductCartDetails= {
          //                                      productId:product.id,
          //                                      productDiscount:product.discount,
          //                                      productPrice:product.price,
          //                                      quantity:1
          //                                   }
          //     this.productsCartDetails.push(productCartDetailsObj)
          // }
        },
        error => {
        });
  }
  quantityChanged(newQuantity, productID: number) {
    var changedProduct = this.productsCart.find(p => p.productId == productID)
    let indexOfChangedProductQuantity = this.productsCart.indexOf(changedProduct);
    this.productsCart[indexOfChangedProductQuantity].productCartQuantity = parseInt(newQuantity);
  }
  computeTotalProductPrice(productID: number) {
    var productCart = this.productsCart.find(p => p.productId == productID)
    productCart.productCartQuantity = isNaN(productCart.productCartQuantity) ? 1 : productCart.productCartQuantity
    return productCart.productPrice * productCart.productCartQuantity;
  }
  computeTotalProductPriceAfterDiscount(productID: number) {
    var productCart = this.productsCart.find(p => p.productId == productID)
    productCart.productCartQuantity = isNaN(productCart.productCartQuantity) ? 1 : productCart.productCartQuantity
    return (productCart.productPrice - (productCart.productPrice * (productCart.productDiscount / 100))) * (productCart.productCartQuantity)
  }
  ComputeCartAndgoToCheckout() {
    let productCartDetails:IProductCartDetails[] = [];
    for (const index in this.productsCart) {
      let currentProduct = this.productsCart[index]
      this.totalOrderPrice += (currentProduct.productPrice 
        - (currentProduct.productPrice * (currentProduct.productDiscount / 100))) * (currentProduct.productCartQuantity);
        
        productCartDetails.push({
        productId :this.productsCart[index].productId,
        quantity:this.productsCart[index].productCartQuantity,
        productDiscount:this.productsCart[index].productDiscount,
        productPrice:this.productsCart[index].productPrice,
      });
    }
    sessionStorage.setItem("totalOrderPrice", String(this.totalOrderPrice));
    sessionStorage.setItem("productsCartDetails", JSON.stringify(productCartDetails))
    this._router.navigate(['/checkout']);

  }
  deleteProductFromCart(prodID:number){
    this._cartService.deleteProductFromCart(prodID)
        .pipe(first())
        .subscribe(
            data => {
              //after delete from database delete from array to uptate cart view
               let deletedProd= this.productsCart.find(pc=>pc.productId == prodID);
               this.productsCart.splice(this.productsCart.indexOf(deletedProd),1);
            },
            error => {
               
            });
  }


}
