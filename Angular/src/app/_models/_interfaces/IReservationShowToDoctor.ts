export interface IReservationShowToDoctor{
    reservetionId:number,
    userId:string,
    state:boolean,
    userName:string,
    phone:string,
    date:string,
    email:string,
    age?:number,
    gender?:string, 
    Symptoms?:string, 
    
    dayShiftFrom?:string,
    dayShiftTo?:string
}