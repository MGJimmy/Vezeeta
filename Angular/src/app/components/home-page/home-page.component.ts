import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IOfferWithMakeOfferCount } from 'src/app/_models/_interfaces/IOfferWithMakeOfferCount';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { OfferService } from 'src/app/_services/offer.service';
import { SpecilatyService } from 'src/app/_services/specilaty.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  SpecailtyList:ISpecialty[];
  Sp_list:any;

  allOfferWithMakeOfferCount:IOfferWithMakeOfferCount[];
  url=environment.apiUrl

  constructor(private _specilatyService:SpecilatyService,private _router:Router
    ,private _dataSharedService:DataSharedService,private _offerService:OfferService) { }

  ngOnInit(): void {
    this._offerService.getAllWithMakeOfferCount().subscribe(data=>{
      this.allOfferWithMakeOfferCount=this.chunks(data,4);
    })

    this._specilatyService.getAllSpecialities().subscribe(data=>
      {
        console.log(data);
        this.SpecailtyList=this.chunks(data,4);
        console.log(this.SpecailtyList);
      })
  }

  ShowDoctors(specailtyId)
  {
    // console.log(specailtyId);
    // this._router.navigate(['showDoctors',specailtyId]);
    this._dataSharedService.sendSpecialtyIdFromHomePageToSearchComponent.next(specailtyId);
  }
  chunks(array, size) {
    let results = [];
    results = [];
    while (array.length) {
    results.push(array.splice(0, size));
    }
      return results;
  }

  goToOfferCategory(offerId){
    this._router.navigate(['ClientOffer/offers',offerId])
  }
}
