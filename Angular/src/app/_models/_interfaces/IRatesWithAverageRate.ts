export interface IRates{
   
    allrates:getRate[],
    averagerate:number

}

export interface getRate{
    comment:string,
    userFullName:string,
    rate:number,
    date:string,
    doctorId:string,
    reservationId:number,
    userId:string

}



