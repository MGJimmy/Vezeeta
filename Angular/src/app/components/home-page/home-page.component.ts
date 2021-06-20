import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { SpecilatyService } from 'src/app/_services/specilaty.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  SpecailtyList:ISpecialty[];
  constructor(private _specilatyService:SpecilatyService,private _router:Router) { }

  ngOnInit(): void {
    this._specilatyService.getAllSpecialities().subscribe(data=>
      {
        this.SpecailtyList=data;
        console.log(data);
      })
  }
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
  ShowDoctors(specailtyId)
  {
    console.log(specailtyId);
    this._router.navigate(['showDoctors',specailtyId]);
  }

}
