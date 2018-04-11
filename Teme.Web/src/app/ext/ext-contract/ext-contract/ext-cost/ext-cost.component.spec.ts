import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtCostComponent } from './ext-cost.component';

describe('ExtCostComponent', () => {
  let component: ExtCostComponent;
  let fixture: ComponentFixture<ExtCostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtCostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtCostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
