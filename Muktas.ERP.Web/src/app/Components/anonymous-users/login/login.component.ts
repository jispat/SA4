import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../Services/user.service'
import { UserModel } from '../../../Models/user.model';
import { AppComponent } from '../../../app.component';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent extends AppComponent implements OnInit {

    userModel: UserModel = new UserModel();

    constructor(private _userService: UserService,
        private _router: Router
        ,private formBuilder: FormBuilder) 
    { 
      super();
      this.userModel.Password = this.userModel.UserName = "Admin";
    }

    ngOnInit() {
      this.form = this.formBuilder.group({
          UserName: [null, [Validators.required]],
          Password: [null, [Validators.required]],
      });      
    }

    loginUser() {
        this.validateAllFormFields(this.form);
        if (!this.form.valid) {
            this.commonFunctions.DisplayRequiredFieldErrorMsg();
            return;
        }
        this.commonFunctions.LoaddingShow();
        debugger;
        console.log(this.userModel.UserName);        
        this._userService.loginUser(this.userModel.UserName, this.userModel.Password)
            .subscribe(
            data => {
                debugger;
                if (data["error"] != null)
                {
                    this.commonFunctions.LoaddingHide();
                    this.commonFunctions.DisplayErrorMsg("Invalid UserName or Password.");
                    console.log(data);    
                }
                else{
                console.log(data);
                localStorage.setItem("UserId",data["UserId"]);
                localStorage.setItem("UserName",data["UserName"]);
                console.log("Login Success");
                this.commonFunctions.LoaddingHide();
                console.log(10003);
                this.commonFunctions.RedirectURL("dashboard");
                //this._router.navigate(["dashboard"]);
                }
            },
            error => {
                debugger;
                this.commonFunctions.LoaddingHide();
                this.commonFunctions.DisplayErrorMsg("Invalid UserName or Password.");
                console.log("Login Failed");
            });
    }

    extractData(user: UserModel) {
        this.userModel = user;
    }
}
