import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { CountryService } from '../../../../Services/country.service'
import { CountryModel } from '../../../../Models/country.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})

export class CountryComponent extends AppComponent implements OnInit {

  constructor(private _countryService: CountryService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  CountryList = new Array<CountryModel>();
  Country = new CountryModel();

  ngOnInit() {
    this.form = this.formBuilder.group({
      CountryName: [null, [Validators.required]],
      CountryCode: [null, [Validators.required]],
    });
    this.getList();
  }

  Edit(id: any) {
    if (this.CountryList.filter(x=>x.CountryId == id).length == 1)
    {
      this.Country = this.CountryList.filter(x=>x.CountryId == id)[0];
    }
  }

  Cancel()
  {
    this.form.reset();
    this.Country = new CountryModel();
  }

  GetResponse(response: any,opration: string) {
    this.Cancel();
    this.getList();
    this.commonFunctions.LoaddingHide();
    if(opration == "Add")
    { this.commonFunctions.DisplayAddSuccessMsg("Country"); }
    else if(opration == "Update")
    { this.commonFunctions.DisplayAddSuccessMsg("Country"); }
  }

  Save()
  {
    this.validateAllFormFields(this.form);
    if (!this.form.valid) {
        this.commonFunctions.DisplayRequiredFieldErrorMsg();
        return;
    }
    console.log(this.Country);
    this.commonFunctions.LoaddingShow();
    if (this.Country.CountryId == undefined || this.Country.CountryId == null)
    {
      this._countryService.Add(this.Country)
      .subscribe(
      response => this.GetResponse(response,"Add"),
      error => this.commonFunctions.ManageError(error)
      );
    }
    else
    {
      this._countryService.Update(this.Country)
      .subscribe(
      response => this.GetResponse(response,"Update"),
      error => this.commonFunctions.ManageError(error)
      );
    }

  }

  Delete(id: any) {
  this._countryService.Remove(id)
      .subscribe(
      response => {
        this.getList();
        this.commonFunctions.DisplayDeleteSuccessMsg("Country");
      },
      error => this.commonFunctions.ManageError(error)
      );
  }

  getList() {
    debugger;
    this._countryService.FindAll()
      .subscribe(
      response => {this.CountryList = response; this.dataTableBinding(this.CountryList);},
      error => this.commonFunctions.ManageError(error)
      );
  }

  dataTableBinding(list: CountryModel[]) {
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
          { "data": "CountryName" },
          { "data": "CountryCode" },
          {
            "data": "CountryId",
            render:
            function (data, type, row) {
              var strButtons = "<span class=\"standard-margin-right\">" +
                "<label title=\"Edit\" class=\"link-label\"> <i id=\"Edit\" accessKey=\"" + data + "\" class=\"fa fa-edit fa-pencil-square-o fa-lg\" aria-hidden=\"true\"></i></label>" +
                "</span>";

              //if (row["IsActive"] == true) {
                strButtons = strButtons + "<span class=\"standard-margin-right\">" +
                  "<label title=\"Delete\" class=\"link-label\"> <i id=\"Delete\" accessKey=\"" + data + "\" class=\"fa fa-delete fa-trash fa-lg\" aria-hidden=\"true\"></i></label>" +
                  "</span>";
              // } else {
              //   strButtons = strButtons + "<span class=\"standard-margin-right\">" +
              //     "<label title=\"Undo Delete\" class=\"link-label\"> <i id=\"UndoDelete\" accessKey=\"" + data + "\" class=\"fa fa-edit fa-undo fa-lg\" aria-hidden=\"true\"></i></label>" +
              //     "</span>";
              // }
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
