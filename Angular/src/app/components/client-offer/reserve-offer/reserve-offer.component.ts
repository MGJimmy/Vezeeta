import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IReserveOffer } from 'src/app/_models/_interfaces/IReserveOffer';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { ReserveOfferService } from 'src/app/_services/reserve-offer.service';

@Component({
  selector: 'app-reserve-offer',
  templateUrl: './reserve-offer.component.html',
  styleUrls: ['./reserve-offer.component.scss']
})
export class ReserveOfferComponent implements OnInit {

  constructor(private _reserveOfferService:ReserveOfferService,private _fb:FormBuilder,
    private _dataSharedservice:DataSharedService) 
  { 
      _dataSharedservice.GoToReserveOfferComponent.subscribe(data=>{
        if(data.date!=""&&data.dayShiftId!=0){
          console.error(data);
          
          this.dayShiftId=data.dayShiftId;
          this.dateOfreseRvation=data.date;
        }
      })
  }
  @Output() showReserveComponent=new EventEmitter;
  @Input() doctorId
  @Input() offerId
  dateOfreseRvation
  dayShiftId

  ReserveDetails:IReserveOffer;

  ngOnInit(): void {
  }
  reservationData=this._fb.group({
    UserName:['',Validators.required],
    Phone:[,Validators.required],
    Email:[''],
  });

  submit(){
    let newReserve:IReserveOffer={
      date:this.dateOfreseRvation,
      dayShiftId:this.dayShiftId,
      doctorId:this.doctorId,
      email:this.Email.value,
      offerId:this.offerId,
      phone:this.Phone.value,
      state:true,
      userName:this.UserName.value
    }
    this._reserveOfferService.createReservation(newReserve).subscribe(data=>{
      this.showReserveComponent.emit(false)
    },err=>{console.error(err);
    })
    
  }
  cancel(){
    this.showReserveComponent.emit(false)
  }

  

  get UserName(){return this.reservationData.get('UserName')}
  get Phone(){return this.reservationData.get('Phone')}
  get Email(){return this.reservationData.get('Email')}
}
