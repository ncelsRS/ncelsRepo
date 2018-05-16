import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpMenuComponent } from './exp-menu.component';

describe('ExpMenuComponent', () => {
    let component: ExpMenuComponent;
    let fixture: ComponentFixture<ExpMenuComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExpMenuComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExpMenuComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
