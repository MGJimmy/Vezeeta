import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Days, IdoctorDayWork, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { OfferRatingService } from 'src/app/_services/offer-rating.service';
import { WorkingDaysService } from 'src/app/_services/working-days.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.scss'],
  providers: [DatePipe]
})
export class OfferDetailsComponent implements OnInit {

  doctorOfferId:number;
  commentNumber:number=5;

  constructor(private _makeOfferService:MakeOfferService,private _dataSharedService:DataSharedService
    ,private _workingDayServices:WorkingDaysService,private datePipe: DatePipe,
    private _rateOfferService:OfferRatingService) 
  { 
    _dataSharedService.GoToOfferDetailsPage.subscribe(data=>{   
      if(data!=0){
        this.doctorOfferId=data;
      }
    })    
  }
  offerDetails:IMakeOfferWithDoctorInfo
  showReserveComponent=false
  url=environment.apiUrl
  imageSelected;
  workingDays


  ngOnInit(): void {
    this.loadData();
    this.loadComment();
  }

  loadData(){
    console.log(this.doctorOfferId);
    
    this._makeOfferService.GetById(this.doctorOfferId).subscribe(data=>{
      this.offerDetails=data;
      console.log(data);
      
      this.imageSelected=data.offerImages[0].image;
      this._workingDayServices.getWorkingDayWithDayShiftForSpecificDoctor(data.doctorId).subscribe(dat=>{
        console.log(dat);
        this.workingDays = this.chunks(this.loadDays(dat),3);
        

      })      
    })
  }

  loadDays(dayForWorkinMonth: _WorkingDay[]) {
    let workingDays: IdoctorDayWork[] = [];
    for (var i = 0; i < 21; i++) {
      let myDate = new Date();
      let s = myDate.setDate(myDate.getDate() + i);
      let f = this.datePipe.transform(s, 'EEEE, MMMM d, y, h:mm:ss a zzzz');
      let newDate = new Date(f);

      var newDayWork: IdoctorDayWork = {
        datee: f,
        IsWork: false
      }
      dayForWorkinMonth.forEach(element => {
        if (element.day == Days[newDate.getDay()]) {
          newDayWork.IsWork = true;
          newDayWork._dayShift = element.dayShifts;
        }
      });
      workingDays.push(newDayWork);
    }
    return workingDays;
  }


  chunks(array, size) {
    let results = [];
    results = [];
    while (array.length) {
      results.push(array.splice(0, size));
    }
    return results;
  }

  changeImage(image){
    this.imageSelected=image
  }

  OfferRates:any;
  hasRate:boolean=false;
  loadComment()
  {
    this._rateOfferService.GetRateing(this.doctorOfferId,this.commentNumber).subscribe(data=>
    {     
      this.OfferRates=data;
      console.log(this.OfferRates)
      if(data['getOfferRatingDtos'].length>0){
        this.hasRate=true;
      }   
      console.log(this.hasRate)
    });
  }
  
  ShowMoreComment()
  {
    this.commentNumber+=5;
    this.loadComment();
  }

  

  reserve(shiftId,date){    
    date=new Date(date).toISOString();
    this._dataSharedService.GoToReserveOfferComponent.next({date:date,dayShiftId:shiftId})
    this.showReserveComponent=true
  }
  

}
