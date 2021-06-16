import { Time } from "@angular/common";
import { IClinicImage } from "./IClinicImage";

export interface IDoctor {
    userId: string,
    image: string,
    titleDegree: string,
    doctorInfo: string,
    isAccepted: boolean,
    specialtyId: number,
    user:_User,
    specialty:_Specialty,
    services:_Service[],
    subspecails:_Subspecail[],
    clinic:_Clinic,
    workingDays:_WorkingDay[],
    clinicCityName: string,
    clinicAreaName: string,
    presentDaysWork?:IdoctorDayWork[],
    clinic_Images?: IClinicImage[]
    

}


export class _User {
    fullName: string;
    phoneNumber: string;
    userName: string
}
export class _Specialty {
    id: number;
    name: string;
}
export class _Service {
    id: number;
    name: string;
    byAdmin: boolean
}

export class _Subspecail {
    id: number;
    name: string;
    byAdmin: boolean;
    specialtyId: number;
}

export class _Clinic {
    doctorId: string;
    street: string;
    fees: number;
    examinationTime: number;
    watingTime: number;
    cityId: number;
    areaId: number;

}


export interface _WorkingDay {
    id: number;
    clinicId: string;
    day: string;
    dayShifts:_DayShift[];
}

export enum Days {
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}
export interface _DayShift {
    id: number;
    from: Time;
    to: Time;
    maxNumOfReservation: number;
    workingDayId: number;
}

export interface IdoctorDayWork
{
    clinicId?:string,
    datee:string,
    IsWork?:boolean,
    _dayShift?:_DayShift[],
}

export interface _DayShiftsforDoctor
{
    doctorId:string,
    workingDays:_WorkingDay[],
}



export interface _IdoctorFilter
{
    specailtyid?:number,
    title?:string[],
    fee?:feelimit[],
    subspecails?:string[]

}
 export interface feelimit{
    MiniMoney:number;
    MaxMoney:number;
} 




  