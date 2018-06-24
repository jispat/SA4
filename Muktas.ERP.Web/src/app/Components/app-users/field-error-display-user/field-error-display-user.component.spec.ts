import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FieldErrorDisplayUserComponent } from './field-error-display-user.component';

describe('FieldErrorDisplayUserComponent', () => {
  let component: FieldErrorDisplayUserComponent;
  let fixture: ComponentFixture<FieldErrorDisplayUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FieldErrorDisplayUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldErrorDisplayUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
