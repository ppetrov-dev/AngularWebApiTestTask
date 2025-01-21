import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WizardMainComponent } from './components/wizard-main/wizard-main.component'
import { WizardStepOneComponent } from './components/wizard-step-one/wizard-step-one.component'
import { WizardStepTwoComponent } from './components/wizard-step-two/wizard-step-two.component'
import { WizardRegistrationSuccessComponent } from './components/wizard-registration-success/wizard-registration-success.component'

import { MustMatchDirective } from './validators/mustmatch.validator';
import { ValidateEmailDirective } from './validators/email.validator';
import { ValidatePassDirective } from './validators/pass.validator';
import { ValidateDefaultnessDirective } from './validators/notdefault.validator';

import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatError } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    WizardMainComponent,
    WizardStepOneComponent,
    WizardStepTwoComponent,
    MustMatchDirective,
    ValidateEmailDirective,
    ValidatePassDirective,
    ValidateDefaultnessDirective,
    WizardRegistrationSuccessComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatProgressBarModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatInputModule,
    MatCheckboxModule,
    MatError,
    MatSelectModule,
    MatIconModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
