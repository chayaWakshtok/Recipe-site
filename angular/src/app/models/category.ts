export class Category {
  id: number;
  name: string;
  description: string;
  image?: string;
  status: number;

  constructor(id: number, name: string, description: string, image: string, status: number) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.image = image;
    this.status = status;
  }

}
