import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CityComponent } from './components/dashboard/city/city.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DoctorAttachmentsComponent } from './components/dashboard/doctor-attachments/doctor-attachments.component';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent,
    
    children:[
      {path: 'cities', component: CityComponent},
      {path: 'specialists', component: SpecialityComponent},
      {path: 'SubSpecialists', component: SubSpecialityComponent}, 
      {path: 'area', component: AreaComponent},
      {path: 'doctorAttachments', component: DoctorAttachmentsComponent},
    
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
