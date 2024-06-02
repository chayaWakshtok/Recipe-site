import { Component, OnInit } from '@angular/core';
import { Follow } from 'src/app/models/follow';
import { FollowService } from 'src/app/services/follow.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.scss']
})
export class FollowingComponent implements OnInit {

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
    this.followService.getFromUser().subscribe(data => {
      this.follows = data;
    });
  }

}
