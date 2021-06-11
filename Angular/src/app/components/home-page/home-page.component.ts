import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ISpecialty } from 'src/app/_models/_interfaces/ISpecilaty';
import { SpecilatyService } from 'src/app/_services/specilaty.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  SpecailtyList:ISpecialty[];
  constructor(private _specilatyService:SpecilatyService,private _router:Router) { }

  ngOnInit(): void {
    this._specilatyService.getAllSpecialities().subscribe(data=>
      {
        this.SpecailtyList=data;
        console.log(data);
      })
  }

  ShowDoctors(specailtyId)
  {
    console.log(specailtyId);
    this._router.navigate(['showDoctors',specailtyId]);
  }

}
