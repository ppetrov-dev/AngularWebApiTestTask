import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WizardRegistrationSuccessComponent } from './wizard-registration-success.component';

describe('WizardRegistrationSuccessComponent', () => {
  let component: WizardRegistrationSuccessComponent;
  let fixture: ComponentFixture<WizardRegistrationSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WizardRegistrationSuccessComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WizardRegistrationSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
