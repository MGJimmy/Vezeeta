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

  public response: any;


  constructor(private _cityService: CityService, private _areaService: AreaService,
    private _formBuilder: FormBuilder, private _router: Router,
    private _clinicImagesService: ClinicImagesService, private _clinicService: ClinicService) { }

  public uploadClinicImages = (event) => {
    this.response = event;
    console.log(event);
    console.log(this.response[0]);
  }

  hasClinic: boolean = false;
  hasImage: boolean = false;
  MyClinic: IClinic;
  MyClinicImages: IClinicImage[];
  Cityies: ICity[];
  Areas: IArea[];

  

  ClinicForm = this._formBuilder.group({
    Street: ['', Validators.required],
    Fees: ['', Validators.required],
    ExaminationTime: ['', Validators.required],
    WatingTime: ['', Validators.required],
    CityId: ['', Validators.required],
    AreaId: ['', Validators.required]

  });

  areaForm=this._formBuilder.group({
    id:[""],
    name:["",[Validators.required,Validators.minLength(3)]],
    cityID:["",[Validators.required]]
  })

  ngOnInit(): void {

    this._cityService.getAllCities().subscribe(data => {

      this.Cityies = data;
    },
      err => {
        console.log("err" + err);
      })

    //condition for auth


    this._clinicService.GetMyClinic().subscribe(data => {
      if (data != null) {
        this.MyClinic = data;
        this.onOptionsSelected(data['cityId']);
        this.ClinicForm.setValue({
          Street: data['street'],
          Fees: data['fees'],
          ExaminationTime: data['examinationTime'],
          WatingTime: data['watingTime'],
          CityId: data['cityId'],
          AreaId: data['areaId']
        });
        this.hasClinic = true;

        // if there clinic check for images
        this._clinicImagesService.GetClinicImages().subscribe(data => {
          if (data.length !== 0) {
            this.MyClinicImages = data;
            this.hasImage = true;
            console.log(this.MyClinicImages);
          }
          else {
            console.log("No Image");
          }

        },
          err => {
            console.log("err" + err);
          })
      }


    },
      err => {
        console.log("err" + err);
        this.hasClinic = false;
      })



  }
  get formFields() { return this.ClinicForm.controls; }

  onOptionsSelected(CityID) {
    this._areaService.getAllAreaWhere(CityID).subscribe(data => {
      this.Areas = data;
    },
      err => {
        console.log(err);
      })


  }

  clinic: IClinic;
  DoctorID: any;
  clinicImages: IClinicImage[] = [];
  onAddSubmit() {
    this.clinic = {
      "Street": this.formFields.Street.value,
      "Fees": this.formFields.Fees.value,
      "ExaminationTime": this.formFields.ExaminationTime.value,
      "WatingTime": this.formFields.WatingTime.value,
      "CityId": this.formFields.CityId.value,
      "AreaId": this.formFields.AreaId.value,
    }
    if (this.response != null) {
      for (var image of this.response) {
        console.log(image);
        let Image: IClinicImage =
        {
          Image: image
        }
        this.clinicImages.push(Image);
      }
    }

    console.log(this.clinicImages);

    this._clinicService.AddClinic(this.clinic).subscribe(data => {
      console.log(data);
      this.DoctorID = data['doctorId'];
      if (this.clinicImages.length !== 0) {
        console.log("entered length");
        this._clinicImagesService.AddImages(this.clinicImages).subscribe(data => {
          console.log("entered Images");
          console.log(data);
        },
          err => {
            console.log("error for enter Images" + err);
          })
      }
      this._router.routeReuseStrategy.shouldReuseRoute = () => false;
      this._router.onSameUrlNavigation = 'reload';
      this._router.navigate([this._router.url]);
    },
      err => {
       
      })

  }
  OnEditSubmit() {
    this.clinic = {
      "DoctorId": this.MyClinic['doctorId'],
      "Street": this.formFields.Street.value,
      "Fees": this.formFields.Fees.value,
      "ExaminationTime": this.formFields.ExaminationTime.value,
      "WatingTime": this.formFields.WatingTime.value,
      "CityId": this.formFields.CityId.value,
      "AreaId": this.formFields.AreaId.value,
    }
    if (this.response != null) {
      for (var image of this.response) {
        console.log(image);
        let Image: IClinicImage =
        {
          Image: image
        }
        this.clinicImages.push(Image);
      }
    }
    this._clinicService.UpdateClinic(this.clinic).subscribe(data => {
      console.log(data);
      if (this.clinicImages.length !== 0) {
        console.log("entered length");
        this._clinicImagesService.AddImages(this.clinicImages).subscribe(data => {
          console.log("entered Images");
          console.log(data);
        },
          err => {
            console.log("error for enter Images" + err);
          })
      }

      this._router.routeReuseStrategy.shouldReuseRoute = () => false;
      this._router.onSameUrlNavigation = 'reload';
      this._router.navigate([this._router.url]);
      
    },
      err => {
       console.log(err);
      })

    console.log("Edit Submit");
  }

  DeleteImage(imgId)
  {
    this._clinicImagesService.DeleteImage(imgId).subscribe(data=>
      {
        this._router.routeReuseStrategy.shouldReuseRoute = () => false;
        this._router.onSameUrlNavigation = 'reload';
        this._router.navigate([this._router.url]);
      })
  }


  public createImgPath = (serverPath: string) => {
    return `http://localhost:57320/${serverPath}`;
  }

  get idArea(){ return this.areaForm.get("id")  }
  get name(){  return this.areaForm.get("name")   }  
  get cityID(){ return this.areaForm.get("cityID")  }

  openAddAreaModal()
  {

  }
  addNewArea()
  {

  }
}
