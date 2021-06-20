import { Component, OnInit } from '@angular/core';
import { IReserveOfferShowToDoctor } from 'src/app/_models/_interfaces/IReserveOfferShowToDoctor';
import { ReserveOfferService } from 'src/app/_services/reserve-offer.service';

@Component({
  selector: 'app-show-offer-reserve-to-doctor',
  templateUrl: './show-offer-reserve-to-doctor.component.html',
  styleUrls: ['./show-offer-reserve-to-doctor.component.scss']
})
export class ShowOfferReserveToDoctorComponent implements OnInit {

  constructor(private _reserveOfferService:ReserveOfferService) { }

  allreservation:IReserveOfferShowToDoctor[]

  ngOnInit(): void {
    this._reserveOfferService.ShowReserveTodoctor().subscribe(data=>{
      this.allreservation=data;
      console.log(data);
      
    },err=>{console.error(err)});
  }

}
