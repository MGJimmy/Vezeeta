import { IMakeOfferImage } from "./IMakeOfferImage";

export interface IMakeOffer{
    id?:number,
    title:string,
    numberOfSession:number,
    fees:number,
    discount:number,
    details:string,
    information:string,
    state:boolean,
    offerId:number,
    subOfferId:number,    
    offerName?:string,
    doctorId?:string,
    offerImages?:IMakeOfferImage[]
}