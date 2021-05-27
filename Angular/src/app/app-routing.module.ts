import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaComponent } from './components/dashboard/area/area.component';
import { CategoriesComponent } from './components/dashboard/categories/categories.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SpecialityComponent } from './components/dashboard/speciality/speciality.component';
import { SubSpecialityComponent } from './components/dashboard/sub-speciality/sub-speciality.component';

const routes: Routes = [
  {
    path:'dashboard',
    component:DashboardComponent,
  
    children:[
      {path: 'categories', component: CategoriesComponent},
      {path: 'specialists', component: SpecialityComponent},
      {path: 'SubSpecialists', component: SubSpecialityComponent},
      
      {path: 'area', component: AreaComponent},
    
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
