import { Category } from "./category";
import { Difficulty } from "./difficulty";
import { Feedback } from "./feedback";
import { Ingredient } from "./ingredient";
import { Instruction } from "./instruction";
import { User } from "./user";


export class Recipe {
  id!: number;
  title!: string | null;
  description!: string | null;
  videoUrl!: string | null;
  servings!: number | null;
  prepTime!: number | null;
  calories!: number | null;
  fat!: number | null;
  protein!: number | null;
  carbs!: number | null;
  userId!: number | null;
  likes!: number | null;
  difficultyId!: number | null;
  status!: number | null;
  createAt!: string | null;
  updateAt!: string | null;
  categoryId!: number | null;
  category!: Category | null;
  difficulty!: Difficulty | null;
  feedbacks!: Feedback[];
  imageUrl!: string | null;
  ingredients!: Ingredient[];
  instructions!: Instruction[];
  user!: User | null;
  countLikes: number = 0;
}
