import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDifficultComponent } from './add-difficult.component';

describe('AddDifficultComponent', () => {
  let component: AddDifficultComponent;
  let fixture: ComponentFixture<AddDifficultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddDifficultComponent]
    });
    fixture = TestBed.createComponent(AddDifficultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
