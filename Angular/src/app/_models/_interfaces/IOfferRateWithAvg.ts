import { IOfferRateGet } from "./IOfferRateGet";

export interface IOfferRateWithAvg{
    
    GetRatingDtos:IOfferRateGet[],
    averagerate:number
}