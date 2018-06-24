import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from './base.service';

@Injectable() 
export class CountryService extends BaseService {
	constructor(http: Http) {
		super('Country', http);
	}

}
