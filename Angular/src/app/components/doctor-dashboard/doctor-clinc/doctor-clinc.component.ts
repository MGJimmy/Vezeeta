import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IArea } from 'src/app/_models/_interfaces/IArea';
import { ICity } from 'src/app/_models/_interfaces/ICity';
import { IClinic } from 'src/app/_models/_interfaces/IClinic';
import { IClinicImage } from 'src/app/_models/_interfaces/IClinicImage';
import { AreaService } from 'src/app/_services/area.service';
import { CityService } from 'src/app/_services/city.service';
import { ClinicImagesService } from 'src/app/_services/clinic-images.service';
import { ClinicService } from 'src/app/_services/clinic.service';

@Component({
  selector: 'app-doctor-clinc',
  templateUrl: './doctor-clinc.component.html',
  styleUrls: ['./doctor-clinc.component.scss']
})
export class DoctorClincComponent implements OnInit {

  public response:any;
  ClinicForm : any;

  constructor(private _cityService:CityService,private _areaService:AreaService,
    private _formBuilder: FormBuilder,private _router:Router,
    private _clinicImagesService:ClinicImagesService,private _clinicService:ClinicService) { }

  public uploadClinicImages = (event) => {
    this.response = event;
    console.log(event);
    console.log(this.response[0]);
  }

  Cityies:ICity[];
  Areas:IArea[];
  ngOnInit(): void {
    this.ClinicForm = this._formBuilder.group({
      Street:['', Validators.required],
      Fees:['', Validators.required],
      ExaminationTime:['', Validators.required],
      WatingTime:['', Validators.required],
      CityId:['', Validators.required],
      AreaId:['', Validators.required]
     
    });
    this._cityService.getAllCities().subscribe(data=>
      {
        
        this.Cityies=data;
        console.log(this.Cityies);
      },
      err=>
      {
      console.log("err"+err);
      })
  }
  get formFields() { return this.ClinicForm.controls;}

  onOptionsSelected(e)
  {
    this._areaService.getAllAreaWhere(e.target['value']).subscribe(data=>
      {
        this.Areas=data;
      },
      err=>
      {
        console.log(err);
      })
    
    
  }
  
  clinic:IClinic;
  clinicImages:any=[];
  onSubmit()
  {
    this.clinic={
      "Street":this.formFields.Street.value,
      "Fees":this.formFields.Fees.value,
      "ExaminationTime":this.formFields.ExaminationTime.value,
      "WatingTime":this.formFields.WatingTime.value,
      "CityId":this.formFields.CityId.value,
      "AreaId":this.formFields.AreaId.value,
    } 
    for(var image in this.response)
    {
      let Image:IClinicImage=
      {
        Image:image
      }
      this.clinicImages.push(Image);
    }
    this._clinicService.AddClinic(this.clinic).subscribe(data=>
      {
        console.log(data);
        if(this.clinicImages.length>=0)
        {
          this._clinicImagesService.AddImages(this.clinicImages,data['DoctorId']).subscribe(data=>
            {
              console.log(data);
            },
            err=>
            {

            })
        }
      },
      err=>
      {

      })
    console.log(this.ClinicForm.controls);
  }

}
