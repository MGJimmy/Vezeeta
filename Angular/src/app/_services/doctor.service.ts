import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctorWithSubSpecialty } from '../_models/_interfaces/IDoctorWithSubSpecialty';
import { IRegisterDoctor } from '../_models/_interfaces/IRegisterDoctor';
import { ISpecialty } from '../_models/_interfaces/ISpecilaty';
import { ISubSpecialty } from '../_models/_interfaces/ISubSpeciality';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private _http:HttpClient) { }
  url=`${environment.apiUrl}/api/Doctor`;

  getCurrentDoctor():Observable<IRegisterDoctor>{

    return this._http.get<IRegisterDoctor>(this.url).pipe(catchError(
      (err)=> { 
      return throwError(err.message ||"Internal Server error contact site adminstarator");
      }))
  }
 

  assignSpecialtyToDoctor(specialty:ISpecialty):Observable<ISpecialty>{
    return this._http.post<any>(this.url+"/assignSpecialty",specialty).pipe(catchError(
      (err)=> { 
      return throwError(err.message ||"Internal Server error contact site adminstarator");
      }))
  }



}
