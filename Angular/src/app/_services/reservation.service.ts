import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IReservation } from '../_models/_interfaces/IReservation';
import { IReservationShowToDoctor } from '../_models/_interfaces/IReservationShowToDoctor';
import { IReservationShowToPatient } from '../_models/_interfaces/IReservationShowToPatient';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private _http:HttpClient) { }

  url=`${environment.apiUrl}/api/Reservation`;

  GetToShowInContinuePage(id:number):Observable<IReservation>{
    return this._http.get<IReservation>(this.url+"/"+id).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  ShowReserveToPatient():Observable<IReservationShowToPatient[]>{
    return this._http.get<IReservationShowToPatient[]>(this.url+"/showReserveToPatient").pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }
  ShowReserveTodoctor():Observable<IReservationShowToDoctor[]>{
    return this._http.get<IReservationShowToDoctor[]>(this.url+"/showReserveToDoctor").pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  createReservation(reserve:IReservation):Observable<any>{
    return this._http.post<any>(this.url,reserve).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  update(reserve:IReservation):Observable<any>{
    return this._http.put<any>(this.url,reserve).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  CancelReservation(reserveId:number):Observable<any>{
    return this._http.delete<any>(this.url+"/"+reserveId).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }
}
