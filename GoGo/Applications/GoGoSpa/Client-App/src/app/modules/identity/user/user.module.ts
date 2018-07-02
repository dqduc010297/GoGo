import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserProfileEditComponent } from './user-profile-edit/user-profile-edit.component';
import { GridModule } from '@progress/kendo-angular-grid';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GridModule
  ],
  declarations: [UserListComponent, UserProfileComponent, UserDetailComponent, UserCreateComponent, UserEditComponent, UserProfileEditComponent]
})
export class UserModule { }
