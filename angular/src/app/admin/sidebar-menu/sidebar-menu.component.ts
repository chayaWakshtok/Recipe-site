import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.scss']
})
export class SidebarMenuComponent implements OnInit {
  isSubMenu: boolean = false;
  user!: User;

  constructor(public storageService: StorageService) {
  }
  
  ngOnInit(): void {
    this.user = this.storageService.getUser();
  }



}
