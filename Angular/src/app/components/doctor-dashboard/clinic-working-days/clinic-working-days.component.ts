import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { IWorkingDay } from 'src/app/_models/_interfaces/IWorkingDay';
import { WorkingDaysService } from 'src/app/_services/working-days.service';

@Component({
  selector: 'app-clinic-working-days',
  templateUrl: './clinic-working-days.component.html',
  styleUrls: ['./clinic-working-days.component.scss']
})
export class ClinicWorkingDaysComponent implements OnInit {
  testClinictId = "05663647-f96a-4039-bd3f-3613e9b096d2";
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

  get formFields() {return this.workingDaysForm.controls}
  get saturdayShifts(){return this.workingDaysForm.get('saturdayShifts') as FormArray}
  constructor(
    private _formBuilder:FormBuilder,
    private _workingDaysService:WorkingDaysService) { }
  
  // private createFormShift(){
  //   return this._formBuilder.group({
  //     from:[''],
  //     to:['']
  //   });
  // }
  private initWorkingDaysForm(){
    this.workingDaysForm = this._formBuilder.group(
      {
        saturday:[''],
        saturdayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        sunday:[''],
        sundayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        monday:[''],
        mondayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        tuesday:[''],
        tuesdayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        wednesday:[''],
        wednesdayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        thursday:[''],
        thursdayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
        friday:[''],
        fridayShifts:this._formBuilder.group({
          shiftOne:this._formBuilder.group({
            from:[''],
            to:['']
          })
        }),
      });
  
  }
  ngOnInit(): void {
    this.initWorkingDaysForm();
   }
  onSubmit(){
    this.loading = true;
    this.successMsg = null;
    this.errorMsg = null;
    let saturday:IWorkingDay = null
    let sunday:IWorkingDay = null
    let monday:IWorkingDay = null
    let tuesday:IWorkingDay = null
    let wednesday:IWorkingDay = null
    let thursday:IWorkingDay = null
    let friday:IWorkingDay = null
    if(this.isSaturdayChecked){
      saturday = {
        clinicId:this.testClinictId,
        day:"saturday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('saturdayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('saturdayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isSundayChecked){
      sunday = {
        clinicId:this.testClinictId,
        day:"sunday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('sundayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('sundayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isMondayChecked){
      monday = {
        clinicId:this.testClinictId,
        day:"monday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('mondayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('mondayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isTuesdayChecked){
      tuesday = {
        clinicId:this.testClinictId,
        day:"tuesday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('tuesdayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('tuesdayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isWednesdayChecked){
      wednesday = {
        clinicId:this.testClinictId,
        day:"wednesday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('wednesdayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('wednesdayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isThursdayChecked){
      thursday = {
        clinicId:this.testClinictId,
        day:"thursday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('thursdayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('thursdayShifts.shiftOne.to').value,
          }
        ]
      }
    }
    if(this.isFridayChecked){
      friday = {
        clinicId:this.testClinictId,
        day:"friday",
        dayShifts:[
          {
            from:this.workingDaysForm.get('fridayShifts.shiftOne.from').value,
            to:this.workingDaysForm.get('fridayShifts.shiftOne.to').value,
          }
        ]
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
  // addSaturdayShift(){
  //   this.saturdayShifts.push(this.createFormShift());
  // }
}
