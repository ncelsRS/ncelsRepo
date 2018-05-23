import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import {ContentType} from '@angular/http/src/enums';
import {environment} from '../../../../environments/environment';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class RefIntContractService  {

  urlAdminApi = environment.urls.admin+"/api/reference/";
  urlContract = environment.urls.extContract+"/Contract/";
  urlContractActions = environment.urls.extContract;
  urlContractCoz = environment.urls.contractCoz+"/Contract/";;
  urlContractCozAction = environment.urls.contractCoz+"/"
  //urlContractDeclarantActions = environment.urls.contractDeclarantActions;



  constructor(private http: HttpClient,
              private toastr: ToastrService){
    this.getContractForm();
  }



  getOrgForm(){
    return  this.http.get(this.urlAdminApi+"OrganizationForm");
  }

  saveBank(nameKz:string,nameRu:string){
    let data = ({  nameRu: nameRu, nameKz: nameKz});
    console.log(data);
    console.log("rr");
    return  this.http.put(this.urlAdminApi+"SaveBank", data

    )

  }

  getBank(){
    return  this.http.get(this.urlAdminApi+"Bank");
  }

  getCountry(){
    return  this.http.get(this.urlAdminApi+"Country");
  }

  getCurrency(){
    return  this.http.get(this.urlAdminApi+"Currency");
  }

  getContractForm(){
    return  this.http.get(this.urlAdminApi+"ContractForm");
  }




  changeModel(obj){
    console.log("ww");
    console.log(obj);
    return  this.http.put(this.urlContract+"ChangeModel",obj
    );

  }

  GetListContracts(statusCode)
  {
    return  this.http.get(this.urlContractCoz+"GetListContract",{
      params: {
        statusCode: statusCode
      }
    })
  }

  GetContractById(Id){
    return  this.http.get(this.urlContractCoz+"GetContractById",{
      params: {
        Id: Id
      }
    });

  }

  GetDeclarantById(id){
    return  this.http.get(this.urlContractCoz+"GetDeclarantById",{
      params: {
        id: id
      }
    });

  }

  DistributionByExecutors(workFlowId, UserId){
    return  this.http.post(this.urlContractCozAction+"Actions/SelectExecutors/selectExecutors/"+workFlowId+"/"+UserId,{})
    ;

  };

  CozExecutorAgreedRequest(promt, option, workFlowId){
    return  this.http.post(this.urlContractCozAction+"Actions/"+promt+"/"+option+"/"+workFlowId,{})
      ;

  };

  CozExecutorNotAgreedRequest(promt, option, workFlowId){
    return  this.http.post(this.urlContractCozAction+"Actions/"+promt+"/"+option+"/"+workFlowId,{})
      ;

  };

  GetViewActions(workFlowId){
    return  this.http.get(this.urlContractActions+"/Actions/contract/"+workFlowId,{});

  }

  RegisterContract(workFlowId){
    return  this.http.post(this.urlContractCozAction+"Actions/RegisterContract/Register/"+workFlowId,{});

  }

  ReturnToDeclarant(workFlowId){
    return  this.http.post(this.urlContractCozAction+"Actions/ReturnToDeclarant/ReturnToDeclarant/"+workFlowId,{});

  }




}
