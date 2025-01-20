import { Component, OnInit } from '@angular/core';
import { WizardStepOneModel } from '../../models/wizard-step-one.model';
import { WizardStepTwoModel } from '../../models/wizard-step-two.model';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { WizardStepBaseModel } from '../../models/wizard-step-base.model';
import { UserService } from '../../services/user.service';
import { CreateUserResult } from '../../models/user.model';

@Component({
  selector: 'wizard-main',
  templateUrl: './wizard-main.component.html',
  styleUrls: ['./wizard-main.component.css'],
  standalone: false,
})

export class WizardMainComponent implements OnInit {
  eventsSubject: Subject<void> = new Subject<void>();

  wizardStepOneModel: WizardStepOneModel = { login: '', password: '', agreeWithTerms: false, isValid: false };
  wizardStepTwoModel: WizardStepTwoModel = { countryId: 0, provinceId: 0, isValid: false };

  steps: WizardStepBaseModel[] = [this.wizardStepOneModel, this.wizardStepTwoModel]
  currentStep: number = 0;

  registrationError: string = '';

  constructor(private router: Router, private userService: UserService) {
  }

  ngOnInit(): void {
  }

  onNextStep() {
    this.eventsSubject.next();

    if (!this.steps[this.currentStep].isValid) {
      return;
    }

    if (this.isLastStep()) {
      let data = {
        login: this.wizardStepOneModel.login,
        password: this.wizardStepOneModel.password,
        provinceId: this.wizardStepTwoModel.provinceId
      };

      this.userService.addUser(data).subscribe(
        (result: CreateUserResult) => this.currentStep++,
        error => {
          let errorMessage = `Server returned code: ${error.status}, message is: ${error.message} `;
          if (error.error && error.error.detail) 
            errorMessage += `. Detailed error is: ${error.error.detail}`
          this.registrationError = errorMessage;
        }
      );
    } else {
      this.currentStep++;
    }
  }

  showButtonLabel() {
    return this.isLastStep() ? 'Save' : 'Go to Step 2';
  }

  isLastStep() {
    return this.steps.length - 1 === this.currentStep;
  }

  onSubmit(): void {
    this.router.navigate(['/complete']);
  }

  isCompleted() {
    return this.currentStep >= this.steps.length;
  }
}
