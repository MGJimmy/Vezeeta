import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IReserveOffer } from '../_models/_interfaces/IReserveOffer';
import { IReserveOfferShowToDoctor } from '../_models/_interfaces/IReserveOfferShowToDoctor';
import { IReserveOfferShowToPatient } from '../_models/_interfaces/IReserveOfferShowToPatient';

@Injectable({
  providedIn: 'root'
})
export class ReserveOfferService {

  constructor(private _http:HttpClient) { }

  url=`${environment.apiUrl}/api/ReserveOffer`

  ShowReserveToPatient():Observable<any>{
    return this._http.get<any>(this.url+"/showReserveToPatient").pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }
  ShowReserveTodoctor():Observable<IReserveOfferShowToDoctor[]>{
    return this._http.get<IReserveOfferShowToDoctor[]>(this.url+"/showReserveToDoctor").pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  createReservation(reserve:IReserveOffer):Observable<any>{
    return this._http.post<any>(this.url,reserve).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }

  CancelReservation(reserveId:number):Observable<any>{
    return this._http.delete<any>(this.url+"/"+reserveId).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")
    }));
  }
}
