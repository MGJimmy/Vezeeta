import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ISubSpecialty } from '../_models/_interfaces/ISubSpeciality';

@Injectable({
  providedIn: 'root'
})
export class SubSpecialityService {

  constructor(private _http:HttpClient) { }
    url=environment.apiUrl+"/api/SupSpecialization";
    baseUrl=environment.apiUrl+"/api/";
    getAllSubSpeciality():Observable<ISubSpecialty[]> {
      return this._http.get<ISubSpecialty[]>(this.url).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    getAllSubSpecialityBySpecialtyId(id,byAdmin:boolean):Observable<ISubSpecialty[]> {
      return this._http.get<ISubSpecialty[]>(this.url+"/getWhere/"+id+"/"+byAdmin).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    getSubSpecialityById(id):Observable<ISubSpecialty> {
      return this._http.get<ISubSpecialty>(this.url+"/"+id).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }

    getSubSpecialitiesCount():Observable<number>{
      return this._http.get<number>(this.url+"/count").pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    getSubSpecialitiesByPage(pageSize:number, pageNumber:number):Observable<ISubSpecialty[]>{
      let newurl = `${environment.apiUrl}/api/SupSpecialization/${pageSize}/${pageNumber}`;
      return this._http.get<ISubSpecialty[]>(newurl).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    deleteSubSpecialty(id):Observable<any> {
      let newUrl=environment.apiUrl+"/api/SupSpecialization/"+id;
      return this._http.delete<any>(newUrl).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }

    addNewSubSpecialty(SubSpecial):Observable<ISubSpecialty>
    {
      return this._http.post<ISubSpecialty>(this.url,SubSpecial).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    /*********/
    addListOfSubSpecialty(SubSpecial):Observable<ISubSpecialty[]>{
      return this._http.post<ISubSpecialty[]>(this.url+"/insertList",SubSpecial).pipe(catchError((err)=>
      {
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }));
    }
    /********* */
    updateSubSpecialty(id:number,SubSpecial:ISubSpecialty):Observable<any>
    {
      return this._http.put<any>(this.url+"/"+id,SubSpecial)
    }


    getAllByPage(pageSize: number, pageNumber: number): Observable<ISubSpecialty[]> {
      return this._http.get<ISubSpecialty[]>(this.url + "/" + pageSize + "/" + pageNumber)
        .pipe(catchError((err) => {
          return throwError(err.message || "An Error Occur")
        })
        )
    }
 
  
    getAllByPageOfNotAccepted(pageSize: number, pageNumber: number): Observable<ISubSpecialty[]> {
      return this._http.get<ISubSpecialty[]>(this.baseUrl + "NotAcceptedSubSpecail/Admin/"+pageSize+"/"+pageNumber)
        .pipe(catchError((err) => {
          return throwError(err.message || "An Error Occur")
        })
        )
    }

  countOfAcceptSubspecialization(): Observable<number> {
      return this._http.get<number>(this.url+"/count").pipe(catchError((err) => {
        return throwError(err.message || "An Error Occur")
      })
      )
    }
    countOfNotAcceptSubspecialization(): Observable<number> {
      return this._http.get<number>(this.baseUrl + "SubspecailNotAccepted/admin/count").pipe(catchError((err) => {
        return throwError(err.message || "An Error Occur")
      })
      )
    }
    
    
}
