export interface IDoctorAttachment{
  doctorId:string,
  PersonalIdImage:string,
  DoctorSyndicateIdImage:string,
  OpenClinicPermissionImage:string,
  isBinding:boolean,
}
export interface IDoctorAttachmentGetOne{
  doctorId:string,
  personalIdImage:string,
  doctorSyndicateIdImage:string,
  openClinicPermissionImage:string,
  isBinding:boolean,
  doctorIsAccepted:boolean,
  rejected?:boolean,
}


