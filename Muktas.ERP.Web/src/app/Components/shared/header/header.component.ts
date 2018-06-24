import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../app.component';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends AppComponent implements OnInit {

  constructor(private _router: Router) { super() }

  ngOnInit() {
  }

  // IsLogin: boolean = localStorage.getItem("UserId") == null ? false : true;
  // UserName: string = localStorage.getItem("UserName");

  // Logout()
  // {
  //   localStorage.clear();
  //   window.location.href = "login" ;
  // }

}
