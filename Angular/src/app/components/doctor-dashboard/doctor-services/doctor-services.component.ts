import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctor-services',
  templateUrl: './doctor-services.component.html',
  styleUrls: ['./doctor-services.component.scss']
})
export class DoctorServicesComponent implements OnInit {

  hasServices:boolean=false;
  SelectedServiceIdList:number[]=[];

  ServicesList=[{id:"1",service:"service1"},{id:"2",service:"service2"},{id:"3",service:"service3"},{id:"4",service:"service4"},{id:"5",service:"service5"},{id:"6",service:"service6"},{id:"7",service:"service7"},{id:"8",service:"service8"},{id:"9",service:"service9"},{id:"10",service:"service10"}];
  ServicesList2=[{id:"11",service:"service1"},{id:"12",service:"service2"},{id:"13",service:"service3"},{id:"14",service:"service4"},{id:"15",service:"service5"},{id:"16",service:"service6"},{id:"17",service:"service7"},{id:"8",service:"service8"}];

  constructor() { }

  ngOnInit(): void {
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
