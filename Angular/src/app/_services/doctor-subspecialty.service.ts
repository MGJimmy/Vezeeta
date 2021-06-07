import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISubSpecialty } from '../_models/_interfaces/ISubSpeciality';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DoctorSubspecialtyService {

  constructor(private _http:HttpClient) { }
  url=`${environment.apiUrl}/api/DoctorSubSpecialization`;

  
 getDoctorSubSpecialty():Observable<ISubSpecialty[]>{

  return this._http.get<ISubSpecialty[]>(this.url).pipe(catchError(
    (err)=> { 
    return throwError(err.message ||"Internal Server error contact site adminstarator");
    }))
}
insertDoctorSuubSpecialty(subSpecialty:ISubSpecialty[]):Observable<any>{
  return this._http.post<any>(this.url,subSpecialty).pipe(catchError(
    (err)=> { 
    return throwError(err.message ||"Internal Server error contact site adminstarator");
    }))
}

updateDoctorSuubSpecialty(subSpecialty:ISubSpecialty[]):Observable<any>{
  return this._http.put<any>(this.url,subSpecialty).pipe(catchError(
    (err)=> { 
    return throwError(err.message ||"Internal Server error contact site adminstarator");
    }))
}


}
