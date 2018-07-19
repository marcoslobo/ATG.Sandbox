
import { Injectable, Inject } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { Order } from '../model/order.model';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class OrderService {
    myAppUrl: string = "";
  
    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }
    
    getOrders() {
        return this._http.get(this.myAppUrl + 'api/v1/Order/Get')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    addOrder(order: Order) {
        let header = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: header });
                
        return this
            ._http
            .post(this.myAppUrl + 'api/v1/Order/Post', JSON.stringify(order), options)
    }
    validateOrder(order: Order) {
        let header = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: header });

        return this
            ._http
            .post(this.myAppUrl + 'api/v1/Order/validate', JSON.stringify(order), options)
    }
    
    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}