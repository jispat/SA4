import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../../app.component';
// import 'rxjs/Rx';

 declare var $: any;
 declare var moment: any;

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent extends AppComponent implements OnInit {

  constructor() { super() }
  
  CurrentYear: string;

  ngOnInit() {
    this.CurrentYear = (new Date(Date.now())).getFullYear().toString();
  }

}
