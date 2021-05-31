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
  getDoctorAttachment(isAccepted:boolean):Observable<IDoctorAttachment[]> {
    let url = `${environment.apiUrl}/api/DoctorAttachment/${isAccepted}`;
    return this._http.get<IDoctorAttachment[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  getDoctorAttachmentCount():Observable<number>{
    let url = `${environment.apiUrl}/api/DoctorAttachment/count`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getDoctorAttachmentByPage(pageSize:number, pageNumber:number):Observable<IDoctorAttachment[]>{
    let url = `${environment.apiUrl}/api/DoctorAttachment/${pageSize}/${pageNumber}`;
    return this._http.get<IDoctorAttachment[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
 
  acceptDoctorAttachment(doctorId:string):Observable<any>{
    let url = `${environment.apiUrl}/api/DoctorAttachment/acceptAttachments/${doctorId}`;
    return this._http.put<any>(url,null).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
   
  rejectDoctorAttachment(doctorId:string):Observable<any>{
    let url = `${environment.apiUrl}/api/DoctorAttachment/rejecteAttachments/${doctorId}`;
    return this._http.put<any>(url,null).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  url=`${environment.apiUrl}/api/DoctorAttachment`;
  
  

  InsertAttachment(dto:IDoctorAttachment):Observable<IDoctorAttachment>{
    return this._http.post<IDoctorAttachment>(this.url,dto).pipe(catchError(err=>{
      return throwError(err.message||"an error occur");
    }))
  }
}
