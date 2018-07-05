import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestListComponent } from './request-list/request-list.component';
import { RequestFormComponent } from './request-form/request-form.component';
import { RequestRoutingModule } from './request-routing.module';
import { FormsModule } from '@angular/forms';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { ComboBoxModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';

@NgModule({
  imports: [
    CommonModule,
    RequestRoutingModule,
    FormsModule,
    DateInputsModule,
    ComboBoxModule,
    GridModule
  ],
  declarations: [
    RequestListComponent,
    RequestFormComponent,
  ]
})
export class RequestModule { }