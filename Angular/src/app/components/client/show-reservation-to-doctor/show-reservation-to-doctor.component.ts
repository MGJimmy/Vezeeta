import { Component, OnInit } from '@angular/core';
import { IReservationShowToDoctor } from 'src/app/_models/_interfaces/IReservationShowToDoctor';
import { ReservationService } from 'src/app/_services/reservation.service';

@Component({
  selector: 'app-show-reservation-to-doctor',
  templateUrl: './show-reservation-to-doctor.component.html',
  styleUrls: ['./show-reservation-to-doctor.component.scss']
})
export class ShowReservationToDoctorComponent implements OnInit {

  constructor(private _reserveServices:ReservationService) { }

  allreservation:IReservationShowToDoctor[]

  ngOnInit(): void {
    this._reserveServices.ShowReserveTodoctor().subscribe(data=>{
      this.allreservation=data;
    },err=>{console.error(err)});
  }

}
