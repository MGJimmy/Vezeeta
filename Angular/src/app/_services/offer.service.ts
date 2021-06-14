import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IOffer } from '../_models/_interfaces/IOffer';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private _http:HttpClient) { }
  
  url = `${environment.apiUrl}/api/Offer`;

  getAll():Observable<IOffer[]> {
    return this._http.get<IOffer[]>(this.url).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  getById(id:number):Observable<IOffer>{
    return this._http.get<IOffer>(this.url+"/"+id).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addNewOffer(newOffer:IOffer):Observable<IOffer>{
    return this._http.post<IOffer>(this.url, newOffer).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  updateOffer(OfferToUpdate:IOffer):Observable<any>{
    return this._http.put<any>(this.url, OfferToUpdate).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  deleteOffer(id:number):Observable<any>{
    return this._http.delete<any>(this.url+"/"+id).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getOfferCount():Observable<number>{
    return this._http.get<number>(this.url+"/count").pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getByPage(pageSize:number, pageNumber:number):Observable<IOffer[]>{
    return this._http.get<IOffer[]>(`${this.url}/${pageSize}/${pageNumber}`).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}
