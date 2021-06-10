import { Component, OnInit } from '@angular/core';
import { IClinicServices } from 'src/app/_models/_interfaces/IClinicService';
import { IRegisterDoctor } from 'src/app/_models/_interfaces/IRegisterDoctor';
import { ClincServicesService } from 'src/app/_services/clinc-services.service';
import { ClinicService } from 'src/app/_services/clinic.service';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-choose-clinic-service',
  templateUrl: './choose-clinic-service.component.html',
  styleUrls: ['./choose-clinic-service.component.scss']
})
export class ChooseClinicServiceComponent implements OnInit {

  allClinicServicesAccepted:IClinicServices[];
  ClinicServicesSelected:IClinicServices[] = [];
  NotAcceptByAdminClinicServicesSelected=new Array<IClinicServices>();
  addNewClinicServices=new Array<IClinicServices>();
  currentDoctor:IRegisterDoctor;
  insertSubSpecialInputValue:string=""
  constructor(
    private _clinicService:ClinicService,
    private _clinicServicesService:ClincServicesService,
    private _doctorService:DoctorService
  ) { }
  private getAcceptedClinicServices(){
    this._clinicServicesService.getAllAcceptedClinicServices().subscribe(
      data=>
      {
        this.allClinicServicesAccepted=data
      },
      error=>console.log(error));
  }
  // private getCurrentClinicServices(){
  //   this._clinicService.getClinicServicesForClinic().subscribe(data=>{
  //     if(data.length !=0){
  //       this.ClinicServicesSelected = data.filter(i=>i.byAdmin==true);
  //       this.NotAcceptByAdminSubSpecialtySelected=data.filter(i=>i.byAdmin==false);
  //       this.state="update"
  //     }
  // }
  ngOnInit(): void {
    this.load();
  }
  load(){
    this._doctorService.getCurrentDoctor().subscribe(
      data=>{
        this.currentDoctor=data;
        this.getAcceptedClinicServices();    
      },
    error=>console.error(error));
  }
  addServicesToClinic(){
    // this._clinicService.addClinicServicesToClinic(this.ClinicServicesSelected).subscribe(
    //   data=>{
    //     console.log("done")
    //   },
    //   erroe=>{
    //     console.log("error")
    //   }
    // );
    console.log(this.ClinicServicesSelected);
  }

}
