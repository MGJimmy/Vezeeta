import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IDoctorService } from 'src/app/_models/_interfaces/IDoctorService';
import { IDoctor_DoctorService } from 'src/app/_models/_interfaces/IDoctor_DoctorService';
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
  //SelectedServiceIdList=new Array<IDoctorService>();
  SelectedServiceIdList: IDoctorService[] = [];


  ServicesList: IDoctorService[] = [];
  // NotAcceptByAdminServiceSelected=new Array<IDoctorService>();
  // insertedServiceList= new Array<IDoctorService>();

  NotAcceptByAdminServiceSelected: IDoctorService[] = [];
  insertedServiceList: IDoctorService[] = [];

  constructor(
    private _doctorServices: DoctorServicesService,
    private _router: Router,
    private _DoctorService: DoctorService) {

  }



  ngOnInit(): void {



    this._doctorServices.getAllDoctorServices().subscribe(data => {
      this.ServicesList = data;
    })

    this._DoctorService.GetMyservices().subscribe(data => {
      if (data.length !== 0) {
        this.SelectedServiceIdList = data.filter(i => i.byAdmin == true);
        this.NotAcceptByAdminServiceSelected = data.filter(i => i.byAdmin == false);
        this.hasServices = true;
      }
      else {
        this.hasServices = false;
      }

    })

  }



 
  Doctor_Services: IDoctor_DoctorService[] = [];
  onAddSubmit() {
    this.SelectedServiceIdList.push.apply(this.SelectedServiceIdList, this.NotAcceptByAdminServiceSelected);
    this.AddServicesList("Add");




  }
  onEditSubmit() {
    
    this.SelectedServiceIdList.push.apply(this.SelectedServiceIdList, this.NotAcceptByAdminServiceSelected);
    this.AddServicesList("Update");

  }

  Update_Service_List() {
    this.SelectedServiceIdList.forEach(service => {
      let doctorServce: IDoctor_DoctorService = {
        serviceID: service.id
      }
      this.Doctor_Services.push(doctorServce);
    });
    this._DoctorService.Updateservices(this.Doctor_Services).subscribe(data => { 
      this._router.routeReuseStrategy.shouldReuseRoute = () => false;
        this._router.onSameUrlNavigation = 'reload';
        this._router.navigate([this._router.url]);
    })
  }

  Add_Service_List() {
    this.SelectedServiceIdList.forEach(service => {
      let doctorServce: IDoctor_DoctorService = {
        serviceID: service.id
      }
      this.Doctor_Services.push(doctorServce);
    });

    this._DoctorService.addservice(this.Doctor_Services).subscribe(data => {

      this._router.routeReuseStrategy.shouldReuseRoute = () => false;
        this._router.onSameUrlNavigation = 'reload';
        this._router.navigate([this._router.url]);
     })
  }
  addNewServicesList = new Array<IDoctorService>();
  insertServiceValue: string = ""

  add(value) {
    this.insertServiceValue = '';
    let s: IDoctorService =
    {
      name: value,
      byAdmin: false
    }
    this.addNewServicesList.push(s);
  }
  removefromNewServices(index) {
    this.addNewServicesList.splice(index, 1)
  }
  removefromNotAcceptedServices(index) {
    this.NotAcceptByAdminServiceSelected.splice(index, 1)
  }

  AddServicesList(btnclick) {
    if (this.addNewServicesList.length > 0) {

      this._doctorServices.addListOfDoctorService(this.addNewServicesList).subscribe(
        data => {
          this.SelectedServiceIdList.push.apply(this.SelectedServiceIdList, data);
          if (btnclick == "Update")
            this.Update_Service_List();
          else
            this.Add_Service_List();

        }, error => console.error(error)
      )
    }
    else {
      if (btnclick == "Update")
        this.Update_Service_List();
      else
        this.Add_Service_List();

    }
  }


}
