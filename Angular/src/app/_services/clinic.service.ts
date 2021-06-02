import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IClinic } from '../_models/_interfaces/IClinic';

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
      console.log(err);
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
 
}
