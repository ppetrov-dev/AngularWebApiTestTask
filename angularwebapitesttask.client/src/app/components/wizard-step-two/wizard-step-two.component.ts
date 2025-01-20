import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { CountryService } from './../../services/country.service';
import { ProvinceService } from './../../services/province.service';
import { Observable, Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';
import { Country } from './../../models/country.model';
import { WizardStepTwoModel } from './../../models/wizard-step-two.model';
import { Province } from './../../models/province.model';

@Component({
  selector: 'wizard-step-two',
  templateUrl: './wizard-step-two.component.html',
  styleUrls: ['./wizard-step-two.component.css'],
  standalone: false,
})
export class WizardStepTwoComponent implements OnInit {
  private validateSubscription!: Subscription;

  countries: Country[] = [];
  countriesToProvinces: { [key: string]: Province[] } = {};

  @Input() wizardStepTwoModel!: WizardStepTwoModel;
  @Input() validate!: Observable<void>;

  showErrors = false;

  @ViewChild(NgForm) form!: NgForm;

  constructor(private countryService: CountryService, private provinceService: ProvinceService) {
  }

  ngOnInit(): void {
    this.getCountries();
    this.wizardStepTwoModel.countryId = 0;
    this.wizardStepTwoModel.provinceId = 0;

    this.validateSubscription = this.validate.subscribe(() => {
      this.showErrors = true;
    });
  }

  getCountries(): void {
    this.countryService.getCountries().subscribe(data => {
      this.countries = data.countries;
    });
  }

  ngAfterViewInit() {
    this.form.statusChanges?.subscribe(
      result => this.wizardStepTwoModel.isValid = result === 'VALID'
    );
  }

  onCountryChange(countryId: number) {
    this.wizardStepTwoModel.provinceId = 0;

    if (this.countriesToProvinces[countryId])
      return;

    this.provinceService.getProvinces(countryId).subscribe(data => {
      this.countriesToProvinces[countryId] = data.provinces;
    });
  }
}
