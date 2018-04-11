import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtContractComponent } from './ext-contract.component';

describe('ExtContractComponent', () => {
    let component: ExtContractComponent;
    let fixture: ComponentFixture<ExtContractComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtContractComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtContractComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
