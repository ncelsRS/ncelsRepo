import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutBodyComponent } from './ext-layout-body.component';

describe('ExtLayoutBodyComponent', () => {
  let component: ExtLayoutBodyComponent;
  let fixture: ComponentFixture<ExtLayoutBodyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtLayoutBodyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtLayoutBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
