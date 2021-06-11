export interface IReservation{
    id?:number,
    userName:string,
    phone:string,
    email:string,
    dayShiftId:number,
    date:string,
    state?:boolean,
    age?:Number,
    symptoms?:string,
    gender?:string,
    userId?:string,
    doctorId:string

}