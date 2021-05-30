import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { ICity } from '../_models/_interfaces/ICity';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  constructor(private _http:HttpClient) { }

  getAllCities():Observable<ICity[]> {
    let url = `${environment.apiUrl}/api/city`;
    return this._http.get<ICity[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCityById(id:number):Observable<ICity>{
    let url = `${environment.apiUrl}/api/city/${id}`;
    return this._http.get<ICity>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addNewCity(newCity:ICity):Observable<ICity>{
    let url = `${environment.apiUrl}/api/city`;
    return this._http.post<ICity>(url, newCity)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateCity(id:number, cityToUpdate:ICity):Observable<ICity>{
    let url = `${environment.apiUrl}/api/city/${id}`;
    return this._http.put<ICity>(url, cityToUpdate)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  deleteCity(id:number):Observable<any>{
    let url = `${environment.apiUrl}/api/city/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCitiesCount():Observable<number>{
    let url = `${environment.apiUrl}/api/city/count`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCitiesByPage(pageSize:number, pageNumber:number):Observable<ICity[]>{
    let url = `${environment.apiUrl}/api/city/${pageSize}/${pageNumber}`;
    return this._http.get<ICity[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}