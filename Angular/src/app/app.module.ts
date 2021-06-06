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
import { DocotorSpecialtyComponent } from './components/doctor-dashboard/docotor-specialty/docotor-specialty.component';

// import {} from 'angular-ng-autocomplete';
import {AutocompleteLibModule} from 'angular-ng-autocomplete';
import { NgSelectModule } from '@ng-select/ng-select';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
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

    DocotorSpecialtyComponent,
    
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
