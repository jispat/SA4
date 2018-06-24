import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
import { StateService } from '../../../../Services/state.service'
import { StateModel } from '../../../../Models/state.model';
import { CountryService } from '../../../../Services/country.service'
import { CountryModel } from '../../../../Models/country.model';
import { CityService } from '../../../../Services/city.service'
import { CityModel } from '../../../../Models/city.model';
import { FormGroup, FormBuilder, Validators, FormControl} from '@angular/forms';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent extends AppComponent implements OnInit {

  constructor(private _stateService: StateService,
        private _cityService: CityService,
        private _countryService: CountryService,
        private _router: Router,
        private activatedRouter: ActivatedRoute
        ,private formBuilder: FormBuilder) 
  { super() }

  countryList = new Array<CountryModel>();
  stateList = new Array<StateModel>();
  allstateList = new Array<StateModel>();
  cityList = new Array<CityModel>();
  cityModel = new CityModel();

  ngOnInit() {
    this.form = this.formBuilder.group({
      CityName: [null, [Validators.required]],
      Pincode: [null, [Validators.required]],
      StateId: [null,[Validators.required]],// [Validators.required, Validators.min(36)]],
      CountryId: [null,[Validators.required]],// [Validators.required, Validators.min(36)]],
    }
    );
    this.getList();
    this.FillList();
    this.cityModel.StateId = "";
    this.cityModel.State = new StateModel();
    this.cityModel.State.CountryId = "";
  }

  FillList()
  {
    this._countryService.FindAll()
      .subscribe(
      response => {this.countryList = response;console.log(this.countryList);},
      error => this.commonFunctions.ManageError(error)
      );    
    this._stateService.FindAll()
      .subscribe(
      response => {this.allstateList = response;console.log(this.allstateList);},
      error => this.commonFunctions.ManageError(error)
      );    
  }

  RefillState()
  {
      if(this.cityModel.State.CountryId !== undefined)
      {
        this.stateList = this.allstateList.filter(x=>x.CountryId == this.cityModel.State.CountryId);
      }
  }

  Edit(id: any) {
    if (this.cityList.filter(x=>x.CityId == id).length == 1)
    {
      this.cityModel = this.cityList.filter(x=>x.CityId == id)[0];
      this.RefillState();
      console.log(this.cityModel);
    }
  }

  Cancel()
  {
    this.form.reset();
    this.form.get("StateId").setValue("");
    this.form.get("CountryId").setValue("");
    this.cityModel = new CityModel();
    this.cityModel.StateId = "";
    this.cityModel.State = new StateModel();
    this.cityModel.State.CountryId = "";
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
    console.log(this.cityModel);
    this.commonFunctions.LoaddingShow();
    if (this.cityModel.CityId == undefined || this.cityModel.CityId == null)
    {
      this._cityService.Add(this.cityModel)
      .subscribe(
      response => this.GetResponse(response,"Add"),
      error => this.commonFunctions.ManageError(error)
      );
    }
    else
    {
      this._cityService.Update(this.cityModel)
      .subscribe(
      response => this.GetResponse(response,"Update"),
      error => this.commonFunctions.ManageError(error)
      );
    }

  }

  Delete(id: any) {
  this._cityService.Remove(id)
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
    this._cityService.FindAll()
      .subscribe(
      response => {this.cityList = response; this.dataTableBinding(this.cityList);},
      error => this.commonFunctions.ManageError(error)
      );
  }

  dataTableBinding(list: CityModel[]) {
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
          { "data": "CityName" },
          { "data": "Pincode" },
          { "data": "State.StateName" },
          { "data": "State.Country.CountryName" },
          {
            "data": "CityId",
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


