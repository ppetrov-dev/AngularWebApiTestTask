import { FormControl } from '@angular/forms';

import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[validateEmail][ngModel]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ValidateEmailDirective, multi: true }],
  standalone: false,
})
export class ValidateEmailDirective implements Validator {
  validate(formControl: FormControl): ValidationErrors | null {
    return ValidateEmail(formControl);
  }
}

const EMAIL_REGEXP: RegExp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

function ValidateEmail(control: FormControl) {
  if (!control) {
    return null;
  }

  if (control.errors && !control.errors['notEmail']) {
    return null;
  }

  const error = { notEmail: true };
  const emailTest = EMAIL_REGEXP.test(control.value);
  const setError = emailTest ? null : error;

  control.setErrors(setError);

  return setError;
}
