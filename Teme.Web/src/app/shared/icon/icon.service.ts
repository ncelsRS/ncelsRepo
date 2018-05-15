import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable()
export class IconService{
  iconUrl:string = environment.urls.icon;

  constructor(private http: HttpClient){ }

  getIconRecords(ModuleType: string, ObjectId: string, FieldName: string){

    // let params = new HttpParams().set('ModuleType', ModuleType);
    // let Params = new HttpParams();
    //
    // // Begin assigning parameters
    // Params = Params.append('firstParameter', parameters.valueOne);
    // Params = Params.append('secondParameter', parameters.valueTwo);

    //return this._HttpClient.get(`${API_URL}/api/v1/data/logs`, { params: params })
    return this.http.get( this.iconUrl + 'GetIconRecords',{ params: {ModuleType, ObjectId, FieldName} })
  }

  //
  // getCountry(){
  //   return this.http.get( this.referenceUrl + 'Country')
  // }
  //
  // getCalculatorServiceType(applicationTypeId:string){
  //   const params = {applicationTypeId: applicationTypeId};
  //   return this.http.get( this.referenceUrl + 'CalculatorServiceType',{ params: params} )
  // }


}
