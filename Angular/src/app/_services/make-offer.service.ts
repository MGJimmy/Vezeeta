import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IMakeOffer } from '../_models/_interfaces/IMakeOffer';
import { IMakeOfferWithDoctorInfo } from '../_models/_interfaces/IMakeOfferWithDoctorInfo';

@Injectable({
  providedIn: 'root'
})
export class MakeOfferService {

  constructor(private _http:HttpClient) { }

  url=`${environment.apiUrl}/api/MakeOffer`;

  GetAll():Observable<IMakeOfferWithDoctorInfo[]>{
    return this._http.get<IMakeOfferWithDoctorInfo[]>(this.url).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  GetAllRelatedToOfferId(id:number,pageSize,pageNumber):Observable<IMakeOfferWithDoctorInfo[]>{
    return this._http.get<IMakeOfferWithDoctorInfo[]>(`${this.url}/RelatedToOfferCategory/${id}/${pageSize}/${pageNumber}`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  GetAllRelatedToSubOfferId(subOfferId:number,pageSize,pageNumber):Observable<IMakeOfferWithDoctorInfo[]>{
    return this._http.get<IMakeOfferWithDoctorInfo[]>(`${this.url}/RelatedToSubOfferCategory/${subOfferId}/${pageSize}/${pageNumber}`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  GetAllRelatedToDoctor():Observable<IMakeOffer[]>{
    return this._http.get<IMakeOffer[]>(`${this.url}/relatedToDoctor`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  
  GetById(id:number):Observable<IMakeOfferWithDoctorInfo>{
    return this._http.get<IMakeOfferWithDoctorInfo>(`${this.url}/${id}`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }

  createMakeOffer(makeOffer:IMakeOffer):Observable<IMakeOffer>{
    return this._http.post<IMakeOffer>(this.url,makeOffer).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  UpdateMakeOffer(makeOffer:IMakeOffer):Observable<any>{
    return this._http.put<any>(this.url,makeOffer).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }

  GetCountOfMakeOfferRelatedToOffer(offerId:number):Observable<number>{
    return this._http.get<number>(`${this.url}/CountOfMakeOfferRelatedToOffer/${offerId}`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }
  GetCountOfMakeOfferRelatedToSubOffer(subOfferId:number):Observable<number>{
    return this._http.get<number>(`${this.url}/CountOfMakeOfferRelatedToSubOffer/${subOfferId}`).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }));
  }

}
