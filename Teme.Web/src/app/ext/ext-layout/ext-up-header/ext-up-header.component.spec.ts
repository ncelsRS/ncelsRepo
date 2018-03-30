import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtUpHeaderComponent } from './ext-up-header.component';

describe('ExtUpHeaderComponent', () => {
  let component: ExtUpHeaderComponent;
  let fixture: ComponentFixture<ExtUpHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtUpHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtUpHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
