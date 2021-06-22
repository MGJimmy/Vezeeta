import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRoles } from 'src/app/_models/_enums/UserRoles';
import { IReserveOffer } from 'src/app/_models/_interfaces/IReserveOffer';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { ReserveOfferService } from 'src/app/_services/reserve-offer.service';

@Component({
  selector: 'app-reserve-offer',
  templateUrl: './reserve-offer.component.html',
  styleUrls: ['./reserve-offer.component.scss']
})
export class ReserveOfferComponent implements OnInit {

  constructor(private _reserveOfferService:ReserveOfferService,private _fb:FormBuilder,private _route:Router,
    private _dataSharedservice:DataSharedService,private _authentcation:AuthenticationService) 
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


  isUserPatient():boolean{
    let role = this._authentcation.getRole();
    return (role == UserRoles.User) ? true : false
  }
  isUserDoctor():boolean{
    let role = this._authentcation.getRole();
    return (role == UserRoles.Doctor) ? true : false
  }

  submit(){

    if(this._authentcation.isLoggedIn()==true && this.isUserPatient()){
      let newReserve:IReserveOffer={
        date:this.dateOfreseRvation,
        dayShiftId:this.dayShiftId,
        doctorId:this.doctorId,
        email:this.Email.value,
        makeOfferId:this.offerId,
        phone:this.Phone.value,
        state:true,
        userName:this.UserName.value
      }
      this._reserveOfferService.createReservation(newReserve).subscribe(data=>{
        this.showReserveComponent.emit(false)
      },err=>{console.error(err);
      })
    }else{
      this._route.navigate(['/login']);
    }

    
    
  }
  cancel(){
    this.showReserveComponent.emit(false)
  }

  

  get UserName(){return this.reservationData.get('UserName')}
  get Phone(){return this.reservationData.get('Phone')}
  get Email(){return this.reservationData.get('Email')}
}
