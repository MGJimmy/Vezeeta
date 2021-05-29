import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ICity } from 'src/app/_models/_interfaces/ICity';
import { IResponse } from 'src/app/_models/_interfaces/IResponse';
import { CityService } from 'src/app/_services/city.service';
import { environment } from 'src/environments/environment';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnInit {
  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasCities:boolean = false;
  private _cityToUpdate:ICity;
  allCities:ICity[]; 
  errorMsg:string;
  successMsg:string;
  cityForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  citiesCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; // citiesCount / pageSize

  // convenience getter for easy access to form fields
  get formFields() { return this.cityForm.controls; }
  constructor(private _cityService:CityService,
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  ngOnInit(): void {
    this.getCitiesCount();
    this.cityForm = this._formBuilder.group({
      name:['', Validators.required]
    });
    this.getSelectedPage(1);
  }

  private getCitiesCount(){
    this._cityService.getCitiesCount().subscribe(
      data => {
        this.citiesCount = data
        this.numberOfPages = Math.ceil(this.citiesCount / this.pageSize)
        
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  private onAddCitySubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.cityForm.invalid) {
        return;
      }

    this.loading = true;
    let newCity:ICity = 
    {
      id:0 ,
      name : this.formFields.name.value
    };
    this._cityService.addNewCity(newCity)
        .pipe(first())
        .subscribe(
            data => {
              console.log("ok")
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
              //let response:IResponse = error;
              console.log(error);
              this.errorMsg = "there's an error but we can't know it becuase error is empty";
                this.loading = false;
            });
  }

  private onUpdateCitySubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.cityForm.invalid) {
        return;
      }

    this.loading = true;
    this._cityToUpdate.name = this.formFields.name.value;
    console.log(this._cityToUpdate);
    this._cityService.updateCity(this._cityToUpdate.id, this._cityToUpdate)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
                this.errorMsg = error;

                this.loading = false;
            });
  }

  onAddOrUpdateSubmit(){
    if(this.actionName == "Add"){
      this.onAddCitySubmit();
    }else{
      this.onUpdateCitySubmit()
    }
  }
 
  openAddCityModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateCityModal(cityId){
    this.actionName = "Update";
    this._cityService.getCityById(cityId)
        .pipe(first())
        .subscribe(
            data => {
                this.cityForm.setValue({
                  name: data.name
                }); 
                this._cityToUpdate = data;
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteCityModal(cityId){
    //this._cityToDeleteId = cityId;
    this.confirmModal.pointerToFunction = this._cityService.deleteCity
    this.confirmModal.title = "Delete City";
    this.confirmModal.itemId = cityId;
    this.confirmModal.message = "Are you sure to delete this city";
    this.confirmModal.pageUrl = this._router.url;
    //this.confirmModal.entityName ="city";
  }

// pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._cityService.getCitiesByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allCities = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasCities = true;
        else
          this.hasCities = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }

}
