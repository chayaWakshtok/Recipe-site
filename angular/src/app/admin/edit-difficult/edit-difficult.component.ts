import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Difficulty } from 'src/app/models/difficulty';
import { DifficultyService } from 'src/app/services/difficulty.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-difficult',
  templateUrl: './edit-difficult.component.html',
  styleUrls: ['./edit-difficult.component.scss']
})
export class EditDifficultComponent {
  difficulty!: Difficulty;

  constructor(private diffService: DifficultyService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (id) {
      this.diffService.getDifficulty(id).subscribe(res => {
        this.difficulty = res;
      })
    }
  }

  deleteDifficulty() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.diffService.delete(this.difficulty.id).subscribe(res => {
          if (!res) {
            this.toastr.error('Fail to delete difficulty', 'Fail delete!');
          }
          else {
            this.router.navigate(["admin/difficulties"]);
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  updateCat() {
    this.diffService.update(this.difficulty).subscribe(res => {
      if (res)
        this.router.navigate(["admin/difficulties"]);
      else
        this.toastr.error('Fail to update difficulty', 'Fail update!');
    })
  }

}
