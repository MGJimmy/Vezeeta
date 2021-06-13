import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ISubOffer } from '../_models/_interfaces/ISubOffer';

@Injectable({
  providedIn: 'root'
})
export class SubOfferService {

  constructor(private _http:HttpClient) { }
  
  url = `${environment.apiUrl}/api/SubOffer`;
  
  getAll():Observable<ISubOffer[]> {
    return this._http.get<ISubOffer[]>(this.url).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  getById(id:number):Observable<ISubOffer>{
    return this._http.get<ISubOffer>(this.url+"/"+id).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  addNewSubOffer(newOffer:ISubOffer):Observable<ISubOffer>{
    return this._http.post<ISubOffer>(this.url, newOffer).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  updateSubOffer(OfferToUpdate:ISubOffer):Observable<any>{
    return this._http.put<any>(this.url, OfferToUpdate).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  deleteSubOffer(id:number):Observable<any>{
    return this._http.delete<any>(this.url+"/"+id).pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getSubOfferCount():Observable<number>{
    return this._http.get<number>(this.url+"/count").pipe(catchError((err)=>{
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getByPage(pageSize:number, pageNumber:number):Observable<ISubOffer[]>{
    return this._http.get<ISubOffer[]>(`${this.url}/${pageSize}/${pageNumber}`).pipe(catchError((err)=>    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }


}
