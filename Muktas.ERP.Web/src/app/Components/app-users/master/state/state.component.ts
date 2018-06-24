import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { StateService } from '../../../../Services/state.service'
import { StateModel } from '../../../../Models/state.model';
import { CountryService } from '../../../../Services/country.service'
import { CountryModel } from '../../../../Models/country.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.css']
})
export class StateComponent extends AppComponent implements OnInit {

  constructor(private _stateService: StateService,
        private _countryService: CountryService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  countryList = new Array<CountryModel>();
  stateList = new Array<StateModel>();
  stateModel = new StateModel();

  ngOnInit() {
    this.form = this.formBuilder.group({
      StateName: [null, [Validators.required]],
      StateCode: [null, [Validators.required]],
      CountryId: [null, [Validators.required]], //Validators.min(36)]],
      StateVatTinNo: [null, [Validators.required,Validators.max(999),Validators.min(1)]],
    }
    );
    this.getList();
    this.FillList();
    this.stateModel.CountryId = "";
  }

  FillList()
  {
    this._countryService.FindAll()
      .subscribe(
      response => {this.countryList = response;},
      error => this.commonFunctions.ManageError(error)
      );    
  }

  Edit(id: any) {
    if (this.stateList.filter(x=>x.StateId == id).length == 1)
    {
      this.stateModel = this.stateList.filter(x=>x.StateId == id)[0];
    }
  }

  Cancel()
  {
    this.form.reset();
    this.form.get("CountryId").setValue("");
    this.stateModel = new StateModel();
    this.stateModel.CountryId = "";
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
    console.log(this.stateModel);
    this.commonFunctions.LoaddingShow();
    if (this.stateModel.StateId == undefined || this.stateModel.StateId == null)
    {
      this._stateService.Add(this.stateModel)
      .subscribe(
      response => this.GetResponse(response,"Add"),
      error => this.commonFunctions.ManageError(error)
      );
    }
    else
    {
      this._stateService.Update(this.stateModel)
      .subscribe(
      response => this.GetResponse(response,"Update"),
      error => this.commonFunctions.ManageError(error)
      );
    }

  }

  Delete(id: any) {
  this._stateService.Remove(id)
      .subscribe(
      response => {
        this.getList();
        this.commonFunctions.DisplayDeleteSuccessMsg("State");
      },
      error => this.commonFunctions.ManageError(error)
      );
  }

  getList() {
    debugger;
    this._stateService.FindAll()
      .subscribe(
      response => {this.stateList = response; this.dataTableBinding(this.stateList);},
      error => this.commonFunctions.ManageError(error)
      );
  }

  dataTableBinding(list: StateModel[]) {
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
          { "data": "StateName" },
          { "data": "StateCode" },
          { "data": "Country.CountryName" },
          { "data": "StateVatTinNo" },
          {
            "data": "StateId",
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

