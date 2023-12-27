import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDifficultComponent } from './edit-difficult.component';

describe('EditDifficultComponent', () => {
  let component: EditDifficultComponent;
  let fixture: ComponentFixture<EditDifficultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditDifficultComponent]
    });
    fixture = TestBed.createComponent(EditDifficultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
