import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Days, IDoctor, IdoctorDayWork, _DayShiftsforDoctor, _WorkingDay } from 'src/app/_models/_interfaces/IDoctorPresentaion';
import { DataSharedService } from 'src/app/_services/data-shared.service';
import { DoctorService } from 'src/app/_services/doctor.service';
import { SubSpecialityService } from 'src/app/_services/sub-speciality.service';

@Component({
  selector: 'app-filter-doctor-data',
  templateUrl: './filter-doctor-data.component.html',
  styleUrls: ['./filter-doctor-data.component.scss']
})
export class FilterDoctorDataComponent implements OnInit {

  // SpecailtyId: any=1;
  DoctorsList: IDoctor[];
  specialtyName: string;
  NumberOfDoctors: number;

  DaysList: any[] = [];
  dbworking_days: _WorkingDay[] = [];
  
  dayShiftsforDoctor: _DayShiftsforDoctor[] = [];

  IsContainDoctor:boolean=false;
  
  constructor(private activeRoute: ActivatedRoute, private _router: Router,
    private _dataSharedService: DataSharedService,
    private _doctorService: DoctorService, private datePipe: DatePipe,
    private _subSpecialityService: SubSpecialityService) {
        _dataSharedService.sendAllDocterAfterFilterToShow.subscribe(data=>{
          if(data.length!=0){
            this.LoadData(data);
            this.IsContainDoctor=true;
          }
          else{
            this.DoctorsList=null;
            this.IsContainDoctor=false;
          }
        })
  }

  ngOnInit(): void {
  }

  LoadData(data){
        this.DoctorsList = data;
       // this.specialtyName = data[0].specialty.name;
        this.NumberOfDoctors = data.length;
        this.DoctorsList.forEach(element => {
          element.presentDaysWork = this.chunks(this.loadDays(element.workingDays), 3);
  
        });
  
  
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

    return workingDays;
  }

  public createImgPath = (serverPath: string) => {
    return `http://localhost:57320/${serverPath}`;

  }


  reserve(shiftId, doctorName, date) {
    date = new Date(date).toISOString();
    this._router.navigate(["/Reversation"])
      .then(() => {
        this._dataSharedService.GoToReservationPage.next({ dayShiftId: shiftId, doctorName: doctorName, date: date })
      })



    // date=date.

    // // console.log(date.toLocaleDateString('en-GB'))
    // console.log(date.toISOString())
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

}
