import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Days, IDoctor, IdoctorDayWork, _DayShiftsforDoctor, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { DoctorService } from 'src/app/_services/doctor.service';

@Component({
  selector: 'app-show-doctors-page',
  templateUrl: './show-doctors-page.component.html',
  styleUrls: ['./show-doctors-page.component.scss'],
  providers: [DatePipe]
})
export class ShowDoctorsPageComponent implements OnInit {
  SpecailtyId: any;
  DoctorsList: IDoctor[];

  DaysList: any[] = [];
  dbworking_days: _WorkingDay[] = [];



  dayShiftsforDoctor: _DayShiftsforDoctor[] = [];

  constructor(private activeRoute: ActivatedRoute,
    private _doctorService: DoctorService, private datePipe: DatePipe) {
    this.activeRoute.params.subscribe(params =>
      this.SpecailtyId = params['id']
    );

  }

  ngOnInit(): void {
    this._doctorService.ShowSpecailtyDoctors(this.SpecailtyId).subscribe(data => {
      console.log(data);
      this.DoctorsList = data;
      this.DoctorsList.forEach(element => {
        element.presentDaysWork = this.loadDays(element.workingDays);

      });
      console.error(this.DoctorsList);


    }, err => {
      console.log("Error");
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

}
