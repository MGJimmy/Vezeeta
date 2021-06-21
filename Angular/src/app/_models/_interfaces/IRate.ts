export interface IRate {
    reservationId: number,
    comment: string,
    userId?: string,
    doctorId?: string,
    rate: number,
    date: string
}
