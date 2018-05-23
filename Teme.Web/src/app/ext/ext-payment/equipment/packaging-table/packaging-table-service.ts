import {Injectable} from '@angular/core';
import {environment} from '../../../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class PackagingTableService{
  urlPackaging = environment.urls.common + '/Packaging/';

  constructor(private http: HttpClient) {}

  savePackaging(row, paymentId) {
    return this.http.put(this.urlPackaging + 'Add', {},
      {
        params: {
          PaymentId: paymentId, Name: row.name, SizeWidth: row.sizeWidth,
          SizeHeight: row.sizeHeight, SizeLength: row.sizeLength, SizeMeasureId: row.sizeMeasure.id,
          numberUnitsInBox: row.numberUnitsInBox, ShortDescription: row.shortDescription,
          PackagingTypeId: row.packagingType.id
        }
      });
  }

  updatePackaging(row, paymentId) {
    return this.http.post(this.urlPackaging + 'Update', {},
      {
        params: {
          Id: row.id, Name: row.name, SizeWidth: row.sizeWidth, SizeHeight: row.sizeHeight, SizeLength: row.sizeLength,
          SizeMeasureId: row.sizeMeasure.id, numberUnitsInBox: row.numberUnitsInBox, ShortDescription: row.shortDescription,
          PaymentId: paymentId, PackagingTypeId: row.packagingType.id
        }
      });
  }

  deletePackaging(row, paymentId){
    return this.http.post(this.urlPackaging + 'Delete', {},
      {
        params: {
          Id: row.id, Name: row.name, SizeWidth: row.sizeWidth, SizeHeight: row.sizeHeight, SizeLength: row.sizeLength,
          SizeMeasureId: row.sizeMeasure.id, numberUnitsInBox: row.numberUnitsInBox, ShortDescription: row.shortDescription,
          PaymentId: paymentId, PackagingTypeId: row.packagingType.id, isDeleted: "true",
        }
      });
  }
}
