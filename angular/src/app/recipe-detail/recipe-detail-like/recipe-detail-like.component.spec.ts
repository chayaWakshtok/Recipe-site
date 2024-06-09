import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeDetailLikeComponent } from './recipe-detail-like.component';

describe('RecipeDetailLikeComponent', () => {
  let component: RecipeDetailLikeComponent;
  let fixture: ComponentFixture<RecipeDetailLikeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecipeDetailLikeComponent]
    });
    fixture = TestBed.createComponent(RecipeDetailLikeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
