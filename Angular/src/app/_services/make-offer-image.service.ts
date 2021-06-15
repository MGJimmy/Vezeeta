import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IMakeOfferImage } from '../_models/_interfaces/IMakeOfferImage';

@Injectable({
  providedIn: 'root'
})
export class MakeOfferImageService {

  constructor(private _http:HttpClient) { }

  url=`${environment.apiUrl}/api/MakeOfferImage`;

  create(images:IMakeOfferImage[]):Observable<any>{
    return this._http.post<any>(this.url,images).pipe(catchError(err=>{
      return throwError(err.message||"an error occur in server");
    }))
  }
}
