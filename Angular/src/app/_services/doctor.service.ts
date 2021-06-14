import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctorDetails } from '../_models/_interfaces/IDoctorDetails';
import { IDoctorWithClinic } from '../_models/_interfaces/IDoctorWithClinic';
import { IDoctor } from '../_models/_interfaces/IDoctorPresentaion';
import { IDoctorSearch } from '../_models/_interfaces/IDoctorSearch';
import { IDoctorWithSubSpecialty } from '../_models/_interfaces/IDoctorWithSubSpecialty';
import { IRegisterDoctor } from '../_models/_interfaces/IRegisterDoctor';
import { ISpecialty } from '../_models/_interfaces/ISpecilaty';
import { ISubSpecialty } from '../_models/_interfaces/ISubSpeciality';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private _http: HttpClient) { }
  url = `${environment.apiUrl}/api/Doctor`;

  getCurrentDoctor():Observable<IRegisterDoctor>{

    return this._http.get<IRegisterDoctor>(this.url).pipe(catchError(
      (err) => {
        return throwError(err.message || "Internal Server error contact site adminstarator");
      }))
  }
  getCurrentDoctorWithSubSpecialty():Observable<ISubSpecialty[]>{

  getDoctorWithClinicDetails(doctorName:string):Observable<IDoctorWithClinic>{
    return this._http.get<IDoctorWithClinic>(this.url+"/GetWithClinicForReservetionCart/"+doctorName).pipe(catchError(
      (err)=> { 
      return throwError(err.message ||"Internal Server error contact site adminstarator");
      }))
  }
 
  getDoctorByName(doctorName:string):Observable<IDoctorDetails>{
    return this._http.get<IDoctorDetails>(this.url+"/doctorByName/"+doctorName).pipe(catchError(
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

  search(pageSize: number, pageNumber: number, specialtyId: number, cityId: number, areaId: number, name: string): Observable<IDoctorSearch[]> {

    let _url = `${this.url}/search/${pageSize}/${pageNumber}`

    let params = new HttpParams();

    if (specialtyId != null) {
      params = params.set('specialtyId', specialtyId.toString());
    }
    if (cityId != null) {
      params = params.set('cityId', cityId.toString());
    }
    if (areaId != null) {
      params = params.set('areaId', areaId.toString());
    }
    if (name != null) {
      params = params.set('name', name);
    }

    //this.url+'/search?specialtyId='+specId +'&cityId='+ciId+'&areaId='+arId+'&name='+name 

    return this._http.get<IDoctorSearch[]>(_url, { params }).pipe(
      catchError(
        (err) => {
         
          return throwError(err.message || "Internal Server error contact site adminstarator");
          
        }
      )
    )

  }




  //doctor_DoctorServices
  addservice(services)
  {
   return this._http.post<any>(`${environment.apiUrl}/api/Doctor/addServices`, services)
   .pipe(catchError((err) => {
       console.log(err);
       return throwError(err.message || "An Error Occur")
     })
   )}
   GetMyservices()
   {
    return this._http.get<any>(`${environment.apiUrl}/api/Doctor/Myservices`)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    )}

Updateservices(services)
   {
    return this._http.put<any>(`${environment.apiUrl}/api/Doctor/UpdateDoctorServices`,services)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    )}

    /// show doctors

  ShowSpecailtyDoctors(SpecailtyId)
   {
    return this._http.get<IDoctor[]>(`${environment.apiUrl}/api/Doctor/GetAllWhere/${SpecailtyId}`)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    )}

  ShowDoctorDetails(DoctorId)
   {
    return this._http.get<IDoctor>(`${environment.apiUrl}/api/Doctor/DoctorDetails/${DoctorId}`)
    .pipe(catchError((err) => {
        console.log(err);
        return throwError(err.message || "An Error Occur")
      })
    )}



}
