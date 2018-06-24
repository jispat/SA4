import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from './base.service';

@Injectable()
export class CityService extends BaseService {
	constructor(http: Http) {
		super('City', http);
	}

	FindByState(StateId: string) {
		let serviceUrl = "City/FindByState";
		return super.GetDataByField(serviceUrl,"StateId",StateId);
	}


}
