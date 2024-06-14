import { Component, Input } from '@angular/core';
import { FeedbackService } from '../services/feedback.service';
import { Feedback } from '../models/feedback';
import { AuthService } from '../services/auth.service';
import { StorageService } from '../services/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent {

  @Input() recipeId: number = 0;

  feedbacks: Feedback[] = [];
  isLogin: boolean = false;
  newFeedback: Feedback = new Feedback(1);
  showReply: boolean = false;


  constructor(public feedbackService: FeedbackService,
    public storageService: StorageService,
    public router: Router
  ) { }

  ngOnInit(): void {
    this.getFeedbacks();
    this.isLogin = this.storageService.isLoggedIn();
  }

  getFeedbacks() {
    this.feedbackService.getFeedbacks(this.recipeId).subscribe(res => {
      this.feedbacks = res;
    })
  }

  postFeedback() {
    this.newFeedback.recipeId = this.recipeId;
    this.newFeedback.userId = this.storageService.getUser().id;
    this.feedbackService.add(this.newFeedback).subscribe(res => {
      this.getFeedbacks();
      this.newFeedback = new Feedback(1);
    })
  }


}
