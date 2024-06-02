import { Component, OnInit } from '@angular/core';
import { Like } from 'src/app/models/like';
import { Recipe } from 'src/app/models/recipe';
import { User } from 'src/app/models/user';
import { LikeService } from 'src/app/services/like.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  likes: Like[] = [];

  constructor(public userService: UserService,
    public recipeService: RecipeService,
    public likeService: LikeService
  ) { }

  ngOnInit(): void {
    this.dtOptions = {
      responsive: true,
      processing: true,
      info: false,
      ordering: false,
      paging: false,
      searching: false
    };
    this.likeService.getLikesByUser().subscribe(data => {
      this.likes = data
    })
  }

  removeLike(id: number) {
    this.likeService.delete(id).subscribe(res => {
      if (res) this.likeService.getLikesByUser().subscribe(data => {
        this.likes = data
      })
    });
  }

}
