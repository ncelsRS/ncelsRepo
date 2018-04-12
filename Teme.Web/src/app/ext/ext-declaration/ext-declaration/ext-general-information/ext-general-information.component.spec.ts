import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtGeneralInformationComponent } from './ext-general-information.component';

describe('ExtGeneralInformationComponent', () => {
    let component: ExtGeneralInformationComponent;
    let fixture: ComponentFixture<ExtGeneralInformationComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtGeneralInformationComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtGeneralInformationComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
