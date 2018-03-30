import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutBodyRightComponent } from './ext-layout-body-right.component';

describe('ExtLayoutBodyRightComponent', () => {
  let component: ExtLayoutBodyRightComponent;
  let fixture: ComponentFixture<ExtLayoutBodyRightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtLayoutBodyRightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtLayoutBodyRightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
