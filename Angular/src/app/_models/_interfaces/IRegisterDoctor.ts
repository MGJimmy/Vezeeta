import { ISubSpecialty } from "./ISubSpeciality";

export interface IRegisterDoctor{
  fullName:string,
  userName:string,
  phoneNumber:string,
  email:string,
  passwordHash:string,
  confirmPassword:string,
  doctorInfo:string,
  titleDegree:string,
  image:string,
  specialtyId:number
}