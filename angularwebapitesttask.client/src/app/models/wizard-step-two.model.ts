import { WizardStepBaseModel } from "./wizard-step-base.model";

export interface WizardStepTwoModel extends WizardStepBaseModel {
  countryId: number;
  provinceId: number;
}
