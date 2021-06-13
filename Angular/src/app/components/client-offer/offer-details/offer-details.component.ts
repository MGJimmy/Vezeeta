import { Component, OnInit } from '@angular/core';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.scss']
})
export class OfferDetailsComponent implements OnInit {

  constructor(private _makeOfferService:MakeOfferService,private _dataSharedService:DataSharedService) { 
    _dataSharedService.GoToOfferDetailsPage.subscribe(data=>{
      console.error(data);
      
      if(data!=0){
        this.loadData(data)
      }
    })    
  }
  offerDetails:IMakeOfferWithDoctorInfo
  url=environment.apiUrl
  imageSelected;


  ngOnInit(): void {
  }

  loadData(id){
    this._makeOfferService.GetById(id).subscribe(data=>{
      this.offerDetails=data;
      this.imageSelected=data.offerImages[0].image
      
    })
  }
  changeImage(image){
    this.imageSelected=image
  }

}
