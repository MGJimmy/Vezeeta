export interface IArea{
    id?:number,
    name:string,
    byAdmin?:boolean,
    cityID:number
};

export interface IAreaWithArea{
    id:number,
    name:string,
    byAdmin:boolean,
    cityID:number,
    cityName:string
};