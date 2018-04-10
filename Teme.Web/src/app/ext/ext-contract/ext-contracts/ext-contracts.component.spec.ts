import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtContractsComponent } from './ext-contracts.component';

describe('ExtContractsComponent', () => {
    let component: ExtContractsComponent;
    let fixture: ComponentFixture<ExtContractsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtContractsComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtContractsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
