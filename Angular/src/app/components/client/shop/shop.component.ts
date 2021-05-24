import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'protractor';
import { ICategory } from 'src/app/_models/_interfaces/ICategory';
import { IColor } from 'src/app/_models/_interfaces/IColor';
import { IProductVM } from 'src/app/_models/_interfaces/IProductVM';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { CartService } from 'src/app/_services/cart.service';
import { CategoryService } from 'src/app/_services/category.service';
import { ColorService } from 'src/app/_services/color.service';
import { ProductService } from 'src/app/_services/product.service';
import { WishlistService } from 'src/app/_services/wishlist.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  hasProducts:boolean = false;
  errorMsg: string;
  allCategories: ICategory[];
  allColors: IColor[];
  productsPerPage: IProductVM[];
  pageSize: number = 9;
  productsCount: number;
  currentPageNumber: number = 1;
  numberOfPages: number; // categoriesCount / pageSize
  title: string = "All products";

  selectedCategoryId: number;
  selectedCategoryName: string;
  selectedColorId: number;
  selectedColorName: string;
  constructor(
    private _productSevice: ProductService,
    private _categoryService: CategoryService,
    private _colorService: ColorService,
    private _cartService: CartService,
    private _wishlistService: WishlistService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _authenticationService: AuthenticationService) {

    this._route.queryParams
      .subscribe(params => {
        // Defaults to 0 if no query param provided.
        //this.selectedCategoryId = this.selectedColorId = 0
        this.selectedCategoryId = params['categoryId'] || 0;
        this.selectedCategoryName = params['categoryName'] || this.selectedCategoryName;
        this.selectedColorId = params['colorId'] || 0;
        this.selectedColorName = params['colorName'] || this.selectedColorName;
        this.changePagetTitle();
        this.getSelectedPage(1);

      })
  }

  ngOnInit(): void {
    // load colors && categorys
    this._categoryService.getAllCategories().subscribe(
      data => {
        this.allCategories = data
      },
      error => {
        this.errorMsg = error;
      }
    )
    this._colorService.getAllColors().subscribe(
      data => {
        this.allColors = data
      },
      error => {
        this.errorMsg = error;
      }
    )
    this.getProductsCount();
  }
  getProductsForCategoryPerPage(currentPageNumber: number): void {
    this._productSevice.getProductsByCategoryPaging(this.selectedCategoryId, this.pageSize, currentPageNumber).subscribe(
      data => {
        this.productsPerPage = data
        this.getProductsCount(this.selectedCategoryId);
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasProducts = true;
        else
          this.hasProducts = false;
      },
      error => {
        this.errorMsg = error
      }
    );
  }
  getProductsForColorPerPage(currentPageNumber: number): void {
    this._productSevice.getProductsByColorPaging(this.selectedColorId, this.pageSize, currentPageNumber).subscribe(
      data => {
        this.productsPerPage = data
        console.log(data)
        this.getProductsCount(0, this.selectedColorId);
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasProducts = true;
        else
          this.hasProducts = false;
      },
      error => {
        this.errorMsg = error
      }
    );
  }

  getProductsPerPage(currentPageNumber: number) {
    this._productSevice.getProductsByPage(this.pageSize, currentPageNumber).subscribe(
      data => {
        this.productsPerPage = data
        this.currentPageNumber = currentPageNumber;
        this.getProductsCount();
        if(data.length != 0)
          this.hasProducts = true;
        else
          this.hasProducts = false;

      },
      error => {
        this.errorMsg = error;
      }
    )
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

  // pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber: number) {
    if (this.selectedCategoryId != 0) {
      this.getProductsForCategoryPerPage(currentPageNumber);
    }
    else if (this.selectedColorId != 0) {
      this.getProductsForColorPerPage(currentPageNumber);
    }
    else {
      this.getProductsPerPage(currentPageNumber);
    }
  }
  // image path
  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }

  changeSelectedCategoryName(categoryName) {
    this.selectedCategoryName = categoryName
  }
  changeSelectedColorName(colorName) {
    this.selectedColorName = colorName
  }
  getProductsCount(categoryId = 0, colorId = 0) {
    this._productSevice.getProductsCount(categoryId, colorId).subscribe(
      data => {
        this.productsCount = data
        this.numberOfPages = Math.ceil(this.productsCount / this.pageSize)
      },
      error => {
        this.errorMsg = error;
      }
    )
  }
  changePagetTitle() {
    if (this.selectedCategoryId != 0) {
      this.title = "Category: " + this.selectedCategoryName;
    }
    else if (this.selectedColorId != 0) {
      this.title = "Color: " + this.selectedColorName;
    }
    else {
      this.title = "All products"
    }
  }
}
