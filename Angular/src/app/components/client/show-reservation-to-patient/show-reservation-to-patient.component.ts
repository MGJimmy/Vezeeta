import { Component, OnInit } from '@angular/core';
import { IReservationShowToPatient } from 'src/app/_models/_interfaces/IReservationShowToPatient';
import { ReservationService } from 'src/app/_services/reservation.service';

@Component({
  selector: 'app-show-reservation-to-patient',
  templateUrl: './show-reservation-to-patient.component.html',
  styleUrls: ['./show-reservation-to-patient.component.scss']
})
export class ShowReservationToPatientComponent implements OnInit {

  constructor(private _reserveService:ReservationService) { }

  allReservation:IReservationShowToPatient[];
  dateNow=new Date().toJSON()

  ngOnInit(): void {
    this.loadData();
  }

  loadData(){
    this._reserveService.ShowReserveToPatient().subscribe(data=>{
      
      this.allReservation=data;
      console.log( this.allReservation)
    },error=>console.error(error));
  }

  cancel(i){
    let reserveId=this.allReservation[i].reservetionId
    this._reserveService.CancelReservation(reserveId).subscribe(data=>{
      this.loadData();
    },err=>{console.error(err)});    
  }

}
