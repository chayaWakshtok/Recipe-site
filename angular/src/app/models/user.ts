import { Like } from "./like";
import { Role } from "./role";

export class User {
  id: number | undefined;
  username!: string;
  status: number = 1;
  createAt!: Date;
  updateAt: Date | undefined;
  email!: string;
  password: string | undefined;
  picture: string | undefined;
  roleId!: number;
  firstName: string | undefined;
  role: Role = new Role();
  token: string | undefined;
  aboutMe:string | undefined;
  likes: Like[] = [];
  countRecipe: number = 0;
  countLikes: number = 0;
  countFollowFromUser: number = 0;
  countFollowToUser: number = 0;

}
