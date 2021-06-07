import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IDoctorService } from 'src/app/_models/_interfaces/IDoctorService';
import { IDoctor_DoctorService } from 'src/app/_models/_interfaces/IDoctor_DoctorService';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DoctorServicesService } from 'src/app/_services/doctor-services.service';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-doctor-services',
  templateUrl: './doctor-services.component.html',
  styleUrls: ['./doctor-services.component.scss']
})
export class DoctorServicesComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  hasServices: boolean = false;
  SelectedServiceIdList=new Array<IDoctorService>();
  //  IDoctorService[]=[];
  

  ServicesList: IDoctorService[] = [];
  NotAcceptByAdminServiceSelected=new Array<IDoctorService>();
  insertedServiceList= new Array<IDoctorService>();
  

  constructor(private _authenticationService: AuthenticationService,
    private _doctorServices: DoctorServicesService,
    private _router: Router,
    private _DoctorService: DoctorService) {

  }

  
  
  ngOnInit(): void {
   


    this._doctorServices.getAllDoctorServices().subscribe(data => {
      this.ServicesList = data;
      console.log(this.ServicesList);

    })

    this._DoctorService.GetMyservices().subscribe(data=>
    {
      if(data.length !==0)
       {
        this.SelectedServiceIdList=data.filter(i=>i.byAdmin==true);;
        this.NotAcceptByAdminServiceSelected=data.filter(i=>i.byAdmin==false);
        this.hasServices=true;
        console.log(this.SelectedServiceIdList);
        console.log(this.NotAcceptByAdminServiceSelected);

       }
       else
       {
         this.hasServices=false;
       }
      
    })

  }

 

  Doctor_Services = new Array<IDoctor_DoctorService>();
  onAddSubmit() {
    this.AddServicesList();
    this.SelectedServiceIdList.forEach(service => {
      let doctorServce: IDoctor_DoctorService = {
        serviceID:service.id
      }
      this.Doctor_Services.push(doctorServce);
    });

    console.log(this.Doctor_Services);
    this._DoctorService.addservice(this.Doctor_Services).subscribe(data => {
        console.log("done");
        console.log(data);
    })

    
  }
  onEditSubmit() {
    this.AddServicesList();
    // console.log(this.insertedServiceList);
    // this.NotAcceptByAdminServiceSelected.push.apply(this.insertedServiceList);
    // console.log(this.NotAcceptByAdminServiceSelected);
    // this.SelectedServiceIdList.push.apply(this.NotAcceptByAdminServiceSelected);  
    console.log(this.SelectedServiceIdList);
    this.SelectedServiceIdList.forEach(service => {
      let doctorServce: IDoctor_DoctorService = {
        serviceID:service.id
      }
      this.Doctor_Services.push(doctorServce);
    });
    this._DoctorService.Updateservices( this.Doctor_Services).subscribe(data=>
      {
        console.log("Update done");
        console.log(data);

      },err=>
      {
        console.log("errrrrrr in Update");
      })

     
  }

  addNewServicesList=new Array<IDoctorService>();
  insertServiceValue:string=""
  addID=0;
  add(value){
    this.insertServiceValue='';
    this.addNewServicesList.push({id:this.addID++,name:value, byAdmin: false});
    console.log(this.addNewServicesList);
  }
  removefromNewServices(option){
    let service = this.addNewServicesList.find(i=>i.id==option);
    let elementIndex =this.addNewServicesList.indexOf(service);
    this.addNewServicesList.splice(elementIndex,1)
    console.log(this.addNewServicesList);
  }

  AddServicesList(){
    if(this.addNewServicesList.length > 0){
      this._doctorServices.addListOfDoctorService(this.addNewServicesList).subscribe(
        data=>{
          this.insertedServiceList=data;
          console.log(this.insertedServiceList);
          
        },error=>console.error(error)
      )
    }
  }
  

}
