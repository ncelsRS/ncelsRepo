import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtAttachmentComponent } from './ext-attachment.component';

describe('ExtAttachmentComponent', () => {
  let component: ExtAttachmentComponent;
  let fixture: ComponentFixture<ExtAttachmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtAttachmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtAttachmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
