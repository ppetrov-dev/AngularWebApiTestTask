import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WizardMainComponent } from './wizard-main.component';

describe('WizardMainComponent', () => {
  let component: WizardMainComponent;
  let fixture: ComponentFixture<WizardMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WizardMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WizardMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
