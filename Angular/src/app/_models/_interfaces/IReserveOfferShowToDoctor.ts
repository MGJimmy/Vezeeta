export interface IReserveOfferShowToDoctor{
    reservetionId:number,
    makeOfferTitle:string,
    offerId:number,
    userId:string,
    state:boolean,
    userName:string,
    phone:string,
    date:string,
    email:string,
    dayShiftFrom?:string,
    dayShiftTo?:string
}