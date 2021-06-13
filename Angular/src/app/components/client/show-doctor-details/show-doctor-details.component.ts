import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Days, IDoctor, IdoctorDayWork, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-show-doctor-details',
  templateUrl: './show-doctor-details.component.html',
  styleUrls: ['./show-doctor-details.component.scss'],
  providers: [DatePipe]
})
export class ShowDoctorDetailsComponent implements OnInit {

  doctor: IDoctor;
  DoctorID: string;
  hasImage:boolean=false;
  hasdoctorServices:boolean=false;
  constructor(private _doctorService:DoctorService,private datePipe: DatePipe,private _router:Router,
    private _dataSharedService:DataSharedService,
    private activeRoute: ActivatedRoute) { 
      this.activeRoute.params.subscribe(params =>
        this.DoctorID = params['id']
      );
    }

  ngOnInit(): void {
    this._doctorService.ShowDoctorDetails(this.DoctorID).subscribe(data=>
      {
        this.doctor=data;
        console.log(data);
        console.log(this.doctor);
        console.log(data['clinic_Images']);
        console.log(this.doctor.services.length)
        if(this.doctor.services.length>0){
          this.hasdoctorServices=true;
        }
        if(this.doctor['clinic_Images'].length>0)
        {
          this.hasImage=true;
        }
        this.doctor.presentDaysWork = this.chunks(this.loadDays(this.doctor.workingDays),3);

      })
  }

  loadDays(dayForWorkinMonth: _WorkingDay[]) {
    let workingDays: IdoctorDayWork[] = [];
    for (var i = 0; i < 31; i++) {
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



    console.log(workingDays);

    return workingDays;
  }
  public createImgPath = (serverPath: string) => {
    return `http://localhost:57320/${serverPath}`;

  }
  chunks(array, size) {
    let results = [];
    results = [];
    while (array.length) {
    results.push(array.splice(0, size));
    }
      return results;
    }
    reserve(shiftId,doctorName,date){
      date=new Date(date).toISOString();
      this._router.navigate(["/Reversation"])
      .then(()=>{
        this._dataSharedService.GoToReservationPage.next({dayShiftId:shiftId,doctorName:doctorName,date:date })
      })
    }
    
}
