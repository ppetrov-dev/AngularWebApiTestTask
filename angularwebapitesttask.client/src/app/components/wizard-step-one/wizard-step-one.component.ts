import { Component, OnInit, Input } from '@angular/core';
import { WizardStepOneModel } from './../../models/wizard-step-one.model';
import { FormBuilder, NgForm } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'wizard-step-one',
  templateUrl: './wizard-step-one.component.html',
  styleUrls: ['./wizard-step-one.component.css'],
  standalone: false,
})
export class WizardStepOneComponent implements OnInit {

  private validateSubscription!: Subscription;

  @Input() wizardStepOneModel!: WizardStepOneModel;
  @Input() validate!: Observable<void>;

  confirmedPassword = '';

  showErrors = false;

  @ViewChild(NgForm) form!: NgForm;

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.validateSubscription = this.validate.subscribe(() => {
      this.showErrors = true;
    });
  }

  ngAfterViewInit() {
    this.form.statusChanges?.subscribe(
      result => this.wizardStepOneModel.isValid = result === 'VALID'
    );
  }

  onLoginInput(login: string) {
    this.wizardStepOneModel.login = login;
  }

  onLoginChanged(login: string) {
    this.wizardStepOneModel.login = login;
  }
}
