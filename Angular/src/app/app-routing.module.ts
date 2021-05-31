import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CityComponent } from './components/dashboard/city/city.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';
import { DoctorDashboardAttachmentComponent } from './components/doctor-dashboard/doctor-dashboard-attachment/doctor-dashboard-attachment.component';
import { DoctorDashboardComponent } from './components/doctor-dashboard/doctor-dashboard.component';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent,
  
    children:[
      {path: 'cities', component: CityComponent},
      {path: 'specialists', component: SpecialityComponent},
      {path: 'SubSpecialists', component: SubSpecialityComponent}, 
      {path: 'area', component: AreaComponent},
    
    ]
  },
  {path:'doctorDashboard',component:DoctorDashboardComponent,
    children:[
      {path:'attachment',component:DoctorDashboardAttachmentComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
