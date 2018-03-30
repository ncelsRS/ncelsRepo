import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutFooterComponent } from './ext-layout-footer.component';

describe('ExtLayoutFooterComponent', () => {
  let component: ExtLayoutFooterComponent;
  let fixture: ComponentFixture<ExtLayoutFooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtLayoutFooterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtLayoutFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
