import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../../Services/user.service'
import { UserModel } from '../../../../Models/user.model';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent extends AppComponent implements OnInit {

    constructor(private _userService: UserService,
        private _router: Router,
        private activatedRouter: ActivatedRoute) 
  { super() }

  UserList = new Array<UserModel>();

  ngOnInit() {
    this.getList();
  }

  Edit(id: any) {
    this._router.navigate(["manageuser/" + id]);
  }

  Delete(id: any) {
  this._userService.Remove(id)
      .subscribe(
      response => {
        this.getList();
        this.commonFunctions.DisplayDeleteSuccessMsg("User");
      },
      error => this.commonFunctions.ManageError(error)
      );
  }

  getList() {
    this._userService.FindAll()
      .subscribe(
      response => {this.UserList = response; this.dataTableBinding(this.UserList);},
      error => this.commonFunctions.ManageError(error)
      );
  }

  dataTableBinding(list: UserModel[]) {
    console.log(list);
    var me = this;
    $(document).ready(function () {
      $('#datatable-list').DataTable({
        data: list,
        responsive: true,
        //retrieve: true,
        destroy: true,
        columns:
        [
          { "data": "UserName" },
          { "data": "FullName" },
          { "data": "Email" },
          { "data": "Mobile" },
          {
            "data": "CreatedOn",
            render:
            function (data, type, row) {
              return me.commonFunctions.GetFormatedDate(data);
            }
          },
          {
            "data": "UpdatedOn",
            render:
            function (data, type, row) {
              return me.commonFunctions.GetFormatedDate(data);
            }
          },
          {
            "data": "UserId",
            render:
            function (data, type, row) {
              var strButtons = "<span class=\"standard-margin-right\">" +
                "<label title=\"Edit\" class=\"link-label\"> <i id=\"Edit\" accessKey=\"" + data + "\" class=\"fa fa-edit fa-pencil-square-o fa-lg\" aria-hidden=\"true\"></i></label>" +
                "</span>";

              if (row["IsActive"] == true) {
                strButtons = strButtons + "<span class=\"standard-margin-right\">" +
                  "<label title=\"Delete\" class=\"link-label\"> <i id=\"Delete\" accessKey=\"" + data + "\" class=\"fa fa-delete fa-trash fa-lg\" aria-hidden=\"true\"></i></label>" +
                  "</span>";
              } else {
                strButtons = strButtons + "<span class=\"standard-margin-right\">" +
                  "<label title=\"Undo Delete\" class=\"link-label\"> <i id=\"UndoDelete\" accessKey=\"" + data + "\" class=\"fa fa-edit fa-undo fa-lg\" aria-hidden=\"true\"></i></label>" +
                  "</span>";
              }
              return strButtons;
            }
          }

        ]
      });
    });
    $("#datatable-list").unbind('click');
    $('#datatable-list').on('click', '.link-label', function (e) {
      debugger;
      console.log('event - ' + e.target.accessKey);
      if (e.target.id == "Edit") {
        me.Edit(e.target.accessKey);
      }
      if (e.target.id == "Delete") {
        me.Delete(e.target.accessKey);
      }
      if (e.target.id == "UndoDelete") {
        me.Delete(e.target.accessKey);
      }
      //e.preventDefault();
    });
  }

}
