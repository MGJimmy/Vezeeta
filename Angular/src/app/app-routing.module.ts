import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CityComponent } from './components/dashboard/city/city.component';
import { ClinicServicesComponent } from './components/dashboard/clinic-services/clinic-services.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DoctorAttachmentsComponent } from './components/dashboard/doctor-attachments/doctor-attachments.component';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';
import { DoctorDashboardAttachmentComponent } from './components/doctor-dashboard/doctor-dashboard-attachment/doctor-dashboard-attachment.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterDoctorComponent } from './components/register-doctor/register-doctor.component';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent,
    
    children:[
      {path: 'cities', component: CityComponent},
      {path: 'specialists', component: SpecialityComponent},
      {path: 'SubSpecialists', component: SubSpecialityComponent}, 
      {path: 'area', component: AreaComponent},
      {path:'clinicServices' , component:ClinicServicesComponent },
      {path: 'doctorAttachments', component: DoctorAttachmentsComponent},
    
    ]
  },
  {path:'doctorDashboard',component:DoctorDashboardComponent,
    children:[
      {path:'attachment',component:DoctorDashboardAttachmentComponent}
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
