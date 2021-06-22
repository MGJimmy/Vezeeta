import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IOfferRate } from '../_models/_interfaces/IOfferRate';
import { IOfferRateWithAvg } from '../_models/_interfaces/IOfferRateWithAvg';

@Injectable({
  providedIn: 'root'
})
export class OfferRatingService {

  constructor(private http:HttpClient) { }

  url=environment.apiUrl+"/api/OfferRating"

  CreateRate(rate:IOfferRate){

   return this.http.post<any>(this.url,rate).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")}))
  }

  GetRateing(doctorOfferId,Commentnumber){
    return this.http.get<IOfferRateWithAvg>(`${this.url}/${doctorOfferId}/${Commentnumber}`)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    ) 
  }
  
}
