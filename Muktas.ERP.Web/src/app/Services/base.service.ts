import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import { WebHelper } from '../Helpers/web.helper';
import * as AppConfig from '../Configurations/app.configuration';

@Injectable()
export class BaseService { 

    _tablename: string;
    _webHelper: WebHelper;
    _http: Http;

    constructor(tablename: string, http: Http) {
        this._tablename = tablename;
        this._http = http;
        this._webHelper = new WebHelper(http);
    }

    FindByID(id: any)
    {        
        //return this.GetDataByField(this._tablename + '/Get'+this._tablename+'/',this._tablename + 'Id', id);
        return this.GetDataByField(this._tablename + "/FindByID",'Id', id);
    }

    FindAll()
    {
        //return this.GetData(this._tablename + '/Get'+this._tablename+'s/');
        return this.GetData(this._tablename + '/FindAll');
    }

    FindAllActive()
    {
        return this.GetData(this._tablename + '/GetActive'+this._tablename+'s/');
    }

    Add(value: any)
    {
        return this.PostData(this._tablename + '/Add'+this._tablename,value);
    }

    Remove(id: any)
    {
        return this.PostValue(this._tablename + '/Remove'+this._tablename+'/', this._tablename + 'Id', id);
    }

    Update(value: any)
    {
        return this.PostData(this._tablename + '/Update'+this._tablename+'/',value);
    }

    PostValue(serviceUrl: string, fieldName: string, value: any)
    {
      let data = new URLSearchParams();
      data.append(fieldName, value);
      return this._webHelper.sendFormPostRequest(AppConfig.BASE_URI_API + serviceUrl, data);
    }
    PostData(serviceUrl: string, value: any)
    {
      return this._webHelper.sendPostRequest(AppConfig.BASE_URI_API + serviceUrl, value);
    }

    GetData(serviceUrl: string)
    {
      let data = new URLSearchParams();
      return this._webHelper.sendGetRequest(AppConfig.BASE_URI_API + serviceUrl, data);
    }

    GetDataByField(serviceUrl: string, fieldName: string, value: any)
    {
      let data = new URLSearchParams();
      data.append(fieldName, value);
      return this._webHelper.sendGetRequest(AppConfig.BASE_URI_API + serviceUrl, data);
    }
    GetDataBy2Field(serviceUrl: string, fieldName1: string, value1: any, fieldName2: string, value2: any)
    {
      let data = new URLSearchParams();
      data.append(fieldName1, value1);
      data.append(fieldName2, value2);
      return this._webHelper.sendGetRequest(AppConfig.BASE_URI_API + serviceUrl, data);
    }

}