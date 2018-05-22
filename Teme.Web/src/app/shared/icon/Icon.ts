import {IconRecord} from './IconRecord';

export class Icon {
  public id: number;
  public moduleType: number;
  public objectId: number;
  public isError: boolean;
  public fieldName: string;
  public iconRecords: Array<IconRecord>;
}
