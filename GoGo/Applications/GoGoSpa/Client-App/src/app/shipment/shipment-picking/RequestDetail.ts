import { Location } from "@angular/common";

// TODO: rename file name to lower first letter
export class requestDetail {
  id: number;
  code: string;
  status: string;
  estimateDate: Date;
  address: string;
  location: any;
  requestOrder: number;
  isProblem: boolean;
  //message: string;
}
