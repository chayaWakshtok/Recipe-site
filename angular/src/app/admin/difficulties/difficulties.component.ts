import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Difficulty } from 'src/app/models/difficulty';
import { CategoryService } from 'src/app/services/category.service';
import { DifficultyService } from 'src/app/services/difficulty.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-difficulties',
  templateUrl: './difficulties.component.html',
  styleUrls: ['./difficulties.component.scss']
})
export class DifficultiesComponent {

  title = 'datatables';
  dtOptions: DataTables.Settings = {};
  difficulties: Difficulty[] = [];

  constructor(private difficultyService: DifficultyService,
    private cdRef: ChangeDetectorRef,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    this.getCategories();
  }

  getCategories() {
    this.difficultyService.getAll().subscribe(res => {
      this.difficulties = res;
      this.cdRef.detectChanges();
    })
  }

  delete(id: number | undefined) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.difficultyService.delete(id).subscribe(res => {
          if (res) {
            this.difficulties = res;
          }
          else {
            this.toastr.error('Fail to delete category', 'Fail delete!');
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  edit(id: number | undefined) {
    this.router.navigate(["admin/edit_difficult", id]);
  }

}
