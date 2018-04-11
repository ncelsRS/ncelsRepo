import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtImnSetComponent } from './ext-imn-set.component';

describe('ExtImnSetComponent', () => {
    let component: ExtImnSetComponent;
    let fixture: ComponentFixture<ExtImnSetComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtImnSetComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtImnSetComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
