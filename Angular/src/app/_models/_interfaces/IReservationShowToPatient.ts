export interface IReservationShowToPatient{
    reservetionId:number,
    doctorName:string,
    clinicArea:string,
    clinicStreeet:string,
    date:string,
    state:boolean,
    
    dayShiftFrom?:string,
    dayShiftTo?:string
}