import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Day } from 'src/app/_models/_enums/Day';
import { IDayShift } from 'src/app/_models/_interfaces/IDayShift';
import { IWorkingDay } from 'src/app/_models/_interfaces/IWorkingDay';
import { ClinicService } from 'src/app/_services/clinic.service';
import { WorkingDaysService } from 'src/app/_services/working-days.service';

@Component({
  selector: 'app-clinic-working-days',
  templateUrl: './clinic-working-days.component.html',
  styleUrls: ['./clinic-working-days.component.scss']
})
export class ClinicWorkingDaysComponent implements OnInit {
  Saturday = Day.Saturday
  Sunday = Day.Sunday
  Monday = Day.Monday
  Tuesday = Day.Tuesday
  Wednesday = Day.Wednesday
  Thursday = Day.Thursday
  Friday = Day.Friday
  workingDaysForm:FormGroup;
  workingDays:IWorkingDay[];
  errorMsg:string;
  successMsg:string;
  loading:boolean = false;
  isAnyDayChoosed:boolean = false;
  isSaturdayChecked:boolean = false;
  isSundayChecked:boolean = false;
  isMondayChecked:boolean = false;
  isTuesdayChecked:boolean = false;
  isWednesdayChecked:boolean = false;
  isThursdayChecked:boolean = false;
  isFridayChecked:boolean = false;
  isHasClinic:boolean = false;

  get formFields() {return this.workingDaysForm.controls}
  get saturdayShiftsArr(){return this.workingDaysForm.get('saturdayShifts') as FormArray}
  get sundayShiftsArr(){return this.workingDaysForm.get('sundayShifts') as FormArray}
  get mondayShiftsArr(){return this.workingDaysForm.get('mondayShifts') as FormArray}
  get tuesdayShiftsArr(){return this.workingDaysForm.get('tuesdayShifts') as FormArray}
  get wednesdayShiftsArr(){return this.workingDaysForm.get('wednesdayShifts') as FormArray}
  get thursdayShiftsArr(){return this.workingDaysForm.get('thursdayShifts') as FormArray}
  get fridayShiftsArr(){return this.workingDaysForm.get('fridayShifts') as FormArray}
  constructor(
    private _formBuilder:FormBuilder,
    private _workingDaysService:WorkingDaysService,
    private _clinicService:ClinicService
    ) { }
  
  private createFormShift(from,to,numOfReservation){
    return this._formBuilder.group({
      from:[from],
      to:[to],
      maxNumOfReservation:[numOfReservation]
    });
  }
  private initWorkingDaysForm(){
    this.workingDaysForm = this._formBuilder.group(
      {
        saturday:[''],
        saturdayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        sunday:[''],
        sundayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        monday:[''],
        mondayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        tuesday:[''],
        tuesdayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        wednesday:[''],
        wednesdayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        thursday:[''],
        thursdayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
        friday:[''],
        fridayShifts:this._formBuilder.array([this.createFormShift("13:00","15:00",5)]),
      });
  
  }
  ngOnInit(): void {
    this.checkClinicExist();
    this.initWorkingDaysForm();
    this.getWorkingDaysForCurrentDoctor();
   }
  onSubmit(){
    this.loading = true;
    this.successMsg = null;
    this.errorMsg = null;
    let saturday:IWorkingDay = null;
    let sunday:IWorkingDay = null;
    let monday:IWorkingDay = null;
    let tuesday:IWorkingDay = null;
    let wednesday:IWorkingDay = null;
    let thursday:IWorkingDay = null;
    let friday:IWorkingDay = null;
    let saturdayShifts:IDayShift[] = [];
    let sundayShifts:IDayShift[] = [];
    let mondayShifts:IDayShift[] = [];
    let tuesdayShifts:IDayShift[] = [];
    let wednesdayShifts:IDayShift[] = [];
    let thursdayShifts:IDayShift[] = [];
    let fridayShifts:IDayShift[] = [];
    if(this.isSaturdayChecked){ 
      this.saturdayShiftsArr.controls.forEach(shiftGroup => {
        saturdayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      saturday = {
        day:this.Saturday,
        dayShifts:saturdayShifts
      }
    }
    if(this.isSundayChecked){
      this.sundayShiftsArr.controls.forEach(shiftGroup => {
        sundayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      sunday = {
        day:"Sunday",
        dayShifts: sundayShifts
      }
    }
    if(this.isMondayChecked){
      this.mondayShiftsArr.controls.forEach(shiftGroup => {
        mondayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      monday = {
        day:"monday",
        dayShifts: mondayShifts
      }
    }
    if(this.isTuesdayChecked){
      this.tuesdayShiftsArr.controls.forEach(shiftGroup => {
        tuesdayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      tuesday = {
        day:"tuesday",
        dayShifts: tuesdayShifts
      }
    }
    if(this.isWednesdayChecked){
      this.wednesdayShiftsArr.controls.forEach(shiftGroup => {
        wednesdayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      wednesday = {
        day:"wednesday",
        dayShifts: wednesdayShifts
      }
    }
    if(this.isThursdayChecked){
      this.thursdayShiftsArr.controls.forEach(shiftGroup => {
        thursdayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      thursday = {
        day:"thursday",
        dayShifts: thursdayShifts
      }
    }
    if(this.isFridayChecked){
      this.fridayShiftsArr.controls.forEach(shiftGroup => {
        fridayShifts.push({
          from: shiftGroup.get('from').value,
          to: shiftGroup.get('to').value,
          maxNumOfReservation:shiftGroup.get('maxNumOfReservation').value
        })
      });
      friday = {
        day:"friday",
        dayShifts: fridayShifts
      }
    }
    
    this.workingDays = [
      saturday,
      sunday,
      monday,
      tuesday,
      wednesday,
      thursday,
      friday
    ]
    console.log(this.workingDays)
    this._workingDaysService.addWorkingDays(this.workingDays).subscribe(
      data=>{
        this.successMsg = "successfully"
        this.loading =false;
      },
      error=>{
        this.errorMsg = "Error"
        this.loading = false;
      }
    );
  }
  saturdayTouched(event){
    this.isSaturdayChecked = false;
    if(event.target.checked){
      this.isSaturdayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  sundayTouched(event){
    this.isSundayChecked = false;
    if(event.target.checked){
      this.isSundayChecked = true;
    }
    this.checkAnyDayChoosed()

  }
  mondayTouched(event){
    this.isMondayChecked = false;
    if(event.target.checked){
      this.isMondayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  tuesdayTouched(event){
    this.isTuesdayChecked = false;
    if(event.target.checked){
      this.isTuesdayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  wednesdayTouched(event){
    this.isWednesdayChecked = false;
    if(event.target.checked){
      this.isWednesdayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  thursdayTouched(event){
    this.isThursdayChecked = false;
    if(event.target.checked){
      this.isThursdayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  fridayTouched(event){
    this.isFridayChecked = false;
    if(event.target.checked){
      this.isFridayChecked = true;
    }
    this.checkAnyDayChoosed()
  }
  checkAnyDayChoosed(){
    if(this.isSaturdayChecked || this.isSundayChecked || this.isMondayChecked
      || this.isTuesdayChecked || this.isWednesdayChecked || this.isThursdayChecked || this.isFridayChecked){
        this.isAnyDayChoosed = true;
      }
      else{
        this.isAnyDayChoosed = false;
      }
  }
  addDayShift(day:Day){
    if(day == Day.Saturday && this.saturdayShiftsArr.length < 3)
      this.saturdayShiftsArr.push(this.createFormShift("13:00","15:00",5));

    else if(day == Day.Sunday && this.sundayShiftsArr.length < 3)
      this.sundayShiftsArr.push(this.createFormShift("13:00","15:00",5));

    else if(day == Day.Monday && this.mondayShiftsArr.length < 3)
      this.mondayShiftsArr.push(this.createFormShift("13:00","15:00",5));

    else if(day == Day.Tuesday && this.tuesdayShiftsArr.length < 3)
      this.tuesdayShiftsArr.push(this.createFormShift("13:00","15:00",5));

    else if(day == Day.Wednesday && this.wednesdayShiftsArr.length < 3)
      this.wednesdayShiftsArr.push(this.createFormShift("13:00","15:00",5));

    else if(day == Day.Thursday && this.thursdayShiftsArr.length < 3)
      this.thursdayShiftsArr.push(this.createFormShift("13:00","15:00",5));
      
    else if(day == Day.Friday && this.fridayShiftsArr.length < 3)
      this.fridayShiftsArr.push(this.createFormShift("13:00","15:00",5));
      
  }
  deleteDayShift(day:Day){
    if(day == Day.Saturday && this.saturdayShiftsArr.length > 1)
      this.saturdayShiftsArr.removeAt(this.saturdayShiftsArr.length - 1);

    else if(day == Day.Sunday && this.sundayShiftsArr.length > 1)
      this.sundayShiftsArr.removeAt(this.sundayShiftsArr.length - 1);

    else if(day == Day.Monday && this.mondayShiftsArr.length > 1)
      this.mondayShiftsArr.removeAt(this.mondayShiftsArr.length - 1);

    else if(day == Day.Tuesday && this.tuesdayShiftsArr.length > 1)
      this.tuesdayShiftsArr.removeAt(this.tuesdayShiftsArr.length - 1);

    else if(day == Day.Wednesday && this.wednesdayShiftsArr.length > 1)
      this.wednesdayShiftsArr.removeAt(this.wednesdayShiftsArr.length - 1);

    else if(day == Day.Thursday && this.thursdayShiftsArr.length > 1)
      this.thursdayShiftsArr.removeAt(this.thursdayShiftsArr.length - 1);

    else if(day == Day.Friday && this.fridayShiftsArr.length > 1)
      this.fridayShiftsArr.removeAt(this.fridayShiftsArr.length - 1);
  }
  getWorkingDaysForCurrentDoctor(){
    this._workingDaysService.getWorkingDays().subscribe(
      data=>{
        this.workingDays = data;
        this.showWorkingDays();
      },
      error=>{
        this.errorMsg = "Error"
      }
    );
  }
  showWorkingDays(){
    this.workingDays.forEach(workingDay => {
      if(workingDay.day == this.Saturday){
        this.isSaturdayChecked = true;
        this.saturdayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.saturdayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
      if(workingDay.day == this.Sunday){
        console.log("sasa")
        this.isSundayChecked = true;
        this.sundayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.sundayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
      if(workingDay.day == this.Monday){
        this.isMondayChecked = true;
        this.mondayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.mondayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }

      if(workingDay.day == this.Tuesday){
        this.isTuesdayChecked = true;
        this.tuesdayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.tuesdayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
      if(workingDay.day == this.Wednesday){
        this.isWednesdayChecked = true;
        this.wednesdayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.wednesdayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
      if(workingDay.day == this.Thursday){
        this.isThursdayChecked = true;
        this.thursdayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.thursdayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
      if(workingDay.day == this.Friday){
        this.isFridayChecked = true;
        this.fridayShiftsArr.removeAt(0);
        workingDay.dayShifts.forEach(dayShift=>{
          this.fridayShiftsArr.push(this.createFormShift(dayShift.from,dayShift.to,dayShift.maxNumOfReservation))
        });
      }
    });
    this.checkAnyDayChoosed()
  }
  checkClinicExist(){
    this._clinicService.GetMyClinic().subscribe(
      data=>{
        console.log(data)
        if(data != null)
          this.isHasClinic = true
      });
  }
}

