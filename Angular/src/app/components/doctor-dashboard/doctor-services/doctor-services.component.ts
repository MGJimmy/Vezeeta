import { Component, OnInit } from '@angular/core';
import { IDoctorService } from 'src/app/_models/_interfaces/IDoctorService';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DoctorServicesService } from 'src/app/_services/doctor-services.service';

@Component({
  selector: 'app-doctor-services',
  templateUrl: './doctor-services.component.html',
  styleUrls: ['./doctor-services.component.scss']
})
export class DoctorServicesComponent implements OnInit {

  hasServices:boolean=false;
  SelectedServiceIdList:number[]=[];

  ServicesList:IDoctorService[];
  // =[{id:"1",Name:"service1",byadmin:"true"},{id:"2",Name:"service2",byadmin:"true"},{id:"3",Name:"service3",byadmin:"true"},{id:"4",Name:"service4",byadmin:"true"},{id:"5",Name:"service5",byadmin:"true"},{id:"6",Name:"service6",byadmin:"true"},{id:"7",Name:"service7",byadmin:"false"},{id:"8",Name:"service8",byadmin:"true"},{id:"9",Name:"service9",byadmin:"true"},{id:"10",Name:"service10",byadmin:"true"}];
  ServicesList2=[{id:"11",Name:"service1"},{id:"12",Name:"service2"},{id:"13",Name:"service3"},{id:"14",Name:"service4"},{id:"15",Name:"service5"},{id:"16",Name:"service6"},{id:"17",Name:"service7"},{id:"8",Name:"service8"}];

  constructor(private _authenticationService:AuthenticationService,
    private _doctorServices:DoctorServicesService) { }

  ngOnInit(): void {
    this._doctorServices.getAllDoctorServices().subscribe(data=>
      {
        this.ServicesList=data;
        console.log(this.ServicesList);
      })
  }

  changeStatus(a,e)
  {
    console.log(a);
    console.log(e);
    console.log(e.target.checked);
    if(e.target.checked==true)
    {
      this.SelectedServiceIdList.push(a);
    }
    else
    {
      this.RemoveElementFromArray(a);
    }
  }

  RemoveElementFromArray(element: number) {
    this.SelectedServiceIdList.forEach((value,index)=>{
        if(value==element) this.SelectedServiceIdList.splice(index,1);
    });
}
  onAddSubmit()
  {
   console.log("add");
   this._authenticationService.addservice(this.SelectedServiceIdList).subscribe(data=>
    {
      console.log("done");
      console.log(data);
    },err=>
    {
      console.log("errrrrrr");
    })
   console.log(this.SelectedServiceIdList);
  }
  OnEditSubmit()
  {
    console.log("Edit");
  }

  AddNewService()
  {
    console.log("add newdoctorService");
  }
}
