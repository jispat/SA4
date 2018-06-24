import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-field-error-display-user',
  templateUrl: './field-error-display-user.component.html',
  styleUrls: ['./field-error-display-user.component.css']
})
export class FieldErrorDisplayUserComponent {

  @Input() errorMsg: string;
  @Input() displayError: boolean;

}
