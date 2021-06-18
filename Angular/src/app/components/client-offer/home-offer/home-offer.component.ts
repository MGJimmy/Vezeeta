import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home-offer',
  templateUrl: './home-offer.component.html',
  styleUrls: ['./home-offer.component.scss']
})
export class HomeOfferComponent implements OnInit {

  constructor() { }


  ngOnInit(): void {
    
  }

  


}
