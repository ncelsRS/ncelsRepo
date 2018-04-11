import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtManufacturerComponent } from './ext-manufacturer.component';

describe('ExtManufacturerComponent', () => {
  let component: ExtManufacturerComponent;
  let fixture: ComponentFixture<ExtManufacturerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtManufacturerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtManufacturerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
