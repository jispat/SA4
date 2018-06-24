import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutes } from './app.router';
import { UrlSerializer } from '@angular/router';
import {DatePipe} from '@angular/common';
import {LocationStrategy, HashLocationStrategy} from '@angular/common';

import { AppComponent } from './app.component';

//import { DateString } from './helpers/datestring.pipe';
import { GlobalErrorHandler } from './Helpers/globalErrorHandler.helper';
import { LowerCaseUrlSerializer } from './Helpers/lowercaseurlserializer.helper';
import { AuthHelper } from './Helpers/auth.helper';

import { LoginComponent } from './Components/anonymous-users/login/login.component';
import { AppUsersModule } from './Components/app-users/app-users.module';
import { PageNotFoundComponent } from './Components/shared/page-not-found/page-not-found.component';
import { HeaderComponent } from './Components/shared/header/header.component';
import { FooterComponent } from './Components/shared/footer/footer.component';
import { ForgotpasswordComponent } from './Components/anonymous-users/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './Components/anonymous-users/resetpassword/resetpassword.component';
import { HomeComponent } from './Components/anonymous-users/home/home.component';
import { FieldErrorDisplayComponent } from './Components/shared/field-error-display/field-error-display.component';

// import { BaseService } from './Services/base.service';
import { LogService } from './Services/log.service';
import { TokenService } from './Services/token.service';
import { UserService } from './Services/user.service';
import { CountryService } from './Services/country.service';
import { StateService } from './Services/state.service';
import { CityService } from './Services/city.service';

//import { DatePickerModule } from '../../node_modules/angular-io-datepicker/src/datepicker/index';


@NgModule({
  declarations: [
    //EqualValidator,
    AppComponent,
    LoginComponent,
    PageNotFoundComponent,
    HeaderComponent,
    FooterComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    HomeComponent,
    FieldErrorDisplayComponent,
//    DateString
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    AppRoutes,
    AppUsersModule,
  //  DatePickerModule
  ],
  // exports:[
  //   FieldErrorDisplayComponent,

  //   DashboardComponent,
  // ],
  providers: [
    {
      provide: ErrorHandler, 
      useClass: GlobalErrorHandler
    },
    {
      provide: UrlSerializer,
      useClass: LowerCaseUrlSerializer
    },
    {provide: LocationStrategy, useClass: HashLocationStrategy},
    // BaseService, 
    DatePipe,
    AuthHelper,
    LogService,
    TokenService,
    UserService,
    StateService,
    CityService,
    CountryService    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
