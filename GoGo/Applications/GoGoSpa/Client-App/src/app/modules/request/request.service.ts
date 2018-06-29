import { Injectable } from '@angular/core';
import { ICreateFormService, IViewFormService, IUpdateFormService, IDataSourceService } from '../../shared/component/form';
import { Observable, observable } from 'rxjs';
import { AuthHttpService } from '../../shared';
import { HttpClient } from '@angular/common/http';
import { DataSourceRequestState, DataResult, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import 'rxjs/add/operator/map'
@Injectable({
  providedIn: 'root'
})
export class RequestService  implements ICreateFormService, IViewFormService, IUpdateFormService, IDataSourceService  {
  
  getDataSource(): Observable<any> { 
    return this._apiHttp.get(`/api/warehouse/customer/77`);
  }

  filterWarehouseList(displayName: string): Observable<any>{
    return this._apiHttp.get(`/api/warehouse/filter-list/${displayName}`);
  }

  edit(id: any, formData: any): Observable<any> {
    return this._apiHttp.put(`/api/request/${id}`, formData);
  }

  getFormData(id: any): Observable<any> { // for view
    return this._apiHttp.get(`/api/request/${id}`);
  }

  create(formData: any): Observable<any> {
    return this._apiHttp.post(`/api/request`, formData);
  }

  getViewFormUrl(id: any): string {
    return `/request/form/view/${id}`;
  }

  getListPageUrl(): string {
    return `/request/list`;
  }

  fetch(state: DataSourceRequestState): Observable<DataResult> {
    const queryStr = `${toDataSourceRequestString(state)}`; // Serialize the state
    const hasGroups = state.group && state.group.length;

    return this._apiHttp
      .get(`/api/request/?${queryStr}`) 
      .map(({ data, total}: GridDataResult) => 
        (<GridDataResult>{
          data: hasGroups ? translateDataSourceResultGroups(data) : data,
          total: total,
        })
      )
  }

  constructor(private _apiHttp: AuthHttpService) { }
}
