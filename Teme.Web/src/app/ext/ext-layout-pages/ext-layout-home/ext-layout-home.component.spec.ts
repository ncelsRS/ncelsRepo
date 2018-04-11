import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtLayoutHomeComponent } from './ext-layout-home.component';

describe('ExtLayoutBodyComponent', () => {
    let component: ExtLayoutHomeComponent;
    let fixture: ComponentFixture<ExtLayoutHomeComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtLayoutHomeComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtLayoutHomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
