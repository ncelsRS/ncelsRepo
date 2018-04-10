import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtProducerComponent } from './ext-producer.component';

describe('ExtProducerComponent', () => {
  let component: ExtProducerComponent;
  let fixture: ComponentFixture<ExtProducerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtProducerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtProducerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
