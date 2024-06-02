export class Like {
  id: number;
  userId: number;
  recipeId: number;
  constructor(id: number, userId: number, recipeId: number) {
    this.id = id;
    this.userId = userId;
    this.recipeId = recipeId;
  }
}
