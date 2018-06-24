import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../../Services/user.service'
import { ChangePasswordModel } from '../../../../Models/changepassword.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';
import { PasswordValidation } from '../../../../helpers/passwordvalidation';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent extends AppComponent implements OnInit {

  
  changePasswordModel: ChangePasswordModel = new ChangePasswordModel();
    constructor(private _userService: UserService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  ngOnInit() {
    debugger;
    this.form = this.formBuilder.group({
      OldPassword: [null, [Validators.required]],
      NewPassword: [null, [Validators.required]],
      ConfirmPassword: [null, [Validators.required]],
    },{ validator: PasswordValidation.MatchPasswordResetPassword }
    );
    this.changePasswordModel.UserId = localStorage.getItem("UserId");
 }

  Cancel()
  {
    this._router.navigate(["updateprofile"]);
  }

  ResetDone(response: any,opration: string) {
    this.commonFunctions.LoaddingHide();
    this._router.navigate(['updateprofile']);
    this.commonFunctions.DisplaySuccessMsg("Your password has been changed.");
  }

  ChangePassword()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid) {
        this.commonFunctions.DisplayRequiredFieldErrorMsg();
        return;
    }
    console.log(this.changePasswordModel);
    this.commonFunctions.LoaddingShow();
    this._userService.ChangePassword(this.changePasswordModel)
      .subscribe(
      response => this.ResetDone(response,"send"),
      error => this.commonFunctions.ManageError(error)
      );

  }

}
