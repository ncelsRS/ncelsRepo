import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtDeclarantComponent } from './ext-declarant.component';

describe('ExtDeclarantComponent', () => {
  let component: ExtDeclarantComponent;
  let fixture: ComponentFixture<ExtDeclarantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtDeclarantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtDeclarantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
