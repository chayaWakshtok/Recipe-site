export class Like {
  id: number | undefined;
  userId: number;
  recipeId: number;
  constructor(userId: number, recipeId: number, id?: number) {
    this.id = id;
    this.userId = userId;
    this.recipeId = recipeId;
  }
}
