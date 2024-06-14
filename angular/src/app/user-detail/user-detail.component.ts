import { ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { StorageService } from '../services/storage.service';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { FollowService } from '../services/follow.service';
import { Follow } from '../models/follow';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent {
  user!: User;
  id: number = 0;
  isLogin: boolean = false;
  followId!: number;
  recipes: Recipe[] = [];
  constructor(
    private activatedRoute: ActivatedRoute,
    private storageService: StorageService,
    public recipeService: RecipeService,
    private router: Router,
    private userService: UserService,
    private followService: FollowService,
    private cdRef: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      const id = params['id'];
      this.id = id;
      var isLoggedIn = this.storageService.isLoggedIn();
      this.isLogin = isLoggedIn;
      this.getUser(id);
    });
  }

  getUser(id: number | undefined) {
    if (id) {
      this.recipeService.getAllByUserId(id).subscribe(res => {
        this.recipes = res;
      });

      this.userService.getUser(id).subscribe((res) => {
        this.user = res;
        this.cdRef.detectChanges();
        if (this.isLogin) {
          this.followService.getFromUser().subscribe((data: Follow[]) => {
            this.user.isUserFollow = data.some(
              (follow) => follow.toUser === this.user?.id
            );

            if (this.user.isUserFollow)
              this.followId = data.find(
                (follow) => follow.toUser === this.user?.id
              )!.id;
          });
        }

        this.cdRef.detectChanges();
      });
    }
  }

  addFollow() {
    if (!this.isLogin) this.router.navigate(['/auth/signin']);
    else {
      var follow = new Follow();
      follow.toUser = this.user!.id!;
      follow.fromUser = this.storageService.getUser().id;
      follow.fromUserNavigation = undefined;
      follow.toUserNavigation = undefined;
      this.followService.add(follow).subscribe((res) => {
        this.user!.isUserFollow = true;
        this.followId = res.find(
          (follow) => follow.toUser === this.user!.id
        )!.id;
        // Handle response if needed
      });
    }
  }

  removeFollow() {
    this.followService.delete(this.followId).subscribe((res) => {
      this.user!.isUserFollow = false;
      // Handle response if needed
    });
  }
}
