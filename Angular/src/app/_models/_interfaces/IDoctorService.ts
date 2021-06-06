export interface IDoctorService {
    id?: number,
    name: string,
    byAdmin: boolean,

}
export interface IDoctorServiceDisplay {
    id?: number,
    name: string,
    byAdmin: boolean,
    checked?:boolean,

}