import {Injectable} from '@angular/core';
import {environment} from '../../../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class EquipmentImnTableService{
  urlEquipment = environment.urls.common + '/Equipment/';

  constructor(private http: HttpClient) {}

  saveEquipment(row, paymentId) {
    return this.http.put(this.urlEquipment + 'Add', {},
      {
        params: {
          PaymentId: paymentId, EquipmentTypeId: row.equipmentType.id, Name: row.name, Code: row.code,
          Model: row.model, Manufacturer: row.manufacturer, CountryId: row.country.id
        }
      });
  }

  updateEquipment(row, paymentId) {
    return this.http.post(this.urlEquipment + 'Update', {},
      {
        params: {
          Id: row.id, PaymentId: paymentId, EquipmentTypeId: row.equipmentType.id, Name: row.name, Code: row.code,
          Model: row.model, Manufacturer: row.manufacturer, CountryId: row.country.id
        }
      });
  }

  deleteEquipment(row, paymentId){
    return this.http.post(this.urlEquipment + 'Delete', {},
      {
        params: {
          Id: row.id, PaymentId: paymentId, EquipmentTypeId: row.equipmentType.id, Name: row.name, Code: row.code,
          Model: row.model, Manufacturer: row.manufacturer, CountryId: row.country.id, isDeleted: "true",
        }
      });
  }

}
