import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { AreaService } from 'src/app/_services/area.service';
import { CityService } from 'src/app/_services/city.service';
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
  specilatyId: number;
  loadingSpecilaty = false;
  //CityService
  cites = [];
  citesBuffer = [];
  cityId: number;
  loadingCity = false;
  
  //areaService`
  areas = [];
  areasBuffer = [];
  areaId: number;
  loadingArea = false;

  doctorName : string = null;

  

  bufferSize = 50;
  numberOfItemsFromEndBeforeFetchingMore = 10;
  


  constructor(private specilatyService: SpecilatyService, private ciryService: CityService, private areaService: AreaService
    , private doctorService : DoctorService) { }

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

  search(){
    this.doctorService.search( 2 , 1 ,this.specilatyId , this.cityId , this.areaId , this.doctorName ).subscribe(
      (data) =>{
        
         console.log( data);

      }
    )
    
  }



}
