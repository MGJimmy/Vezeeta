import { IDayShift } from './IDayShift';

export interface IWorkingDay{
  clinicId:string,
  day:string,
  dayShifts:IDayShift[]
}