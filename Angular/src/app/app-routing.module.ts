import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CityComponent } from './components/dashboard/city/city.component';
import { ClinicServicesComponent } from './components/dashboard/clinic-services/clinic-services.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DoctorAttachmentsComponent } from './components/dashboard/doctor-attachments/doctor-attachments.component';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';
import { DoctorClincComponent } from './components/doctor-dashboard/doctor-clinc/doctor-clinc.component';
import { ClinicWorkingDaysComponent } from './components/doctor-dashboard/clinic-working-days/clinic-working-days.component';
import { DoctorDashboardAttachmentComponent } from './components/doctor-dashboard/doctor-dashboard-attachment/doctor-dashboard-attachment.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterDoctorComponent } from './components/register-doctor/register-doctor.component';
import { DoctorServicesComponent } from './components/doctor-dashboard/doctor-services/doctor-services.component';
import { DocotorSpecialtyComponent } from './components/doctor-dashboard/docotor-specialty/docotor-specialty.component';
import { AuthGuard } from './_helpers/auth.guard';
import { ManageDoctorServicesComponent } from './components/dashboard/manage-doctor-services/manage-doctor-services.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { CreatReservationComponent } from './components/client/creat-reservation/creat-reservation.component';
import { CreatReservationContinueComponent } from './components/client/creat-reservation-continue/creat-reservation-continue.component';
import { ShowReservationToPatientComponent } from './components/client/show-reservation-to-patient/show-reservation-to-patient.component';
import { ShowReservationToDoctorComponent } from './components/client/show-reservation-to-doctor/show-reservation-to-doctor.component';
import { OfferComponent } from './components/dashboard/offer/offer.component';
import { SubOfferComponent } from './components/dashboard/sub-offer/sub-offer.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ShowDoctorsPageComponent } from './components/show-doctors-page/show-doctors-page.component';
import { DoctorMakeOfferComponent } from './components/doctor-dashboard/doctor-make-offer/doctor-make-offer.component';
import { ClientOfferComponent } from './components/client-offer/client-offer.component';
import { HomeOfferComponent } from './components/client-offer/home-offer/home-offer.component';
import { OfferDetailsComponent } from './components/client-offer/offer-details/offer-details.component';
import { ShowDoctorDetailsComponent } from './components/client/show-doctor-details/show-doctor-details.component';
import { ShowOfferReserveToPatientComponent } from './components/client-offer/show-offer-reserve-to-patient/show-offer-reserve-to-patient.component';
import { ShowOfferReserveToDoctorComponent } from './components/client-offer/show-offer-reserve-to-doctor/show-offer-reserve-to-doctor.component';
import { ClientComponent } from './components/client/client.component';
import { OfferCategoryComponent } from './components/client-offer/offer-category/offer-category.component';
import { SubofferCategoryComponent } from './components/client-offer/suboffer-category/suboffer-category.component';
import { RegisterAdminComponent } from './components/register-admin/register-admin.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';
import { UserInformationComponent } from './components/user-information/user-information.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { ResetPasswordComponent } from './components/user-information/reset-password/reset-password.component';
import { ClientRateComponent } from './components/client/client-rate/client-rate.component';
import { ResetForgetPasswordComponent } from './components/reset-forget-password/reset-forget-password.component';

const routes: Routes = [
  {
    path:'dashboard',component:DashboardComponent,
    children:[
      {path: 'cities', component: CityComponent},
      {path: 'specialists', component: SpecialityComponent},
      {path: 'SubSpecialists', component: SubSpecialityComponent}, 
      {path: 'area', component: AreaComponent},
      {path:'clinicServices' , component:ClinicServicesComponent},
      {path: 'doctorAttachments', component: DoctorAttachmentsComponent},
      {path: 'doctorServices', component: ManageDoctorServicesComponent},
      {path:"offer",component:OfferComponent},
      {path:"subOffer",component:SubOfferComponent},
    
    ]
  },
  {path:'doctorDashboard',component:DoctorDashboardComponent,

    children:[
      {path:'attachment',component:DoctorDashboardAttachmentComponent},
      {path:'clinc',component:DoctorClincComponent},
      {path:'attachment',component:DoctorDashboardAttachmentComponent},
      {path:'workingDays',component:ClinicWorkingDaysComponent},
      {path:'doctorServices',component:DoctorServicesComponent,canActivate:[AuthGuard]},
      {path:'clinc',component:DoctorClincComponent, canActivate:[AuthGuard]},
      {path:'workingDays',component:ClinicWorkingDaysComponent, canActivate:[AuthGuard]},
      {path:'doctorSpecialty',component:DocotorSpecialtyComponent},
      {path:'doctorMakeOffer',component:DoctorMakeOfferComponent},
    ]
  },

  {path:"ClientOffer",component:ClientOfferComponent
    ,children:[
      {path:"offers",component:HomeOfferComponent},
      {path:"offers/:id",component:OfferCategoryComponent},
      {path:"subOffers/:id",component:SubofferCategoryComponent},
      {path:"OfferDetails",component:OfferDetailsComponent},
      {path:"UserOfferAppointments",component:ShowOfferReserveToPatientComponent},
      {path:"DoctorOfferAppointments",component:ShowOfferReserveToDoctorComponent},
    ]
  },

  {path:"", component:ClientComponent ,
    children:[
      {path:"",component:HomePageComponent},
      {path:"Reversation",component:CreatReservationComponent},
      {path:"ReversationContinue",component:CreatReservationContinueComponent},
      {path:"UserAppointments",component:ShowReservationToPatientComponent},
      {path:"DoctorAppointments",component:ShowReservationToDoctorComponent},      
      {path:"showDoctors", component:ShowDoctorsPageComponent},
      {path:"ShowDoctorDetails/:id", component:ShowDoctorDetailsComponent},
     
      // {path:"home", component:ClientComponent},
      // {path:"**", component:ClientComponent},
  ]},

  {path:"registerDoctor", component:RegisterDoctorComponent},
  {path:"registerUser", component:UserRegisterComponent},
  {path:"login", component:LoginComponent},
  {path:"forgetPassword",component:ForgetPasswordComponent},
  {path:"resetForgetPasswod",component:ResetForgetPasswordComponent},
  {path:"ResetPassword",component:ResetPasswordComponent},

  {path:"registeAdmin",component:RegisterAdminComponent},
  


  {path:'MyInformation',component:UserInformationComponent,

    children:[
      {path:"UserUpdate",component:UserUpdateComponent},
      {path:"ResetPassword",component:ResetPasswordComponent},     
    ]
  },
  {path:"RateDoctor/:ReservationId", component:ClientRateComponent},
  // {path:"Reversation",component:CreatReservationComponent},
  // {path:"ReversationContinue",component:CreatReservationContinueComponent},
  // {path:"UserAppointments",component:ShowReservationToPatientComponent},
  // {path:"DoctorAppointments",component:ShowReservationToDoctorComponent},
  
  // {path:"registerDoctor", component:RegisterDoctorComponent},
  // {path:"registerUser", component:UserRegisterComponent},
  // {path:"login", component:LoginComponent},
  // {path:"showDoctors", component:ShowDoctorsPageComponent},
  // {path:"ShowDoctorDetails/:id", component:ShowDoctorDetailsComponent},
  // {path:"home", component:ClientComponent},
  // {path:"**", component:ClientComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
