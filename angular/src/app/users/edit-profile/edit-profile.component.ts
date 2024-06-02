import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  user!: User;
  message: string = "";
  imageSrc: string = '';
  error: string = "";

  constructor(public userservice: UserService) {
    this.user = new User();
  }
  ngOnInit(): void {
    this.userservice.getUserDetail().subscribe(data => {
      this.user = data;
      this.imageSrc = this.user.picture ?? "";
    });
  }

  save() {
    this.user.likes = [];
    this.userservice.update(this.user).subscribe(data => {
      console.log(data);
      this.message = " Your profile was successfully updated";
      setTimeout(() => {
        location.reload();
      }, 2000);
    }, err => {
      this.message = " Your profile was not updated";
      this.error = err.error.message;
    });
  }

  uploadImage(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.user.picture = reader.result as string;
      }
    }
  }
}
