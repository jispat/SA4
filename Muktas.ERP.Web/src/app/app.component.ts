import { Component } from '@angular/core';
import { CommonFunctions } from './Helpers/common-functions.helper';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';
import * as AppConfig from './Configurations/app.configuration';
declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
    
    form: FormGroup;
    
    constructor()
    {
        this.CheckGuestUrls();
    }
  
    IsLogin: boolean = false;
    UserName: string = "";
    title = 'app';
    public commonFunctions = new CommonFunctions();

    CheckGuestUrls()
    {
        this.SetLoginInfo();
        if (window.location.href.toLowerCase() !== AppConfig.BASE_URI_APP)
        {
            if (window.location.href.toLowerCase().indexOf("/login") > 0
            || window.location.href.toLowerCase().indexOf("/forgotpassword") > 0
            || window.location.href.toLowerCase().indexOf("/resetpassword") > 0 )
            {
                this.Logout();
                this.SetLoginInfo();
            }
            else if (!this.IsLogin) 
            {
                console.log(10000);
                this.commonFunctions.RedirectURL( "login");
            }
        }
    }

    SetLoginInfo()
    {
        this.IsLogin = localStorage.getItem("UserId") == null ? false : true;
        this.UserName = localStorage.getItem("UserName");
    }

    LogoutWithLogin()
    {
        this.Logout();     
        console.log(10001);
        this.commonFunctions.RedirectURL("login");
    }
    Logout()
    {
        localStorage.clear();
        this.IsLogin = false;
        this.UserName = "";
    }

    displayFieldCss(field: string,formGroup = this.form) {
        return {
            'has-error': this.isFieldValid(field,formGroup),
            'has-feedback': this.isFieldValid(field,formGroup)
        };
    }

    isFieldValid(field: string,formGroup = this.form ) {
        //return !this.form.get(field).valid && this.form.get(field).touched;
        return !formGroup.get(field).valid && formGroup.get(field).touched;
    }

    validateAllFormFields(formGroup = this.form) {
        Object.keys(formGroup.controls).forEach(field => {
            console.log(field);
            const control = formGroup.get(field);
            if (control instanceof FormControl) {
                control.markAsTouched({ onlySelf: true });
            } else if (control instanceof FormGroup) {
                this.validateAllFormFields(control);
            }
        });
    }

    IsNullOrEmpty(str: string)
    {
        if (str == null || str == undefined || str == "")
            return true;
        else
            return false;
    }
    newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
            return v.toString(16);
        });
    }

    emptyGuid()
    {
        return "00000000-0000-0000-0000-000000000000";
    }

    GetDatepicker(elementId: string) {
      let me = this;
      $(document).ready(function () {

      $("#" + elementId).datepicker({
        dateFormat: "dd-mm-yy",
        onClose: function () {
            debugger;
            var datePart = $(this).val().split("-");
            var selectedDate = new Date(datePart[2], datePart[1] - 1, datePart[0]);
            if (moment(selectedDate).isValid()) {
                var v = datePart[0] + '-' + datePart[1] + '-' + datePart[2];
                me.form.get(elementId).reset(v);
                me.form.get(elementId).patchValue(v);
            } 
            else{
                me.form.get(elementId).setValue(undefined);
            }        
        }

      });

    });
  }


}
