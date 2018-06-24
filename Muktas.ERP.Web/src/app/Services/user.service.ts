import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ResetPasswordModel } from '../Models/resetpassword.model';
import { ChangePasswordModel } from '../Models/changepassword.model';

import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import { UserModel } from '../Models/user.model';
import * as appConfig from '../Configurations/app.configuration';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class UserService extends BaseService {

    _userLoginURL: string = appConfig.BASE_URI_API + 'get/token';

    constructor(http: Http) {
        super('User', http);
    }

    ForgotPassword(email: string) {
		let serviceUrl = "User/ForgotPassword";
		return super.PostValue(serviceUrl,"email",email);
	}

    CheckResetPasswordCode(code: string) {
		let serviceUrl = "User/CheckResetPasswordCode";
		return super.PostValue(serviceUrl,"code",code);
	}
    
    ResetPassword(ResetPassword: ResetPasswordModel) {
		let serviceUrl = "User/ResetPassword";
		return super.PostData(serviceUrl,ResetPassword);
	}

    ChangePassword(changePassword: ChangePasswordModel) {
		let serviceUrl = "User/ChangePassword";
		return super.PostData(serviceUrl,changePassword);
	}

    UpdateProfile(userModel: UserModel) {
		let serviceUrl = "User/UpdateUser";
		return super.PostData(serviceUrl,userModel);
	}

    AddUser(userModel: UserModel) {
		let serviceUrl = "User/AddUser";
		return super.PostData(serviceUrl,userModel);
	}

    loginUser(userName: string, password: string): Observable<UserModel[]> {

        let data = new URLSearchParams();
        data.append('UserName', userName);
        data.append('Password', password);

        var auth = btoa(userName + ":" + password);

        // return this._http.post(this._userLoginURL, data.toString(), 
        //     { headers: this.getAuthenticationHeader(auth) })
        //     .map(this.extractData)
        //     .catch(this.handleError);
        let headers = this.getAuthenticationHeader(auth);
        let url = this._userLoginURL + "?userName=" + userName + "&password=" + password;
        console.log(url);

        return this._http.post(url,//this._userLoginURL,// data.toString(),
            { headers: headers })
            .map(this.extractData)
            .catch(this.handleError);

    }


    getAuthenticationHeader(auth: string): Headers {
        let headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        headers.append('Authorization', 'Basic ' + auth);

        return headers;
    }

    // This method parses the data to JSON
    private extractData(res: Response) {
        let body;
        if (res["_body"] != "")
        { body = res.json(); }

        if (body != undefined && body != null) {
            if ((res.headers.toJSON()).token === undefined) {
                localStorage.setItem("accessToken", (res.headers.toJSON()).Token[0]);
            }
            else {
                localStorage.setItem("accessToken", (res.headers.toJSON()).token[0]);
            }

            // localStorage.setItem("accessToken", (res.headers.toJSON()).token[0]);
            localStorage.setItem("userName", body.FullName);
        }
        else {
            return Observable.throw("Invalid credentials");
        }
        return body || {};
    }

    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }

}
