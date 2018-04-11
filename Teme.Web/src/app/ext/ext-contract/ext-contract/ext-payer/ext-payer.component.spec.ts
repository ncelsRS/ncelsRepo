import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtPayerComponent } from './ext-payer.component';

describe('ExtPayerComponent', () => {
  let component: ExtPayerComponent;
  let fixture: ComponentFixture<ExtPayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtPayerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtPayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
