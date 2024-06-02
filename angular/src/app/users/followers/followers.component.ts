import { Component } from '@angular/core';
import { Follow } from 'src/app/models/follow';
import { FollowService } from 'src/app/services/follow.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.scss']
})
export class FollowersComponent {

  dtOptions: DataTables.Settings = {};
  follows: Follow[] = [];

  constructor(public followService: FollowService) { }
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'simple_numbers',
      paging: true,
      responsive: true,
      processing: true,
      pageLength: 10,
    };
    this.followService.getToUser().subscribe(data => {
      this.follows = data;
    });
  }
}
