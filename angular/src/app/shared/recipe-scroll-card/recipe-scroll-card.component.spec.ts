import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeScrollCardComponent } from './recipe-scroll-card.component';

describe('RecipeScrollCardComponent', () => {
  let component: RecipeScrollCardComponent;
  let fixture: ComponentFixture<RecipeScrollCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    imports: [RecipeScrollCardComponent]
})
    .compileComponents();

    fixture = TestBed.createComponent(RecipeScrollCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
