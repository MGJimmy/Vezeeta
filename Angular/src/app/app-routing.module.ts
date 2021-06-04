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
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent,
    canActivate:[AuthGuard],
    canActivateChild:[AuthGuard],
    children:[
      {path: 'cities', component: CityComponent,canActivate:[AuthGuard]},
      {path: 'specialists', component: SpecialityComponent,canActivate:[AuthGuard]},
      {path: 'SubSpecialists', component: SubSpecialityComponent,canActivate:[AuthGuard]}, 
      {path: 'area', component: AreaComponent,canActivate:[AuthGuard]},
      {path:'clinicServices' , component:ClinicServicesComponent,canActivate:[AuthGuard] },
      {path: 'doctorAttachments', component: DoctorAttachmentsComponent,canActivate:[AuthGuard]},
    
    ]
  },
  {path:'doctorDashboard',component:DoctorDashboardComponent,
  canActivate:[AuthGuard],
  canActivateChild:[AuthGuard],
    children:[
      {path:'attachment',component:DoctorDashboardAttachmentComponent},
      {path:'clinc',component:DoctorClincComponent, canActivate:[AuthGuard]},
      {path:'workingDays',component:ClinicWorkingDaysComponent, canActivate:[AuthGuard]},
    ]
  },
  {path:"registerDoctor", component:RegisterDoctorComponent},
  {path:"login", component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
