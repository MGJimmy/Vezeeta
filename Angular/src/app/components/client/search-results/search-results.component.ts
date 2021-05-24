import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'protractor';
import { IProductVM } from 'src/app/_models/_interfaces/IProductVM';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { CartService } from 'src/app/_services/cart.service';
import { ProductService } from 'src/app/_services/product.service';
import { WishlistService } from 'src/app/_services/wishlist.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss']
})
export class SearchResultsComponent implements OnInit {
  searchKey:string;
  searchedProducts:IProductVM[];
  errorMsg:string;
  foundProducts:boolean = false;
  constructor(private _route:ActivatedRoute,
    private _productService:ProductService,
    private _cartService:CartService,
    private _wishlistService:WishlistService,
    private _authenticationService:AuthenticationService,
    private _router:Router) { }

  ngOnInit(): void {
    this._route.paramMap.subscribe(
      params=>{
        this.searchKey = params.get('searchkeyword');
        this.getSearchedProducts();
      }
    )
  }
  getSearchedProducts(){
    this._productService.getProductsBySearch(this.searchKey).subscribe(
      data=>{
        this.searchedProducts = data;
        if(data.length != 0)
          this.foundProducts = true;
        else
          this.foundProducts = false;
      },
      error=>{
        this.errorMsg = error;
      }
    )
  }
  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }
  // dealing with cart
  addProductToCart(productId: number) {
    if (this._authenticationService.isLoggedIn()) {
      this._cartService.addProductToCart(productId).subscribe(
        data => {
          alert("added to cart")
        },
        error => {
          alert(error);
        }
      )
    }
    else {
      alert("Login to add product to cart");
      this._router.navigate(['/login'])
    }
  }
  addProductToWishlist(productId) {
    if(this._authenticationService.isLoggedIn())
    {
      this._wishlistService.addProductToWishlist(productId).subscribe(
        data => {
          alert("added to wishlist")
        },
        error => {
          alert(error);
        }
      )
    }
    else {
      alert("Login to add product to wishlist");
      this._router.navigate(['/login']);
    }
  }

    

}
