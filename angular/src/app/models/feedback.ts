import { User } from "./user";


export class Feedback {
  id!: number;
  createAt!: string | null;
  updateAt!: string | null;
  mark!: string | null;
  userId!: number | null;
  type!: number | null;
  recipeId!: number | null;
  user!: User | null;
}
