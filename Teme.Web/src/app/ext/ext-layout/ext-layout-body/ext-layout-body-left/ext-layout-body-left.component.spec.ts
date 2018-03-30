import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutBodyLeftComponent } from './ext-layout-body-left.component';

describe('ExtLayoutBodyLeftComponent', () => {
  let component: ExtLayoutBodyLeftComponent;
  let fixture: ComponentFixture<ExtLayoutBodyLeftComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtLayoutBodyLeftComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtLayoutBodyLeftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
