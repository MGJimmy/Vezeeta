export interface IReservationShowToPatient{
    reservetionId:number,
    doctorName:string,
    clinicArea:string,
    clinicStreeet:string,
    date:string,
    state:boolean,
    isRated?:boolean,
    
    dayShiftFrom?:string,
    dayShiftTo?:string
}