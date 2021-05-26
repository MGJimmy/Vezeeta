import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IArea, IAreaWithArea } from '../_models/_interfaces/IArea';

@Injectable({
  providedIn: 'root'
})
export class AreaService {

  constructor(private _http: HttpClient) { }
  baseUrl = environment.apiUrl + "/api/"
  AreaUrl=environment.apiUrl+"/api/area/";
  // AreaWithCityUrl=;

  getById(id): Observable<IArea> {
    return this._http.get<IArea>(this.AreaUrl + id).pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }

  getAll(): Observable<IAreaWithArea[]> {
    return this._http.get<IAreaWithArea[]>(this.AreaUrl).pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }

  getAllByPage(pageSize: number, pageNumber: number): Observable<IAreaWithArea[]> {
    return this._http.get<IAreaWithArea[]>(this.baseUrl + "AreaWithCity/" + pageSize + "/" + pageNumber)
      .pipe(catchError((err) => {
        return throwError(err.message || "An Error Occur")
      })
      )
  }
   
  getAllByPageOfNotAccepted(pageSize: number, pageNumber: number): Observable<IAreaWithArea[]> {
    return this._http.get<IAreaWithArea[]>(this.baseUrl + "AreaWithCity/admin/" + pageSize + "/" + pageNumber)
      .pipe(catchError((err) => {
        return throwError(err.message || "An Error Occur")
      })
      )
  }

  countOfAcceptAreas(): Observable<number> {
    return this._http.get<number>(this.baseUrl + "area/count").pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }
  countOfNotAcceptAreas(): Observable<number> {
    return this._http.get<number>(this.baseUrl + "area/admin/count").pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }

  insertArea(area:IArea):Observable<any>{
    return this._http.post<IArea>(this.AreaUrl,area).pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }

  updateArea(id:number,area:IArea):Observable<any>{
    return this._http.put<any>(this.AreaUrl+id,area).pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }

  deleteArea(id:number):Observable<any>{
    return this._http.delete<any>(this.AreaUrl+id).pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }



  //delete city
  getAllCity(){
    return this._http.get<ICity>(environment.apiUrl + "/api/city").pipe(catchError((err) => {
      return throwError(err.message || "An Error Occur")
    })
    )
  }
  // end delete


}
interface ICity{
  id:number
  name:string
}
