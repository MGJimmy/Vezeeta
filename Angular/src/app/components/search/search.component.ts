import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { _IdoctorFilter } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { AreaService } from 'src/app/_services/area.service';
import { CityService } from 'src/app/_services/city.service';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SpecilatyService } from 'src/app/_services/specilaty.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  
  
  //SpecilatyService
  specilaties = [];
  SpecilatyBuffer = []; 
  loadingSpecilaty = false;
  //CityService
  cites = [];
  citesBuffer = [];
  loadingCity = false;
  
  //areaService`
  areas = [];
  areasBuffer = [];
  loadingArea = false;

  //2 ways binding
  specilatyId: number;
  cityId: number;
  areaId: number;
  doctorName : string = null;

  doctorFilter:_IdoctorFilter={
    specailtyid:1,
    title: [],
    fee: [],
    subspecails:[],
    name:"",
    areaId:null,
    cityId:null
  };

  bufferSize = 50;
  numberOfItemsFromEndBeforeFetchingMore = 10;
  


  constructor(private specilatyService: SpecilatyService, private ciryService: CityService, private areaService: AreaService
    , private doctorService : DoctorService,private _dataSharedService:DataSharedService,private _router:Router) {

      _dataSharedService.sendDataToSearchComponent.subscribe(data=>{
        if(data.title.length!=0||
          data.fee.length!=0||data.subspecails.length!=0){
            this.doctorFilter.title=data.title;
            this.doctorFilter.subspecails=data.subspecails;
            this.doctorFilter.fee=data.fee;
            console.log(data);
            this.search();
        }
      })

      _dataSharedService.sendSpecialtyIdFromHomePageToSearchComponent.subscribe(data=>{
        if(data!=0){
          this.specilatyId=data;
          this.changeSpecialty(0)
          this.search();
        }
      })

    }

  ngOnInit(): void {
    

    //SpecilatyService 
    this.specilatyService.getAllSpecialities().subscribe(
      data => {
        this.specilaties = data;
        this.SpecilatyBuffer = this.specilaties.slice(0, this.bufferSize);
      
      
      }

    )

    //CityService
    this.ciryService.getAllCities().subscribe(
      data => {
        this.cites = data;
        this.citesBuffer = this.cites.slice(0, this.bufferSize);
      }
    )

    //areaService
    this.areaService.getAll().subscribe(
      data => {
        this.areas = data;
        this.areasBuffer = this.areas.slice(0, this.bufferSize);
      }
    )
  }


  
  search(){
    this.doctorFilter.cityId=this.cityId;
    this.doctorFilter.areaId=this.areaId;
    this.doctorFilter.specailtyid=this.specilatyId;
    this.doctorFilter.name=this.doctorName;

    console.error(this.doctorFilter)
    
    this.doctorService.ShowSpecailtyDoctorswithFilter(this.doctorFilter).subscribe(data=>{
      this._dataSharedService.sendAllDocterAfterFilterToShow.next(data);
      this._router.navigate(["showDoctors"])
      console.log(data)
    })

  }


  changeSpecialty(event){
    this._dataSharedService.sendSpecialtyIdToSideBarComponent.next(this.specilatyId);
    console.error(event)
  }



  

  onScrollToEndForSpecilaty() {
    this.fetchMoreForSpecilaty();
  }

  onScrollToEndForCity() {
    this.fetchMoreForCity();
  }

  onScrollToEndForArea() {
    this.fetchMoreForArea();
  }

  onScrollForSpecilaty({ end }) {
    if (this.loadingSpecilaty || this.specilaties.length <= this.SpecilatyBuffer.length) {
      return;
    }
    if (end + this.numberOfItemsFromEndBeforeFetchingMore >= this.SpecilatyBuffer.length) {
      this.fetchMoreForSpecilaty();
    }

  }

  onScrollForCity({ end }) {
    if (this.loadingCity || this.cites.length <= this.citesBuffer.length) {
      return;
    }
    if (end + this.numberOfItemsFromEndBeforeFetchingMore >= this.citesBuffer.length) {
      this.fetchMoreForCity();
    }
  }

  onScrollForArea({ end }) {

    if (this.loadingArea || this.areas.length <= this.areasBuffer.length) {
      return;
    }
    if (end + this.numberOfItemsFromEndBeforeFetchingMore >= this.areasBuffer.length) {
      this.fetchMoreForArea();
    }
  }

  private fetchMoreForSpecilaty() {
    const len = this.SpecilatyBuffer.length;
    const more = this.specilaties.slice(len, this.bufferSize + len);
    this.loadingSpecilaty = true;
    //  using timeout here to simulate backend API delay
    this.loadingSpecilaty = false;
    this.SpecilatyBuffer = this.SpecilatyBuffer.concat(more);
  }

  private fetchMoreForCity() {
    const len = this.citesBuffer.length;
    const more = this.cites.slice(len, this.bufferSize + len);
    this.loadingCity = true;
    //  using timeout here to simulate backend API delay
    this.loadingCity = false;
    this.citesBuffer = this.citesBuffer.concat(more);

  }

  private fetchMoreForArea() {
    const len = this.areasBuffer.length;
    const more = this.areas.slice(len, this.bufferSize + len);
    this.loadingArea = true;
    //  using timeout here to simulate backend API delay
    this.loadingArea = false;
    this.areasBuffer = this.areasBuffer.concat(more);

  }


}
