import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { LogService } from '../Services/log.service';
import { LogModel } from '../Models/log.model';
//import { Redirect } from '../Helpers/redirect.helper';
import * as AppConfig from '../Configurations/app.configuration';
import { Router, ActivatedRoute } from '@angular/router';
import 'rxjs/Rx';

declare var $: any;
declare var moment: any;

@Injectable()
export class CommonFunctions {

    constructor() { }

    DisplayAlert(type, Message) {
        $("#divAlert").show();
        var alerthtml = "";
        $("#divAlert").removeClass("alert-success");
        $("#divAlert").removeClass("alert-info");
        $("#divAlert").removeClass("alert-danger");
        $("#divAlert").removeClass("alert-warning");
        if (type == "warning") {
            $("#divAlert").addClass("alert-warning");
            alerthtml = "Warning!";
        }
        else if (type == "success") {
            $("#divAlert").addClass("alert-success");
            alerthtml = "Success!";
        }
        else if (type == "info") {
            $("#divAlert").addClass("alert-info");
            alerthtml = "Info!";
        }
        else if (type == "danger") {
            $("#divAlert").addClass("alert-danger");
            alerthtml = "Error!";
        }

        alerthtml = "<span>" + alerthtml + "</span><br/>";
        $("#divAlert p").html("");
        $("#divAlert p").html(alerthtml + Message);

        setTimeout(function () { $("#divAlert").hide(); }, 5000);
    }
    DisplayWarningMsg(Message) {
        this.DisplayAlert("warning", Message)
    }
    DisplaySuccessMsg(Message) {
        this.DisplayAlert("success", Message)
    }
    DisplayInfoMsg(Message) {
        this.DisplayAlert("info", Message)
    }
    DisplayErrorMsg(Message) {
        this.DisplayAlert("danger", Message)
    }

    DisplayAddSuccessMsg(ModuleName) {
        this.DisplaySuccessMsg(ModuleName + " added successfully.")
    }
    DisplayUpdateSuccessMsg(ModuleName) {
        this.DisplaySuccessMsg(ModuleName + " updated successfully.")
    }
    DisplayDeleteSuccessMsg(ModuleName) {
        this.DisplaySuccessMsg(ModuleName + " make deleted/undo successfully.")
    }
    DisplayRequiredFieldErrorMsg() {
        this.DisplayErrorMsg("Please input all the required fields value.")
    }
    // DisplayAlert("warning","this is test message this is test message this is test message this is test message this is test message this is test message ");
    // DisplayAlert("success","this is test message this is test message this is test message this is test message this is test message this is test message ");
    //DisplayAlert("info","this is test message this is test message this is test message this is test message this is test message this is test message ");
    //DisplayAlert("danger","this is test message this is test message this is test message this is test message this is test message this is test message ");

    ManageError(error:any)
    {
        this.LoaddingHide();
        debugger;
        let errormsg = "";
        if (error["status"] !== undefined)
        {
            if (error["_body"] !== undefined && error["_body"].length > 0 ) 
            {
                errormsg = error["_body"];
                if (errormsg.startsWith("\""))
                {errormsg = errormsg.substring(1,errormsg.length-1);}
            }
            else
                {errormsg = error["statusText"]; }
        }
        else 
        { errormsg = error; }
        this.DisplayErrorMsg(errormsg);
    }

  public LoaddingShow()
  {
    $(".loader").show();
  }

  public LoaddingHide()
  {
    $(".loader").hide();
  }

  RedirectURL(routename: string)
  {
      console.log(100);
      //Redirect.route.navigate([routename]);
      //alert(AppConfig.BASE_URI_APP);
    location.href = AppConfig.BASE_URI_APP + routename;
    location.reload(true);

      //router.navigate([routename]);
  }

    GetFormatedDate(date: string) {
        
        let newDate = new Date(date);
        return newDate.getDate() + "-" + (newDate.getMonth() + 1) + "-" + newDate.getFullYear();

    }

    GetFormatedDateTime(datetime: string) {
        
        let newDateTime = new Date(datetime);
        return newDateTime.getDate() + "-" + (newDateTime.getMonth() + 1) + "-" + newDateTime.getFullYear()
            + " " + newDateTime.getHours() + ":" + newDateTime.getMinutes();

    }

  SetDate(controlid: string, value: Date)
  {
      if (value === undefined || value === null)
      {
          $("#"+ controlid).val(undefined);
      }
      else{
          debugger; 
          var datePart = value.toString().split("-");
          var v = datePart[0] + '-' + datePart[1] + '-' + datePart[2].substr(0,2);
          $("#"+ controlid).val(v);
      }
  }




}
