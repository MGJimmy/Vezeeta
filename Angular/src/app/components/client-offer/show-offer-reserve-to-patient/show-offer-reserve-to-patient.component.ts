import { Component, OnInit } from '@angular/core';
import { IReserveOfferShowToPatient } from 'src/app/_models/_interfaces/IReserveOfferShowToPatient';
import { ReserveOfferService } from 'src/app/_services/reserve-offer.service';

@Component({
  selector: 'app-show-offer-reserve-to-patient',
  templateUrl: './show-offer-reserve-to-patient.component.html',
  styleUrls: ['./show-offer-reserve-to-patient.component.scss']
})
export class ShowOfferReserveToPatientComponent implements OnInit {

  constructor(private _reserveOfferService:ReserveOfferService) { }
  allReservation:IReserveOfferShowToPatient[];
  dateNow=new Date().toJSON()
  ngOnInit(): void {
    this.loadData()
  }

  loadData(){
    this._reserveOfferService.ShowReserveToPatient().subscribe(data=>{
      console.log(data);
      
      this.allReservation=data;
      
    },error=>console.error(error));
  }

  cancel(i){    
    let reserveId=this.allReservation[i].reserveOfferId
    this._reserveOfferService.CancelReservation(reserveId).subscribe(data=>{

      this.loadData();
    },err=>{console.error(err)});    
  }
}
