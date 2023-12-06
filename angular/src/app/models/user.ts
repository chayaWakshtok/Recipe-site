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
}
