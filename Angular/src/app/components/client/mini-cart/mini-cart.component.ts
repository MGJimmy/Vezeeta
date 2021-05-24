import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-mini-cart',
  templateUrl: './mini-cart.component.html',
  styleUrls: ['./mini-cart.component.scss']
})
export class MiniCartComponent implements OnInit {
  products
  constructor(private _cartService:CartService) { }

  ngOnInit(): void {
    this._cartService.getAll()
      .pipe(first())
      .subscribe(
        data => {
          this.products = data
          //initialze  array
        },
        error => {
        });
  }

}
