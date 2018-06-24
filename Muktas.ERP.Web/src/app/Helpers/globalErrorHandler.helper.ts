import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { LogService } from '../Services/log.service';
import { LogModel } from '../Models/log.model';
import { CommonFunctions } from '../Helpers/common-functions.helper';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';

// import * as StackTrace from 'stacktrace-js';
//https://medium.com/@amcdnl/global-error-handling-with-angular2-6b992bdfb59c

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
commonFunctions = new CommonFunctions();
constructor(//private injector: Injector,
private _logService: LogService
) { }

    handleError(error) { 
        if (error != null)
        {
            debugger;
            let log = new LogModel();
            log.MessageType = "Web";
            log.Message = error.message;
            log.Description = error.stack;
            console.log( "Error - " + JSON.stringify(log));    
            this._logService.Add(log)
            .subscribe(
                response => console.log("Error has been logged in database."),
                error => console.log("Error not able to logged in database - " + error.message)
            );
            this.commonFunctions.DisplayErrorMsg(error.message);
            throw null;
        }
    }  
}
