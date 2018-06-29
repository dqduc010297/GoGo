import { Component, OnInit } from '@angular/core';
import { RequestService } from '../request.service';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State, DataSourceRequestState } from '@progress/kendo-data-query';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.scss']
})
export class RequestListComponent implements OnInit {
  public view: GridDataResult;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 10
  };

  constructor(private _requestService: RequestService) {
    this._requestService.fetch(this.state).subscribe(result => {
      for (var i = 0; i < result.data.length; i++) {
        result.data[i].pickingDate = new Date(result.data[i].pickingDate);
        result.data[i].expectedDate = new Date(result.data[i].expectedDate);
      }
      this.view = result;
      console.log(result);
    });

  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this._requestService.fetch(this.state).subscribe(result => {
      for (var i = 0; i < result.data.length; i++) {
        result.data[i].pickingDate = new Date(result.data[i].pickingDate);
        result.data[i].expectedDate = new Date(result.data[i].expectedDate);
      }
      this.view = result;
    });
  }

  ngOnInit() {
  } 

}
