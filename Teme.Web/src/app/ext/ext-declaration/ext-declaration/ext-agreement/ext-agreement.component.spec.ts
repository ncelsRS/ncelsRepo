import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtAgreementComponent } from './ext-agreement.component';

describe('ExtAgreementComponent', () => {
    let component: ExtAgreementComponent;
    let fixture: ComponentFixture<ExtAgreementComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtAgreementComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtAgreementComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
