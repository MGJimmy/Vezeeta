import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardHeaderComponent } from './components/dashboard/dashboard-header/dashboard-header.component';
import { DashboardSidebarComponent } from './components/dashboard/dashboard-sidebar/dashboard-sidebar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ConfirmModalComponent } from './components/_reusableComponents/confirm-modal/confirm-modal.component';
import { UploadComponent } from './components/_reusableComponents/upload/upload.component';
import { AuthInterceptor } from './_helpers/auth.interceptor';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CityComponent } from './components/dashboard/city/city.component';
import { ClinicServicesComponent } from './components/dashboard/clinic-services/clinic-services.component';
import { ClientComponent } from './components/client/client.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { ClientHeaderComponent } from './components/client/client-header/client-header.component';
import { ClientFooterComponent } from './components/client/client-footer/client-footer.component';
import { RegisterDoctorComponent } from './components/register-doctor/register-doctor.component';
import { LoginComponent } from './components/login/login.component';
import { DoctorAttachmentsComponent } from './components/dashboard/doctor-attachments/doctor-attachments.component';
import { DoctorDashboardSildbarComponent } from './components/doctor-dashboard/doctor-dashboard-sildbar/doctor-dashboard-sildbar.component';
import { DoctorDashboardAttachmentComponent } from './components/doctor-dashboard/doctor-dashboard-attachment/doctor-dashboard-attachment.component';
import { UploadAndShowComponent } from './components/_reusableComponents/upload-and-show/upload-and-show.component';
import { DoctorClincComponent } from './components/doctor-dashboard/doctor-clinc/doctor-clinc.component';
import { UploadMultipleComponent } from './components/_reusableComponents/upload-multiple/upload-multiple.component';
import { ClinicWorkingDaysComponent } from './components/doctor-dashboard/clinic-working-days/clinic-working-days.component';
import { DoctorServicesComponent } from './components/doctor-dashboard/doctor-services/doctor-services.component';
import { ManageDoctorServicesComponent } from './components/dashboard/manage-doctor-services/manage-doctor-services.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { DocotorSpecialtyComponent } from './components/doctor-dashboard/docotor-specialty/docotor-specialty.component';

// import {} from 'angular-ng-autocomplete';
import {AutocompleteLibModule} from 'angular-ng-autocomplete';
import { NgSelectModule } from '@ng-select/ng-select';

import { ChooseClinicServiceComponent } from './components/doctor-dashboard/choose-clinic-service/choose-clinic-service.component'

import { CreatReservationComponent } from './components/client/creat-reservation/creat-reservation.component';
import { CreatReservationContinueComponent } from './components/client/creat-reservation-continue/creat-reservation-continue.component';
import { ShowReservationToPatientComponent } from './components/client/show-reservation-to-patient/show-reservation-to-patient.component';
import { ShowReservationToDoctorComponent } from './components/client/show-reservation-to-doctor/show-reservation-to-doctor.component';
import { OfferComponent } from './components/dashboard/offer/offer.component';
import { SubOfferComponent } from './components/dashboard/sub-offer/sub-offer.component';

import { HomePageComponent } from './components/home-page/home-page.component';
import { ShowDoctorsPageComponent } from './components/show-doctors-page/show-doctors-page.component';
import { FilterDoctorsideBarComponent } from './components/show-doctors-page/filter-doctorside-bar/filter-doctorside-bar.component';
import { DoctorMakeOfferComponent } from './components/doctor-dashboard/doctor-make-offer/doctor-make-offer.component';
import { ClientOfferComponent } from './components/client-offer/client-offer.component';
import { OfferNavbarComponent } from './components/client-offer/offer-navbar/offer-navbar.component';
import { HomeOfferComponent } from './components/client-offer/home-offer/home-offer.component';
import { OfferDetailsComponent } from './components/client-offer/offer-details/offer-details.component'
import { ShowDoctorDetailsComponent } from './components/client/show-doctor-details/show-doctor-details.component';
import { ReserveOfferComponent } from './components/client-offer/reserve-offer/reserve-offer.component';
import { ShowOfferReserveToPatientComponent } from './components/client-offer/show-offer-reserve-to-patient/show-offer-reserve-to-patient.component';
import { ShowOfferReserveToDoctorComponent } from './components/client-offer/show-offer-reserve-to-doctor/show-offer-reserve-to-doctor.component'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SearchComponent } from './components/search/search.component';
import { FilterDoctorDataComponent } from './components/show-doctors-page/filter-doctor-data/filter-doctor-data.component';
import { OfferCategoryComponent } from './components/client-offer/offer-category/offer-category.component';
import { SubofferCategoryComponent } from './components/client-offer/suboffer-category/suboffer-category.component';
import { NewestOfferComponent } from './components/client-offer/home-offer/newest-offer/newest-offer.component';
import { CarsoulOfferComponent } from './components/client-offer/home-offer/carsoul-offer/carsoul-offer.component'
import { UserUpdateComponent } from './components/user-update/user-update.component'
import { RegisterAdminComponent } from './components/register-admin/register-admin.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';
import { UserInformationComponent } from './components/user-information/user-information.component';
import { UserInfoSideBarComponent } from './components/user-information/user-info-side-bar/user-info-side-bar.component';
import { ResetPasswordComponent } from './components/user-information/reset-password/reset-password.component';
import { ClientRateComponent } from './components/client/client-rate/client-rate.component';
import { BarRatingModule } from 'ngx-bar-rating';
import { SuggestionDoctorsComponent } from './components/client/suggestion-doctors/suggestion-doctors.component';
import { ResetForgetPasswordComponent } from './components/reset-forget-password/reset-forget-password.component';
import { OfferRatingComponent } from './components/client-offer/offer-rating/offer-rating.component';
import { SugestionMakeOfferComponent } from './components/client-offer/sugestion-make-offer/sugestion-make-offer.component';
import { DoctorCheckIsAcceptComponent } from './components/doctor-dashboard/doctor-check-is-accept/doctor-check-is-accept.component';
import { SearchInHomePageComponent } from './components/client/search-in-home-page/search-in-home-page.component';
import { ExtraInformationComponent } from './extra-information/extra-information.component';
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    DashboardHeaderComponent,
    DashboardSidebarComponent,
    UploadComponent,
    ConfirmModalComponent,
    SpecialityComponent,
    SubSpecialityComponent,
    AreaComponent,
    CityComponent,
    ClinicServicesComponent,
    ClientComponent,
    DoctorDashboardComponent,
    ClientHeaderComponent,
    ClientFooterComponent,
    RegisterDoctorComponent,
    LoginComponent,
    DoctorAttachmentsComponent,
    DoctorDashboardSildbarComponent,
    DoctorDashboardAttachmentComponent,
    UploadAndShowComponent,
    DoctorClincComponent,
    UploadMultipleComponent,
    ClinicWorkingDaysComponent,
    DoctorServicesComponent,
    ManageDoctorServicesComponent,
    UserRegisterComponent,

    DocotorSpecialtyComponent,
      CreatReservationComponent,
      CreatReservationContinueComponent,
      ShowReservationToPatientComponent,
      ShowReservationToDoctorComponent,
      OfferComponent,
      SubOfferComponent,
      HomePageComponent,
      ShowDoctorsPageComponent,
      FilterDoctorsideBarComponent,
      DoctorMakeOfferComponent,
      ClientOfferComponent,
      OfferNavbarComponent,
      HomeOfferComponent,
      OfferDetailsComponent,
      ShowDoctorDetailsComponent,
      ReserveOfferComponent,
      ShowOfferReserveToPatientComponent,
      ShowOfferReserveToDoctorComponent,
      SearchComponent,
      FilterDoctorDataComponent,
      OfferCategoryComponent,
      SubofferCategoryComponent,
      NewestOfferComponent,
      CarsoulOfferComponent,
      UserUpdateComponent,
      ResetPasswordComponent,
      RegisterAdminComponent,
      ForgetPasswordComponent,
      UserInformationComponent,
      UserInfoSideBarComponent,
      ClientRateComponent,
      SuggestionDoctorsComponent,
      ChooseClinicServiceComponent,
      ResetForgetPasswordComponent,
      OfferRatingComponent,
      SugestionMakeOfferComponent,
      DoctorCheckIsAcceptComponent,
      SearchInHomePageComponent,
      ExtraInformationComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    AutocompleteLibModule,
    NgSelectModule,
    BarRatingModule,
    // BrowserAnimationsModule,
    // CarouselModul,
    // NgxSpinnerModule,
    // BrowserAnimationsModule,
    // NgbModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
