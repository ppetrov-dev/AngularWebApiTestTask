import { FormControl } from '@angular/forms';

import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[validatePass][ngModel]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ValidatePassDirective, multi: true }],
  standalone: false,
})

export class ValidatePassDirective implements Validator {
  validate(formControl: FormControl): ValidationErrors | null {
    return ValidatePass(formControl);
  }
}

const MINPASS_REGEXP: RegExp = new RegExp(/^.*(?=.*\d)(?=.*[A-Z]).*$/);

function ValidatePass(control: FormControl) {
  if (!control) {
    return null;
  }

  if (control.errors && !control.errors['weakPass']) {
    return null;
  }

  const error = { weakPass: true };
  const minpassTest = MINPASS_REGEXP.test(control.value);
  const setError = minpassTest ? null : error;

  control.setErrors(setError);

  return setError;
}
