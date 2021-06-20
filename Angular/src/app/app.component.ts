import { Component } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Angular';

     // owl carousel
     customOptions: OwlOptions = {
      loop: true,
      mouseDrag: true,
      touchDrag: true,
      pullDrag: false,
      stagePadding:150,
      margin:30,
      autoplayTimeout: 3000,
      autoplayHoverPause: true,
      dots: false,
      autoplay:false,
      navSpeed: 600,
      navText: ['&#8249', '&#8250;'],
      responsive: {
        0: {
          stagePadding: 100,
          items: 1 
        },
        400: {
          stagePadding: 100,
          items: 2
        },
        760: {
          stagePadding: 130,
          items: 3
        },
        1000: {
          items: 4
        }
      },
      nav: false
    }
}
