import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Difficulty } from 'src/app/models/difficulty';
import { DifficultyService } from 'src/app/services/difficulty.service';

@Component({
  selector: 'app-add-difficult',
  templateUrl: './add-difficult.component.html',
  styleUrls: ['./add-difficult.component.scss']
})
export class AddDifficultComponent {
  difficult: Difficulty = new Difficulty(0, "", 1);

  constructor(private difService: DifficultyService,
    private router: Router,
    private toastr: ToastrService) {

  }

  saveDifficult() {
    this.difService.add(this.difficult).subscribe(res => {
      if (res.length > 0)
        this.router.navigate(["admin/difficulties"]);
      else {
        this.toastr.error('Fail to save new difficulty', 'Fail save!');

      }
    }, err => {
      this.toastr.error('Fail to save new difficulty', 'Fail save!');
    })
  }
}
