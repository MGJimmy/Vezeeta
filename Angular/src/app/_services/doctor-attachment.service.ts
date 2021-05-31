import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctorAttachment } from '../_models/_interfaces/IDoctorAttachments';

@Injectable({
  providedIn: 'root'
})
export class DoctorAttachmentService {

  constructor(private _http:HttpClient) { }

  url=`${environment.apiUrl}/api/DoctorAttachment`;
  
  

  InsertAttachment(dto:IDoctorAttachment):Observable<IDoctorAttachment>{
    return this._http.post<IDoctorAttachment>(this.url,dto).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }))
  }
}
