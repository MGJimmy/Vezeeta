import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ISpecialty } from '../_models/_interfaces/ISpecilaty';

@Injectable({
  providedIn: 'root'
})
export class SpecilatyService {

  constructor(private _http:HttpClient) { }
  getAllCategories():Observable<ISpecialty[]> {
    let url = `${environment.apiUrl}/api/Specialty`;
    return this._http.get<ISpecialty[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getSpecialitiesCount():Observable<number>{
    let url = `${environment.apiUrl}/api/Specialty/count`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getSpecialitiesByPage(pageSize:number, pageNumber:number):Observable<ISpecialty[]>{
    let url = `${environment.apiUrl}/api/Specialty/${pageSize}/${pageNumber}`;
    return this._http.get<ISpecialty[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCategoryById(id:number):Observable<ISpecialty>{
    let url = `${environment.apiUrl}/api/Specialty/${id}`;
    return this._http.get<ISpecialty>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addNewSpecialty(newSpeciality:ISpecialty):Observable<ISpecialty>{
    let url = `${environment.apiUrl}/api/Specialty`;
    return this._http.post<ISpecialty>(url, newSpeciality)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateSpecialty(id:number, specialtyToUpdate:ISpecialty):Observable<any>{
    let url = `${environment.apiUrl}/api/Specialty/${id}`;
    return  this._http.put<any>(url, specialtyToUpdate)
           
  }
  deleteSpecialty(id:number):Observable<any>{
    let url = `${environment.apiUrl}/api/Specialty/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

}
