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
  insertClinicServiceInputValue:string=""
  state="insert";
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
  private getCurrentClinicServices(){
    this._clinicService.getClinicServicesForClinic().subscribe(data=>{
      if(data.length !=0){
        this.ClinicServicesSelected = data.filter(i=>i.byAdmin==true);
        this.NotAcceptByAdminClinicServicesSelected = data.filter(i=>i.byAdmin==false);
        this.state="update"
      }
    });
  }
  ngOnInit(): void {
    this.getCurrentClinicServices();
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
    this.ClinicServicesSelected.push.apply(this.ClinicServicesSelected,this.NotAcceptByAdminClinicServicesSelected);
          
    if(this.addNewClinicServices.length > 0){
      this._clinicServicesService.addListOfClinicServices(this.addNewClinicServices).subscribe(
        data=>{
          this.ClinicServicesSelected.push.apply(this.ClinicServicesSelected,data);
          this.addOrUpdateClinicServices();
        },error=>console.error(error)
      )
    }else{
      this.addOrUpdateClinicServices();
    }
  }
  addOrUpdateClinicServices(){
      this._clinicService.addClinicServicesToClinic(this.ClinicServicesSelected).subscribe(
      data=>{
        console.log("done")
      },
      erroe=>{
        console.log("error")
      }
    );
  }
  add(value){
    this.insertClinicServiceInputValue='';
    this.addNewClinicServices.push({id:0,name:value, byAdmin: false})
  }
  removefromNewSubSpecial(option){
    let clinicService = this.addNewClinicServices.find(i=>i.id==option);
    let elementIndex =this.addNewClinicServices.indexOf(clinicService);
    this.addNewClinicServices.splice(elementIndex,1)
  }
  
}
