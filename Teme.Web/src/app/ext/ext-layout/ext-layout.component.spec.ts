import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutComponent } from './ext-layout.component';

describe('ExtLayoutComponent', () => {
    let component: ExtLayoutComponent;
    let fixture: ComponentFixture<ExtLayoutComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtLayoutComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtLayoutComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
