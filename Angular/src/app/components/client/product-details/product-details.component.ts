import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { IProduct } from 'src/app/_models/_interfaces/IProduct';
import { IProductVM } from 'src/app/_models/_interfaces/IProductVM';
import { IReview } from 'src/app/_models/_interfaces/IReview';
import { IReviewVM } from 'src/app/_models/_interfaces/IReviewVM';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { CartService } from 'src/app/_services/cart.service';
import { ProductService } from 'src/app/_services/product.service';
import { ReviewService } from 'src/app/_services/review.service';
import { WishlistService } from 'src/app/_services/wishlist.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  isUserLoggedIn: boolean;
  productID: number;
  product: IProductVM;
  relatedProducts: IProductVM[];
  errorMsg: string;
  addReviewForm: FormGroup;
  updateReviewForm: FormGroup;
  allReviews: IReviewVM[];
  currentUserReview: IReviewVM;
  hasReview: Boolean;
  loggedUserRating: number = 1;

  loading = false;
  submitted = false;

  reviewsCount: number;
  pageSize: number = 8;
  currentPageNumber: number = 1;
  numberOfPages: number; // categoriesCount / pageSize
  needUpdate: boolean = false;

  get addReviewFormFields() { return this.addReviewForm.controls; }
  get updateReviewFormFields() { return this.updateReviewForm.controls; }
  constructor(private _route: ActivatedRoute,
    private _productServie: ProductService,
    private _spinner: NgxSpinnerService,
    private _cartService: CartService,
    private _wishlistService: WishlistService,
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _reviewService: ReviewService,
    private _authenticationService: AuthenticationService,) { }

  ngOnInit(): void {
    this.isUserLoggedIn = this._authenticationService.isLoggedIn();
    this._spinner.show();
    this.getProductIdFromUrl();
    this.getCurrentProduct();
    this.formBuildAndBind();
    this.initPaging();
    if (this.isUserLoggedIn) {
      this.getUserReview();
    }
  }
  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }
  // private functions
  private getCurrentProduct() {
    this._productServie.getProductById(this.productID)
      .pipe(first())
      .subscribe(
        data => {
          this.product = data;
          this._spinner.hide();
          this.getRelatedProducts(3);
        },
        error => {
          this.errorMsg = error;

        });
  }
  private formBuildAndBind() {
    this.addReviewForm = this._formBuilder.group({
      comment: ['']
    });
    this.updateReviewForm = this._formBuilder.group({
      comment: ['']
    });
  }
  private initPaging() {
    this._reviewService.getReviewsCount(this.productID).subscribe(
      data => {
        this.reviewsCount = data
        this.numberOfPages = Math.ceil(this.reviewsCount / this.pageSize)
        this.getSelectedPage(1);
      },
      error => {
        this.errorMsg = error;
      }
    )
  }
  private getProductIdFromUrl() {
    this._route.paramMap.subscribe((params: ParamMap) => {
      this.productID = parseInt(params.get('id'));

    })
  }
  private getUserReview() {
    this._reviewService.getCurrentUserReviewOnProduct(this.productID)
      .pipe(first())
      .subscribe(
        data => {
          this.currentUserReview = data;
          this.loggedUserRating = this.currentUserReview?.rating || 1;
          this.hasReview = (this.currentUserReview != null) ? true : false;
        },
        error => {
          this.errorMsg = error;

        });
  }
  private getRelatedProducts(numberOfProducts: number) {
    this._productServie.getRandomRelatedProducts(this.product.categoryId, numberOfProducts)
      .pipe(first())
      .subscribe(
        data => {
          this.relatedProducts = data;
        },
        error => {
          this.errorMsg = error;

        });
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
  // reviews
  openUpdateReviewForm() {
    this.needUpdate = true;
    this.updateReviewForm.setValue({
      comment: this.currentUserReview.comment
    })
  }
  cancelUpdateReviewForm() {
    this.needUpdate = false;
    this.resetFields()
  }
  addReview() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.addReviewForm.invalid) {
      return;
    }

    this.loading = true;
    let newReview: IReview =
    {
      id: 0,
      rating: this.loggedUserRating,
      comment: this.addReviewFormFields.comment.value,
      productId: this.productID
    };
    if (this.isUserLoggedIn) {
      this._reviewService.addNewReview(newReview)
        .pipe(first())
        .subscribe(
          data => {
            this.loading = false;
            this.currentUserReview = data;
            this.hasReview = true;
            this.resetFields();
            this.getSelectedPage(1);
          },
          error => {
            this.errorMsg = error;
            this.loading = false;
          });

    }
    else {
      alert("Login first to add review");
      this._router.navigate(['/login']);
    }
  }
  updateReview() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.updateReviewForm.invalid) {
      return;
    }

    this.loading = true;
    this.currentUserReview.comment = this.updateReviewFormFields.comment.value;
    this.currentUserReview.rating = this.loggedUserRating;
    this._reviewService.updateReview(this.productID, this.currentUserReview)
      .pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          this.currentUserReview = data;
          this.needUpdate = false;
          this.resetFields();
        },
        error => {
          this.errorMsg = error;
          this.loading = false;
        });
  }
  deleteReview() {
    this._reviewService.deleteReview(this.currentUserReview.id)
      .pipe(first())
      .subscribe(
        data => {
          this.loading = false;
          this.currentUserReview = null;
          this.loggedUserRating = 1
          this.hasReview = false;
          this.resetFields();
        },
        error => {
          this.errorMsg = error;
          this.loading = false;
        });
  }
  // pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber: number) {
    console.log(this.hasReview)
    this._reviewService.getReviewsByPage(this.productID, this.pageSize, currentPageNumber).subscribe(
      data => {
        this.allReviews = data
        this.currentPageNumber = currentPageNumber;
      },
      error => {
        this.errorMsg = error;
      }
    )
  }
  private resetFields(): void {
    this.addReviewForm.setValue({
      comment: ['']
    });
    this.updateReviewForm.setValue({
      comment: ['']
    });
  }
}
