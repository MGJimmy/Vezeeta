import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-newest-offer',
  templateUrl: './newest-offer.component.html',
  styleUrls: ['./newest-offer.component.scss']
})
export class NewestOfferComponent implements OnInit {

  constructor(private _makeOfferService:MakeOfferService,private _router:Router,
    private _dataSharedService:DataSharedService) { }

  allOffer:IMakeOfferWithDoctorInfo[];
  url=environment.apiUrl;
  math=Math;

  ngOnInit(): void {
    this._makeOfferService.GetAll().subscribe(data=>{
      this.allOffer=data.slice(0,36);      
    })
  }
  Book(id){
    this._router.navigate(['ClientOffer/OfferDetails']).then(()=>this._dataSharedService.GoToOfferDetailsPage.next(id))
  }
}
