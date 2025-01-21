import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'wizard-registration-success',
  templateUrl: './wizard-registration-success.component.html',
  styleUrls: ['./wizard-registration-success.component.css'],
  standalone: false,
})
export class WizardRegistrationSuccessComponent implements OnInit {
  @Output() back = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onBack() {
    this.back.emit();
  }
}
