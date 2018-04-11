import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtDeclarationsComponent } from './ext-declarations.component';

describe('ExtDeclarationsComponent', () => {
    let component: ExtDeclarationsComponent;
    let fixture: ComponentFixture<ExtDeclarationsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExtDeclarationsComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExtDeclarationsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
