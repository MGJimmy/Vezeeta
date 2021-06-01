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

  AddClinic(clic)
  {
    return this._http.post<IClinic>(this.url,clic).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

}
