export interface IReserveOfferShowToPatient{
    reserveOfferId:number,
    makeOfferTitle:string,
    doctorName:string,
    clinicArea:string,
    clinicStreeet:string,
    date:string,
    state:boolean,
    dayShiftFrom?:string,
    dayShiftTo?:string,
    isRated?:boolean
}