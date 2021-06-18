import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { IOffer } from 'src/app/_models/_interfaces/IOffer';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { OfferService } from 'src/app/_services/offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-category',
  templateUrl: './offer-category.component.html',
  styleUrls: ['./offer-category.component.scss']
})
export class OfferCategoryComponent implements OnInit {
  allOffer:IMakeOfferWithDoctorInfo[];
  OfferCategory:IOffer;
  url=environment.apiUrl;
  math=Math;

  constructor(private _makeOfferService:MakeOfferService,private _offerService:OfferService
    ,private _router:Router,private _activatedRoute:ActivatedRoute
    ,private _dataSharedService:DataSharedService) {

      _activatedRoute.paramMap.subscribe((params:ParamMap)=>{
        let offerId=parseInt(params.get('id'));
        this._offerService.getById(offerId).subscribe(data=>{
          this.OfferCategory=data;
        })
        this._makeOfferService.GetAllRelatedToOfferId(offerId).subscribe(data=>{
          this.allOffer=data;      
          console.log(this.allOffer);
          
        })
      })
    }

  

  ngOnInit(): void {
    
  }


  Book(id){
    this._router.navigate(['ClientOffer/OfferDetails']).then(()=>this._dataSharedService.GoToOfferDetailsPage.next(id))
  }

}
