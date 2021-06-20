import { Component, OnInit } from '@angular/core';
import { IOfferWithSubOffer } from 'src/app/_models/_interfaces/IOfferWithSubOffer';
import { OfferService } from 'src/app/_services/offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-navbar',
  templateUrl: './offer-navbar.component.html',
  styleUrls: ['./offer-navbar.component.scss']
})
export class OfferNavbarComponent implements OnInit {

  constructor(private _offerService:OfferService) { }
  allOffer:IOfferWithSubOffer[];
  url=environment.apiUrl

  ngOnInit(): void {
    this._offerService.getAllWithSubOffer().subscribe(data=>{
      this.allOffer=data
      console.log(data)
    })
  }

}
