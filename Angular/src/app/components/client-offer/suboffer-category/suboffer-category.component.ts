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

  subOfferId:number
  allOffer:IMakeOfferWithDoctorInfo[];
  OfferCategory:IOffer;
  SubOfferCategory:ISubOffer
  url=environment.apiUrl;
  math=Math;

  currentPage:number=1;
  pageSize:number=15;
  countOfDoctorOffer:number;
  numberOfPage:number;


  constructor(private _makeOfferService:MakeOfferService,private _offerService:OfferService,
    private _subOfferService:SubOfferService,private _router:Router,
    private _activatedRoute:ActivatedRoute,private _dataSharedService:DataSharedService) {

      _activatedRoute.paramMap.subscribe((params:ParamMap)=>{
        this.subOfferId=parseInt(params.get('id'));
        this._subOfferService.getById(this.subOfferId).subscribe(data=>{
          this.SubOfferCategory=data;
          _offerService.getById(data.offerId).subscribe(offer=>{
            this.OfferCategory=offer;
          })
        })
        this.GetNumberOfPage();
        this.LoadDataByPageing(this.currentPage);
      })
    }

  ngOnInit(): void {
  }

  GetNumberOfPage(){
    this._makeOfferService.GetCountOfMakeOfferRelatedToSubOffer(this.subOfferId).subscribe(data=>{
      this.countOfDoctorOffer=data,
      this.numberOfPage=Math.ceil(this.countOfDoctorOffer / this.pageSize)
    });
  }

  LoadDataByPageing(currentPage:number){
    this._makeOfferService.GetAllRelatedToSubOfferId(this.subOfferId,this.pageSize,currentPage).subscribe(data=>{
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
