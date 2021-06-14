import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Days, IdoctorDayWork, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { IMakeOfferWithDoctorInfo } from 'src/app/_models/_interfaces/IMakeOfferWithDoctorInfo';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { WorkingDaysService } from 'src/app/_services/working-days.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.scss'],
  providers: [DatePipe]
})
export class OfferDetailsComponent implements OnInit {

  constructor(private _makeOfferService:MakeOfferService,private _dataSharedService:DataSharedService
    ,private _workingDayServices:WorkingDaysService,private datePipe: DatePipe) 
  { 
    _dataSharedService.GoToOfferDetailsPage.subscribe(data=>{
      console.error(data);      
      if(data!=0){
        this.loadData(data)
      }
    })    
  }
  offerDetails:IMakeOfferWithDoctorInfo
  showReserveComponent=false
  url=environment.apiUrl
  imageSelected;
  workingDays


  ngOnInit(): void {
  }

  loadData(id){
    this._makeOfferService.GetById(id).subscribe(data=>{
      this.offerDetails=data;
      console.log(data);
      
      this.imageSelected=data.offerImages[0].image;
      this._workingDayServices.getWorkingDayWithDayShiftForSpecificDoctor(data.doctorId).subscribe(dat=>{
        this.workingDays = this.chunks(this.loadDays(dat),3);
        console.log(dat);

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


  

  reserve(shiftId,date){    
    date=new Date(date).toISOString();
    this._dataSharedService.GoToReserveOfferComponent.next({date:date,dayShiftId:shiftId})
    this.showReserveComponent=true
  }
  

}
