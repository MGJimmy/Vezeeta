import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';``
import { IClinicServices } from '../_models/_interfaces/IClinicService';

@Injectable({
  providedIn: 'root'
})
export class ClincServicesService {

  constructor( private _http : HttpClient  ) { }
  url = environment.apiUrl+'/api/ClincServices'; 

  getAllClinicServices():Observable<IClinicServices[]> {
    return this._http.get<IClinicServices[]>(this.url).pipe(
    catchError(
       (err)=> { 
       return throwError(err.message ||"Internal Server error contact site adminstarator");
       }))
  }
  getAllAcceptedClinicServices():Observable<IClinicServices[]> {
    return this._http.get<IClinicServices[]>(`${this.url}/getAccepted`).pipe(
    catchError(
       (err)=> { 
       return throwError(err.message ||"Internal Server error contact site adminstarator");
       }))
  }

  
  getClinicServicesById(id:number):Observable<IClinicServices>{
    let _url = `${this.url}/${id}`
    return this._http.get<IClinicServices>(_url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  addNewClinicService(newService:IClinicServices):Observable<IClinicServices>{
  
    return this._http.post<IClinicServices>(this.url, newService)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateClinicService(id:number, clinicService:IClinicServices):Observable<IClinicServices>{
    let _url = `${this.url}/${id}`;
    console.log(_url);
    return this._http.put<IClinicServices>(_url,clinicService)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  deleteClinicService(id:number):Observable<any>{
    let _url = `${environment.apiUrl}/api/ClincServices/${id}`;
    return this._http.delete<any>(_url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  
  getClinicServicesByPage(pageSize:number, pageNumber:number):Observable<IClinicServices[]>{
    let url = `${this.url}/${pageSize}/${pageNumber}`;
    return this._http.get<IClinicServices[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  getClnicServicesCount():Observable<number>{
    let url = `${this.url}/count`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }


}
