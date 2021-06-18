import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { IOffer } from 'src/app/_models/_interfaces/IOffer';
import { ISubOffer } from 'src/app/_models/_interfaces/ISubOffer';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { OfferService } from 'src/app/_services/offer.service';
import { SubOfferService } from 'src/app/_services/sub-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-suboffer-category',
  templateUrl: './suboffer-category.component.html',
  styleUrls: ['./suboffer-category.component.scss']
})
export class SubofferCategoryComponent implements OnInit {

  allOffer:IMakeOfferWithDoctorInfo[];
  OfferCategory:IOffer;
  SubOfferCategory:ISubOffer
  url=environment.apiUrl;
  math=Math;

  constructor(private _makeOfferService:MakeOfferService,private _offerService:OfferService,
    private _subOfferService:SubOfferService,private _router:Router,
    private _activatedRoute:ActivatedRoute,private _dataSharedService:DataSharedService) {

      _activatedRoute.paramMap.subscribe((params:ParamMap)=>{
        let subOfferId=parseInt(params.get('id'));
        this._subOfferService.getById(subOfferId).subscribe(data=>{
          this.SubOfferCategory=data;
          _offerService.getById(data.offerId).subscribe(offer=>{
            this.OfferCategory=offer;
          })
        })
        this._makeOfferService.GetAllRelatedToSubOfferId(subOfferId).subscribe(data=>{
          this.allOffer=data;          
        })
      })
    }

  ngOnInit(): void {
  }
  Book(id){
    this._router.navigate(['ClientOffer/OfferDetails']).then(()=>this._dataSharedService.GoToOfferDetailsPage.next(id))
  }
}
