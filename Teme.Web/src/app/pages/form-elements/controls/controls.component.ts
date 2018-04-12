import { Component, ViewEncapsulation, ViewChild } from '@angular/core';
import { IMultiSelectOption, IMultiSelectSettings, IMultiSelectTexts } from 'angular-2-dropdown-multiselect';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-controls',
  templateUrl: './controls.component.html',
  styleUrls: ['./controls.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ControlsComponent {
    
    public firstControlModel: number[];
    public firstControlOptions: IMultiSelectOption[] = [
        { id: 1, name: 'Option 1' },
        { id: 2, name: 'Option 2' },
        { id: 3, name: 'Option 3' }
    ];

    public secondControlModel: number[];
    public secondControlSettings: IMultiSelectSettings = {
        checkedStyle: 'fontawesome',
        buttonClasses: 'btn btn-secondary btn-block',
        dynamicTitleMaxItems: 3,
        displayAllSelectedText: true,
        showCheckAll: true,
        showUncheckAll: true
    };
    public secondControlTexts: IMultiSelectTexts = {
        checkAll: 'Select all',
        uncheckAll: 'Unselect all',
        checked: 'item selected',
        checkedPlural: 'items selected',
        searchPlaceholder: 'Find',
        defaultTitle: 'Select countries',
        allSelected: 'All selected',
    };
    public secondControlOptions: IMultiSelectOption[] = [
        { id: 1, name: 'Sweden'},
        { id: 2, name: 'Norway' },
        { id: 3, name: 'Canada' },
        { id: 4, name: 'USA' }
    ];


    public thirdControlModel: number[];
    public thirdControlSettings: IMultiSelectSettings = {
        enableSearch: true,
        checkedStyle: 'checkboxes',
        buttonClasses: 'btn btn-secondary btn-block',
        dynamicTitleMaxItems: 3,
        displayAllSelectedText: true
    };
    public thirdControlTexts: IMultiSelectTexts = {
        checkAll: 'Select all',
        uncheckAll: 'Unselect all',
        checked: 'item selected',
        checkedPlural: 'items selected',
        searchPlaceholder: 'Find...',
        defaultTitle: 'Select countries using search filter',
        allSelected: 'All selected',
    };
    public thirdControlOptions: IMultiSelectOption[] = [
        { id: 1, name: 'Sweden'},
        { id: 2, name: 'Norway' },
        { id: 3, name: 'Canada' },
        { id: 4, name: 'USA' }
    ];
    public onChange() {
        console.log(this.firstControlModel);
    }  


    //Basic datepicker
    public model: NgbDateStruct;
    public date: {year: number, month: number};    
    public selectToday() {
      this.model = {year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate()};
    }

    //Multiple months
    public displayMonths = 2;
    public navigation = 'select';

    //Datepicker in a popup
    public modelPopup: NgbDateStruct = {year: new Date().getFullYear(), month: new Date().getMonth()+1, day: new Date().getDate()-3};

    //Custom day view
    public modelCustom: NgbDateStruct;
    public isWeekend(date: NgbDateStruct) {
      const d = new Date(date.year, date.month - 1, date.day);
      return d.getDay() === 0 || d.getDay() === 6;
    }
    public isDisabled(date: NgbDateStruct, current: {month: number}) {
      return date.month !== current.month;
    }

    //Disabled datepicker
    public modelDisabled: NgbDateStruct = {year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate()};
    public disabled = true;


    //Basic timepicker
    public time = { hour: 13, minute: 30, second: 20 };

    //Meridian
    public timeMeridian = { hour: 15, minute: 20, second: 25 };
    public meridian: boolean = true;

    //Seconds
    public timeSeconds: NgbTimeStruct = { hour: 16, minute: 40, second: 30 };
    public seconds = true;

    //Spinners
    public timeSpinners = { hour: 13, minute: 30 };
    public spinners = true;

    //Custom steps
    public timeCustomSteps: NgbTimeStruct = { hour: 13, minute: 30, second: 0 };
    public hourStep = 1;
    public minuteStep = 15;
    public secondStep = 30;

    //Custom validation
    public timeValidation = { hour: 11, minute: 30 };
    public ctrl = new FormControl('', (control: FormControl) => {
      const value = control.value;
      if (!value) {
        return null;
      }
      if (value.hour < 12) {
        return {tooEarly: true};
      }
      if (value.hour > 13) {
        return {tooLate: true};
      }
      return null;
    });

    //Rating
    public currentRate = 8;

    //Events and readonly ratings
    public selected = 0;
    public hovered = 0;
    public readonly = false;

    //Custom star template
    public currentRateCustom = 6;

    //Custom decimal rating
    public currentRateDecimal = 3.14;

    //Form integration
    public ctrlFormIntegration = new FormControl(null, Validators.required);
    public toggle() {
      if (this.ctrlFormIntegration.disabled) {
        this.ctrlFormIntegration.enable();
      } else {
        this.ctrlFormIntegration.disable();
      }
    }
}
