import { Product } from "./product";

export class Ingredient {
  id!: number;
  count!: number | null;
  typeCount!: ETypeCount | null;
  recipeId!: number | null;
  productId!: number;
  product: Product = new Product();
}

export enum ETypeCount {
  Gram = 1,
  Pound = 2,
  Kilogram = 3,
  Pinch = 4,
  Liter = 5,
  Fluidounce = 6,
  Gallon = 7,
  Pint = 8,
  Quart = 9,
  Milliliter = 10,
  Cup = 11,
  Tablespoon = 12,
  Teaspoon = 13,
}
