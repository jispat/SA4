import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../Services/user.service'
import { ResetPasswordModel } from '../../../Models/resetpassword.model';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl} from '@angular/forms';
import { PasswordValidation } from '../../../helpers/passwordvalidation';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent extends AppComponent implements OnInit {

  resetPasswordModel: ResetPasswordModel = new ResetPasswordModel();

    constructor(private _userService: UserService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  ngOnInit() {
    localStorage.clear();
    this.resetPasswordModel.ConfirmPassword = "";
    this.resetPasswordModel.NewPassword = "";
    this.form = this.formBuilder.group({
        NewPassword: [null, [Validators.required]],
        ConfirmPassword: [null, [Validators.required]],
    },{ validator: PasswordValidation.MatchPasswordResetPassword });
    this.activatedRouter.params.subscribe(params => {
       this.resetPasswordModel.Code = params['code'];
    });
    this.CheckCode();
 }

  CheckCode()
  {
    debugger;
     this._userService.CheckResetPasswordCode(this.resetPasswordModel.Code)
      .subscribe(
      response => {console.log(response);},
      error => {this.commonFunctions.ManageError(error);
        this.Cancel();
        }
      );
  }

  Cancel()
  {
    this._router.navigate(["login"]);
  }

  ResetPassword()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid) 
    {      
        this.commonFunctions.DisplayRequiredFieldErrorMsg();
        return;
    }
    if (this.resetPasswordModel.NewPassword != this.resetPasswordModel.ConfirmPassword)
    {      
        this.commonFunctions.DisplayErrorMsg("Confirm password must be match with new password.");
        return;
    }
    this.commonFunctions.LoaddingShow();
    this._userService.ResetPassword(this.resetPasswordModel)
      .subscribe(
      response => this.ResetDone(response,"send"),
      error => this.commonFunctions.ManageError(error)
      );
  }

  ResetDone(response: any,opration: string) {
    this.commonFunctions.LoaddingHide();
    this._router.navigate(["login"]);
    this.commonFunctions.DisplaySuccessMsg("Your password has been changed, please login.");
  }

}
