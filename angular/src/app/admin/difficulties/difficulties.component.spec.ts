import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifficultiesComponent } from './difficulties.component';

describe('DifficultiesComponent', () => {
  let component: DifficultiesComponent;
  let fixture: ComponentFixture<DifficultiesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DifficultiesComponent]
    });
    fixture = TestBed.createComponent(DifficultiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
