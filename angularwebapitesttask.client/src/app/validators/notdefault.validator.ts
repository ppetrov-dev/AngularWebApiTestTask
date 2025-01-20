import { FormControl } from '@angular/forms';

import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[notDefault][ngModel]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ValidateDefaultnessDirective, multi: true }],
  standalone: false,
})
export class ValidateDefaultnessDirective implements Validator {
  validate(formControl: FormControl): ValidationErrors | null {
    return ValidateDefaultness(formControl);
  }
}

function ValidateDefaultness(control: FormControl) {
  if (!control) {
    return null;
  }

  if (control.errors && !control.errors['isDefault']) {
    return null;
  }

  const error = control.value === 0 ? { isDefault: true } : null;

  control.setErrors(error);

  return error;
}
