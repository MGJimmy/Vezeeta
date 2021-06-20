export interface IOfferWithSubOffer{
    id?:number,
    name:string,
    image:string,
    subOffers:ISubOffer[]
}
interface ISubOffer{
    id?:number,
    name:string
}