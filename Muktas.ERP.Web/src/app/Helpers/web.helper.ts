import { Http, Headers, Jsonp, RequestOptions, Response, URLSearchParams } from '@angular/http';
import * as AppConfig from '../Configurations/app.configuration';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';
import { Router } from '@angular/router';

export class WebHelper {

    router: Router;

    constructor(private _http: Http) { 

    }

    getAuthenticationHeader(auth: string): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        headers.append('Authorization', 'Basic ' + auth);
        return headers;
    }

    getHeader(): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        var token = localStorage.getItem("accessToken"); 
        headers.append("Token", token);
        return headers;
    }

    getFormHeader(): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        
        var token = localStorage.getItem("accessToken"); 
        headers.append("Token", token);
        return headers;
    }

    sendPostRequest(url: string, data: any) {

        return this._http.post(url, JSON.stringify(data),
            { headers: this.getHeader() })
            .map(this.extractData)
            .catch(this.handleError);   

    }

    sendFormPostRequest(url: string, data: any) {

        let requestOptions = new RequestOptions();
        requestOptions.headers = this.getFormHeader();
        requestOptions.params = data;

        return this._http.post(url, data, requestOptions)
            .map(this.extractData)
            .catch(this.handleError);   

    }

    sendGetRequest(url: string, data: any) {

        let requestOptions= new RequestOptions();
        requestOptions.headers = this.getHeader();
        requestOptions.params = data;
        return this._http.get(url, requestOptions)
            .map(this.extractData)
            .catch(this.handleError);
    }

    // This method parses the data to JSON
    extractData(res: Response) {
        let body;
        
        if (res["_body"].length > 0)// JSON.parse(JSON.stringify(res))["_body"].length > 0)
        {
            body = res.json();
        }
        return body || {};
    }

    handleError(error: Response | any) {
        if(error.status == '401') {
            console.log(10005);
            //window.location.href = AppConfig.BASE_URI_APP + "login";
        }

        // let errMsg: string;
        // if (error instanceof Response) {
        //     const body = error.json() || '';
        //     const err = body.error || JSON.stringify(body);
        //     errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        // } else {
        //     errMsg = error.message ? error.message : error.toString();
        // }
        // console.error(errMsg);
        //return Observable.throw(errMsg);
        return Observable.throw(error);
    }

}
