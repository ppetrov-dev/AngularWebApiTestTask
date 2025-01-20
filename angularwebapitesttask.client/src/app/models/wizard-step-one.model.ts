import { WizardStepBaseModel } from "./wizard-step-base.model";

export interface WizardStepOneModel extends WizardStepBaseModel {
  login: string;
  password: string;
  agreeWithTerms: boolean;
}
