import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../../Services/user.service'
import { UserModel } from '../../../../Models/user.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';

@Component({
  selector: 'app-updateprofile',
  templateUrl: './updateprofile.component.html',
  styleUrls: ['./updateprofile.component.css']
})
export class UpdateprofileComponent extends AppComponent implements OnInit {

  
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
      Email: [null, [Validators.required]],
      Mobile: [null, [Validators.required]],
    });

    this.userModel.UserId = localStorage.getItem("UserId");
    this.loadUserProfile();
  }

  loadUserProfile()
  {
    this._userService.FindByID(this.userModel.UserId)
      .subscribe(
      response => { this.userModel = response; },
      error => {this.Cancel(); this.commonFunctions.ManageError(error);}
      );    
  }

  Cancel()
  {
    this._router.navigate(["dashboard"]);
  }
  
  SaveDone(response: any,opration: string) {
    this.commonFunctions.LoaddingHide();
    localStorage.setItem("UserName",this.userModel.UserName);
    this.commonFunctions.DisplaySuccessMsg("Your profile has been updated.");
  }

  Save()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid) {
        this.commonFunctions.DisplayRequiredFieldErrorMsg();
        return;
    }
    this.commonFunctions.LoaddingShow();
    this.userModel.Password = "NotUpdate";
    console.log(this.userModel);
    this._userService.UpdateProfile(this.userModel)
      .subscribe(
      response => this.SaveDone(response,"send"),
      error => this.commonFunctions.ManageError(error)
      );

  }

}
