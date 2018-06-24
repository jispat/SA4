import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../../Services/user.service'
import { UserModel } from '../../../../Models/user.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';
import { PasswordValidation } from '../../../../helpers/passwordvalidation';

@Component({
  selector: 'app-manageuser',
  templateUrl: './manageuser.component.html',
  styleUrls: ['./manageuser.component.css']
})
export class ManageuserComponent extends AppComponent implements OnInit {

  userModel: UserModel = new UserModel();
  constructor(private _userService: UserService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  ngOnInit() {
    debugger;
    this.form = this.formBuilder.group({
      UserName: [null, [Validators.required]],
      FullName: [null, [Validators.required]],
      Mobile: [null, [Validators.required]],
      Email: [null, [Validators.required]],
      Password: [null, [Validators.required]],
      ConfirmPassword: [null, [Validators.required]],
    },{ validator: PasswordValidation.MatchPassword }
    );
    this.activatedRouter.params.subscribe(params => {
       this.userModel.UserId = params['id'];
       if (this.userModel.UserId !== undefined && this.userModel.UserId.length > 0)
       this.loadUserProfile();
    });    
  }

  loadUserProfile()
  {
    this._userService.FindByID(this.userModel.UserId)
      .subscribe(
      response => { this.userModel = response; 
            this.userModel.ConfirmPassword = this.userModel.Password = "NotUpdate";
        },
      error => {
        this.Cancel();
        this.commonFunctions.DisplayWarningMsg("User not found.");     
      }
      );    
  }

  Cancel()
  {
    this._router.navigate(["userlist"]);
  }

  SaveDone(response: any,opration: string) {
    this.commonFunctions.LoaddingHide();
    this.Cancel();
    this.commonFunctions.DisplaySuccessMsg("User has been " + opration + " completed.");
  }

  Save()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid) {
        this.commonFunctions.DisplayRequiredFieldErrorMsg();
        return;
    }
    this.commonFunctions.LoaddingShow();
    console.log(this.userModel);
    if (this.userModel.UserId === undefined || this.userModel.UserId.length == 0)
    {
    this._userService.AddUser(this.userModel)
      .subscribe(
      response => this.SaveDone(response,"Add"),
      error => this.commonFunctions.ManageError(error)
      );
    }
    else
    {
    this._userService.UpdateProfile(this.userModel)
      .subscribe(
      response => this.SaveDone(response,"Update"),
      error => this.commonFunctions.ManageError(error)
      );
    }

  }


}
