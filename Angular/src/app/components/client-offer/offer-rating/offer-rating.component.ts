import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { IOfferRate } from 'src/app/_models/_interfaces/IOfferRate';
import { OfferRatingService } from 'src/app/_services/offer-rating.service';

@Component({
  selector: 'app-offer-rating',
  templateUrl: './offer-rating.component.html',
  styleUrls: ['./offer-rating.component.scss']
})
export class OfferRatingComponent implements OnInit {

  reversationID:number;
  rate:number=0;

  constructor(private fb:FormBuilder,private _activaterouter:ActivatedRoute,
    private _offerRateServiceService:OfferRatingService,private _route:Router) {
    _activaterouter.paramMap.subscribe((param:ParamMap)=>
    {
      this.reversationID=parseInt(param.get("ReservationId"));
    })
  }

  ngOnInit(): void {
  }
  RateForm=this.fb.group({
    comment:[""],
    Rate:["",[Validators.required]],
  })

  get formFields() { return this.RateForm.controls; }

  
  Onsubmit(){
    
    const newRate:IOfferRate={
      comment:this.formFields.comment.value,
      date:new Date().toJSON(),
      rate:this.formFields.Rate.value,
      reserveOfferId:this.reversationID
    }
    this._offerRateServiceService.CreateRate(newRate).subscribe(data=>{
      this._route.navigate(['/ClientOffer/UserOfferAppointments']);
    })
    console.log(newRate);
  }

}
