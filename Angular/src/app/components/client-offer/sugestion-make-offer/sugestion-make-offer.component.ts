import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sugestion-make-offer',
  templateUrl: './sugestion-make-offer.component.html',
  styleUrls: ['./sugestion-make-offer.component.scss']
})
export class SugestionMakeOfferComponent implements OnInit {

  SuggestDoctorOffers:IMakeOfferWithDoctorInfo[]=[];
  constructor(private _makeOfferService:MakeOfferService,private _router: Router
    ,private _dataSharedService:DataSharedService) { }
url=environment.apiUrl;
math=Math;
  ngOnInit(): void {

    this._makeOfferService.GetSuggestionTop12Rated().subscribe(data=>{
      this.SuggestDoctorOffers=this.chunks(data,4);
      console.log(this.SuggestDoctorOffers);
    })
  }

  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;

  }
  chunks(array, size) {
    let results = [];
    results = [];
    while (array.length) {
    results.push(array.splice(0, size));
    }
      return results;
  }

  ShowDetails(DoctorId) {
    this._router.navigate(['ShowDoctorDetails', DoctorId]);
  }

  Book(id){
    this._router.navigate(['/ClientOffer/OfferDetails']).then(()=>this._dataSharedService.GoToOfferDetailsPage.next(id))
  }

}
