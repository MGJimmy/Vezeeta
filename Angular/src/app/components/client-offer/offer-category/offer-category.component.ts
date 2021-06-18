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
  offerId:number

  currentPage:number=1;
  pageSize:number=15;
  countOfDoctorOffer:number;
  numberOfPage:number;

  constructor(private _makeOfferService:MakeOfferService,private _offerService:OfferService
    ,private _router:Router,private _activatedRoute:ActivatedRoute
    ,private _dataSharedService:DataSharedService) {

      _activatedRoute.paramMap.subscribe((params:ParamMap)=>{
        this.offerId =parseInt(params.get('id'));
        this._offerService.getById(this.offerId).subscribe(data=>{
          this.OfferCategory=data;
        })

        this.GetNumberOfPage();
        this.LoadDataByPageing(this.currentPage);

        
      })
    }

  

  ngOnInit(): void {
    
  }
  GetNumberOfPage(){
    this._makeOfferService.GetCountOfMakeOfferRelatedToOffer(this.offerId).subscribe(data=>{
      this.countOfDoctorOffer=data,
      this.numberOfPage=Math.ceil(this.countOfDoctorOffer / this.pageSize)
    });
  }

  LoadDataByPageing(currentPage:number){
    this._makeOfferService.GetAllRelatedToOfferId(this.offerId,this.pageSize,currentPage).subscribe(data=>{
      this.allOffer=data;  
      this.currentPage=currentPage;        
    })
  }

  counter(i:number){
    return new Array(i);
  }
  pageChange(pageNumber){
    this.LoadDataByPageing(pageNumber);
  }



  Book(id){
    this._router.navigate(['ClientOffer/OfferDetails']).then(()=>this._dataSharedService.GoToOfferDetailsPage.next(id))
  }

}
