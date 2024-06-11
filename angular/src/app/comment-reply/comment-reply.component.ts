import { Component, Input } from '@angular/core';
import { Feedback } from '../models/feedback';

@Component({
  selector: 'app-comment-reply',
  templateUrl: './comment-reply.component.html',
  styleUrls: ['./comment-reply.component.scss']
})
export class CommentReplyComponent {

  @Input() feedbacks: Feedback[] = [];

}
