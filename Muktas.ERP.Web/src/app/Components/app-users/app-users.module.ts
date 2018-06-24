import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
//import { FieldErrorDisplayComponent } from '../shared/field-error-display/field-error-display.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UpdateprofileComponent } from './user/updateprofile/updateprofile.component';
import { ChangepasswordComponent } from './user/changepassword/changepassword.component';
import { ManageuserComponent } from './user/manageuser/manageuser.component';
import { UserlistComponent } from './user/userlist/userlist.component';
import { FieldErrorDisplayUserComponent } from './field-error-display-user/field-error-display-user.component';
import { StateComponent } from './master/state/state.component';
import { CityComponent } from './master/city/city.component';
import { CountryComponent } from './master/country/country.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    declarations: [
    //    FieldErrorDisplayComponent,
        DashboardComponent,
        UpdateprofileComponent,
        ChangepasswordComponent,
        ManageuserComponent,
        UserlistComponent,
        FieldErrorDisplayUserComponent,
        StateComponent,
        CityComponent,
        CountryComponent],
    exports: [
        DashboardComponent, 
    ]
})

export class AppUsersModule { }
