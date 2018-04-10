import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtDeclarationComponent } from './ext-declaration.component';

describe('ExtDeclarationComponent', () => {
  let component: ExtDeclarationComponent;
  let fixture: ComponentFixture<ExtDeclarationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExtDeclarationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtDeclarationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
