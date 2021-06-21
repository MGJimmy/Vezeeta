import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IRate } from '../_models/_interfaces/IRate';
import { IRates } from '../_models/_interfaces/IRatesWithAverageRate';

@Injectable({
  providedIn: 'root'
})
export class RateServiceService {

  constructor(private http:HttpClient) { }

  url=environment.apiUrl+"/api/Rating"

  //{DoctorId}

  CreateRate(rate:IRate){

   return this.http.post<any>(this.url,rate).pipe(catchError((error)=>{
      return throwError(error.message||"an error ocur")}))
  }

  // GetRateing(doctorId){

  //   return this.http.get<IRates[]>(`${environment.apiUrl}/api/Rating/${doctorId}`)
  //   .pipe(catchError((err) => {
  //       console.log(err);
  //       return throwError(err.message || "An Error Occur")
  //     })
  //   ) 
  // }
  GetRateing(doctorId,Commentnumber){

    // return this.http.get<any>(this.url+"/"+{doctorId}).pipe(catchError((error)=>{
    //    return throwError(error.message||"an error ocur")}))
    return this.http.get<IRates[]>(`${environment.apiUrl}/api/Rating/${doctorId}/${Commentnumber}`)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    ) 
  }
 

}
