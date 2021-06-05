import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDoctorService } from '../_models/_interfaces/IDoctorService';


@Injectable({
  providedIn: 'root'
})
export class DoctorServicesService {

  constructor(private _http:HttpClient) { }
  getAllDoctorServices():Observable<IDoctorService[]> {
    let url = `${environment.apiUrl}/api/DoctorService`;
    return this._http.get<IDoctorService[]>(url).pipe(catchError((err)=>
    {
      console.log(err);
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getDoctorServicesCount(ByAdmin:boolean):Observable<number>{
    let url = `${environment.apiUrl}/api/DoctorService/count/${ByAdmin}`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getDoctorServicesByPage(pageSize:number, pageNumber:number,ByAdmin:boolean):Observable<IDoctorService[]>{
    let url = `${environment.apiUrl}/api/DoctorService/${pageSize}/${pageNumber}/${ByAdmin}`;
    console.log(url);
    return this._http.get<IDoctorService[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getDoctorServiceById(id:number):Observable<IDoctorService>{
    let url = `${environment.apiUrl}/api/DoctorService/${id}`;
    
    return this._http.get<IDoctorService>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addNewDoctorService(newDoctorService:IDoctorService):Observable<IDoctorService>{
  
    let url = `${environment.apiUrl}/api/DoctorService`;
    console.log(url)
    return this._http.post<IDoctorService>(url, newDoctorService)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateDoctorService(DoctorServiceToUpdate:IDoctorService):Observable<any>{
    let url = `${environment.apiUrl}/api/DoctorService`;
    return  this._http.put<any>(url, DoctorServiceToUpdate)
           
  }
  deleteDoctorService(id:number):Observable<any>{
    let url = `${environment.apiUrl}/api/DoctorService/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}
