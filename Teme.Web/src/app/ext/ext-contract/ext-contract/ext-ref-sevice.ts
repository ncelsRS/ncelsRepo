import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/Rx';
import 'rxjs/add/operator/map';
import {ContentType} from '@angular/http/src/enums';
import {environment} from '../../../../environments/environment';


@Injectable()
export class RefService  {

  urlAdminApi = environment.urls.admin+"/api/reference/";
  urlContract = environment.urls.extContract+"/Contract/";
  DeclarationActions = environment.urls.extContract;



  constructor(private http: HttpClient){
    this.getContractForm();
  }

  saveOrgForm(nameKz:string,nameRu:string){

    return  this.http.put(this.urlAdminApi+"SaveOrganizationForm",  {
   },{
    params: {
      nameRu: nameKz,
      nameKz: nameRu
      // observe: 'response'
    },
      headers:   { "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
          "Content-Type": "application/json; charset=utf-8",
          "Accept": "application/json",
          "Access-Control-Allow-Credentials": "true",
        }
    })

  }

  getOrgForm(){
    return  this.http.get(this.urlAdminApi+"OrganizationForm");
  }

  saveBank(nameKz:string,nameRu:string){
     let data = ({  nameRu: nameRu, nameKz: nameKz});
     console.log(data);
    console.log("rr");
    return  this.http.put(this.urlAdminApi+"SaveBank", {},{
      params:{
        nameRu: nameKz,
        nameKz: nameRu
      }
      }

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

  getChosenPayer(){
    return  this.http.get(this.urlAdminApi+"ChosenPayer");
  }

  getHolderType(){
    return  this.http.get(this.urlAdminApi+"HolderType");
  }


  getCalculatorApplicationType(contractScope:string,contractForm:string){

    return  this.http.get(this.urlAdminApi+"CalculatorApplicationType",{
      params: {
        contractScope: contractScope,
        contractForm: contractForm
       }
    })

  }

  getCalculatorServiceType(applicationTypeId:string){

    return  this.http.get(this.urlAdminApi+"CalculatorServiceType",{
      params: {
        applicationTypeId: applicationTypeId,

      }
    })

  }

  getShowPriceList(isImport:string, serviceTypeId: string, serviceTypeModifId?: string){

    return  this.http.get(this.urlAdminApi+"ShowPriceList",{
      params: {
        isImport: isImport,
        serviceTypeId:serviceTypeId,
        serviceTypeModifId: serviceTypeModifId
      }

    })

  }

  createContract(cntType, cntScope){
    return  this.http.post(this.urlContract+"Create", {
          contractType: cntType,
          contractScope: cntScope

      }
    );
  }

  changeModel(obj){
    console.log("ww");
    console.log(obj);
     return  this.http.put(this.urlContract+"ChangeModel",obj
    );

  }

  AddDeclarant(contractId,code){
    return  this.http.get(this.urlContract+"AddDeclarant",{
      params: {
        contractId: contractId,
        code:code
    }});
  }

  SearchDeclarantResident(iin){
    return  this.http.get(this.urlContract+"SearchDeclarantResident",{
      params: {
        iin: iin
      }});
  }

  SearchDeclarantNonResident(countryId){
    return  this.http.get(this.urlContract+"SearchDeclarantNonResident",{
      params: {
        countryId: countryId
      }
    });
  }

  GetListContracts(contractScope){
    return  this.http.get(this.urlContract+"GetListContracts",{
      params: {
        contractScope: contractScope
      }
      // ,
      // headers:   { "Access-Control-Allow-Origin": "*",
      //   "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
      //   "Content-Type": "application/json; charset=utf-8",
      //   "Accept": "application/json",
      //   "Access-Control-Allow-Credentials": "true",
      // }
    });

  }

  GetContractById(contractId){
    return  this.http.get(this.urlContract+"GetContractById",{
      params: {
        contractId: contractId
      }
    });

  }

  GetDeclarantById(id){
    return  this.http.get(this.urlContract+"GetDeclarantById",{
      params: {
        id: id
      }
    });

  }

  SaveCostWorked(obj){
    return  this.http.put(this.urlContract+"SaveCostWork",obj)

  }

  DeleteCostWork(contractId){
    return  this.http.delete(this.urlContract+"DeleteCostWork",{
     params: {
       contractId: contractId
     }
    })
  }

  SendOrRemoveSendWithSign(workflowId, contractType, contractId){
    return  this.http.delete(this.urlContract+"SendOrRemoveSendWithoutSign",{
      params: {
        workflowId: workflowId,
        contractType:contractType,
        contractId: contractId
      }
    })
  }

  SendOrRemoveSendWithoutSign(workflowId, contractType){
    console.log(workflowId);
    return  this.http.post(this.DeclarationActions+"/DeclarantActions/SendOrRemove/sendWithoutSign/"+contractType+"/"+workflowId,{})


  }
}
