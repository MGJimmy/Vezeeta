import { ISubSpecialty } from "./ISubSpeciality";

export interface IDoctorSearch {

  UserId: string;
  DoctorName: string;
  Image: string;
  TitleDegree: string
  specialtyName : string
  CityName : string
  AreaName : string 
  Street : string
  doctorInfo: string;
  IsAccepted: boolean;
  specialtyId: number;
  CityId: number;
  AreaId: number;
  Fees: number;
  WatingTime: number;
  ExmantionTime : number
  supSpecializations : Array<ISubSpecialty>


 
  
  

  



  


  



}