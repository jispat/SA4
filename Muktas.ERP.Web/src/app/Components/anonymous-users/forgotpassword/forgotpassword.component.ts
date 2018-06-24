import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../Services/user.service'
import { UserModel } from '../../../Models/user.model';
import { FormGroup,FormBuilder,Validators,FormControl} from '@angular/forms';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent extends AppComponent implements OnInit {

  userModel: UserModel;

  constructor(private _userService: UserService,
        private _router: Router
        ,private formBuilder: FormBuilder) 
  { super() }

  ngOnInit() {
    this.userModel = new UserModel();
    this.form = this.formBuilder.group({
      Email: [null, [Validators.required]],
    });

  }

  Cancel()
  {
    this._router.navigate(["login"]);
  }
  
  SendEmail()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid){ 
      this.commonFunctions.DisplayRequiredFieldErrorMsg();
      return;
    }
    
    this.commonFunctions.LoaddingShow();
    this._userService.ForgotPassword(this.userModel.Email)
      .subscribe(
      response => this.ResSendEmail(response,"send"),
      error => this.commonFunctions.ManageError(error)
    );
  }

  ResSendEmail(response: any,opration: string) {
    this.commonFunctions.LoaddingHide();
    this.Cancel();
    this.commonFunctions.DisplaySuccessMsg("Please check your email.");  
  }

}
