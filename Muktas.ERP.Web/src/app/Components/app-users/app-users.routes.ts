import { Route, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChangepasswordComponent } from './user/changepassword/changepassword.component';
import { ManageuserComponent } from './user/manageuser/manageuser.component';
import { UpdateprofileComponent } from './user/updateprofile/updateprofile.component';
import { UserlistComponent } from './user/userlist/userlist.component';

import { CountryComponent } from './master/country/country.component';
import { StateComponent } from './master/state/state.component';
import { CityComponent } from './master/city/city.component';


export const AppUsersRoutes: Route[] = [
    //{
        { path: 'dashboard', component: DashboardComponent },
        { path: 'changepassword', component: ChangepasswordComponent },
        { path: 'manageuser', component: ManageuserComponent },
        { path: 'manageuser/:id', component: ManageuserComponent },
        { path: 'updateprofile', component: UpdateprofileComponent },
        { path: 'userlist', component: UserlistComponent }

        ,{ path: 'country', component: CountryComponent }
        ,{ path: 'state', component: StateComponent }
        ,{ path: 'city', component: CityComponent }


        // path: '', component: DashboardComponent,
        // children: [
        //     { path: 'dashboard', component: DashboardComponent },
        //     { path: 'changepassword', component: ChangepasswordComponent },
        //     { path: 'manageuser', component: ManageuserComponent },
        //     { path: 'updateprofile', component: UpdateprofileComponent },
        //     { path: 'userlist', component: UserlistComponent },
        // ]
    //}

];

