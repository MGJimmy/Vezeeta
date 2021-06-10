import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IClinic } from '../_models/_interfaces/IClinic';
import { IClinicServices } from '../_models/_interfaces/IClinicService';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

  constructor(private _http:HttpClient) { }
  url=environment.apiUrl+"/api/Clinic";

  // /api/Clinic/Myclinic
  AddClinic(clnc)
  {
    return this._http.post<IClinic>(this.url,clnc).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  GetMyClinic()
  {
    return this._http.get<IClinic>(this.url+"/Myclinic").pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  UpdateClinic(clnc)
  {
    return this._http.put<any>(this.url,clnc).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addClinicServicesToClinic(clinicServices:IClinicServices[]){
    return this._http.post<IClinic>(`${this.url}/addClinicServicesToClinic`,clinicServices).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    })); 
  }
  getClinicServicesForClinic():Observable<IClinicServices[]>{

    return this._http.get<IClinicServices[]>(`${this.url}/getClinicServices`).pipe(catchError(
      (err)=> { 
      return throwError(err.message ||"Internal Server error contact site adminstarator");
      }))
  }
}
