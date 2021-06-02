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
      {path:'attachment',component:DoctorDashboardAttachmentComponent},
      {path:'clinc',component:DoctorClincComponent},
      {path:'attachment',component:DoctorDashboardAttachmentComponent},
      {path:'workingDays',component:ClinicWorkingDaysComponent},
    ]
  },
  {path:"registerDoctor", component:RegisterDoctorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
