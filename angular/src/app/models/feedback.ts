import { User } from "./user";

export enum ETypeFeedback {
  Reply = 2,
  Comment = 1
}

export class Feedback {
  id!: number;
  createAt!: string | null;
  updateAt!: string | null;
  mark!: string | null;
  userId!: number | null;
  type!: ETypeFeedback | null;
  recipeId!: number | null;
  user!: User | null;
  feedbackId!: number | null;
  replys: Feedback[] | null = [];

  constructor(type: ETypeFeedback) {
    this.type = type;
  }

}
