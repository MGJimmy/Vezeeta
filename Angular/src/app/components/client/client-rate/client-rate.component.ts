import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IRate } from 'src/app/_models/_interfaces/IRate';
import { RateServiceService } from 'src/app/_services/rate-service.service';

@Component({
  selector: 'app-client-rate',
  templateUrl: './client-rate.component.html',
  styleUrls: ['./client-rate.component.scss']
})
export class ClientRateComponent implements OnInit {

  reversationID:number;
  constructor(private fb:FormBuilder,private _activaterouter:ActivatedRoute,private _rateServiceService:RateServiceService) {
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



rate:number=0;  
  Onsubmit(){
    
    const newRate:IRate={
      comment:this.formFields.comment.value,
      date:new Date().toJSON(),
      rate:this.formFields.Rate.value,
       reservationId:this.reversationID
    }

    this._rateServiceService.CreateRate(newRate).subscribe(data=>{
      console.log("done");
      console.log(data);
    })
    console.log(newRate);
  }
}
