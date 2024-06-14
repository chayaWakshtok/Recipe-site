import { User } from "./user";

export class Follow {
  id!: number;
  toUser!: number;
  fromUser!: number;
  fromUserNavigation?: User = new User();
  toUserNavigation?: User = new User();

}
