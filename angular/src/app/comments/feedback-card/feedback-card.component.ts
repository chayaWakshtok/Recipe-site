import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Feedback } from 'src/app/models/feedback';
import { FeedbackService } from 'src/app/services/feedback.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-feedback-card',
  templateUrl: './feedback-card.component.html',
  styleUrls: ['./feedback-card.component.scss']
})
export class FeedbackCardComponent {

  @Input() feedback!: Feedback;
  @Output() feedbacksChanged = new EventEmitter<Feedback[]>();
  @Input() recipeId!: number;

  showReply: boolean = false;
  replyFeedback: Feedback = new Feedback(2);
  isLogin: boolean = false;


  constructor(public feedbackService: FeedbackService,
    public storageService: StorageService,
    public router: Router
  ) {

  }

  ngOnInit(): void {
    this.isLogin = this.storageService.isLoggedIn();
  }

  clickReply() {
    if (this.isLogin)
      this.showReply = true;
    else {
      this.showReply = false;
      this.router.navigate(["/auth/login"]);
    }
  }

  postReply(feedback: number) {
    this.replyFeedback.feedbackId = feedback;
    this.replyFeedback.recipeId = this.recipeId;
    this.replyFeedback.userId = this.storageService.getUser().id;
    this.feedbackService.add(this.replyFeedback).subscribe(res => {
      this.feedbacksChanged.emit(res);
      this.showReply = false;
      this.replyFeedback = new Feedback(2);
    })
  }
}
