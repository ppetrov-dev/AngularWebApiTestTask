import { WizardStepBaseModel } from "./wizard-step-base.model";

export class WizardStepOneModel extends WizardStepBaseModel {
  login: string = '';
  password: string = '';
  agreeWithTerms: boolean = false;
}
