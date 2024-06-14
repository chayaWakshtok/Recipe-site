import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyRecipeCardComponent } from './my-recipe-card.component';

describe('MyRecipeCardComponent', () => {
  let component: MyRecipeCardComponent;
  let fixture: ComponentFixture<MyRecipeCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyRecipeCardComponent]
    });
    fixture = TestBed.createComponent(MyRecipeCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
