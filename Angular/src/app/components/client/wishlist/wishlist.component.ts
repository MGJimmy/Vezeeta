import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { IProduct } from 'src/app/_models/_interfaces/IProduct';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { CartService } from 'src/app/_services/cart.service';
import { WishlistService } from 'src/app/_services/wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
  products: IProduct[] = [];
  constructor(private _wishlistService: WishlistService,
    private _authenticationService: AuthenticationService,
    private _cartService: CartService) { }

  ngOnInit(): void {
    this._wishlistService.getAll()
      .pipe(first())
      .subscribe(
        data => {
          this.products = data
        },
        error => {
        });
  }
  deleteProductFromWishList(prodID: number) {
    this._wishlistService.deleteProductFromWishlist(prodID)
      .pipe(first())
      .subscribe(
        data => {
          //after delete from database delete from array to uptate wishlist view
          let deletedProd = this.products.find(pc => pc.id == prodID);
          this.products.splice(this.products.indexOf(deletedProd), 1);
        },
        error => {

        });
  }
  addProductToCart(productId: number) {

    this._cartService.addProductToCart(productId).subscribe(
      data => {
        //after add product to cart remove it from wishlist
        this.deleteProductFromWishList(productId);
        alert("added to cart")
      },
      error => {
        alert(error);
      }
    )

  }

}
