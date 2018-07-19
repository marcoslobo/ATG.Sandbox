declare var jquery: any;
declare var $: any;

import { TemplateRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../model/order.model';
import { OrderResult } from '../../model/orderResult.model';
import { Headers, Response } from '@angular/http';


import 'rxjs/Rx';
@Component({ templateUrl: './order.component.html' })


export class OrderComponent implements OnInit {

    orders: Array<OrderResult>;
    orderData: Order;


    constructor(private serv: OrderService) {
        this.orders = new Array<OrderResult>();
        this.orderData = new Order(0, '', '', '', 0);

    }

    ngOnInit() {
         this.loadOrders();
        $('#Quantity').mask("0000000000", { reverse: true });
                        
    }

    private loadOrders() {
        this
            .serv
            .getOrders()
            .subscribe((resp: Response) => {
                this.serv.getOrders().subscribe(
                    data => this.orders = data
                )

            });
    }


    addOrder() {


        if (this.orderData.price == null) {
            $.notify({
                message: 'O Preço necessita ser um número positivo!',
                icon: 'glyphicon glyphicon-warning-sign'
            },
                {
                    type: 'warning',
                    newest_on_top: true,
                });
            return;
        }


        if (this.orderData.quantity == '') {
            this.orderData.quantity = '0';
        }

        

        this
            .serv
            .validateOrder(this.orderData)
            .subscribe((resp: Response) => {
                if (resp.status != 200) {
                    $.notify({
                        message: resp.text,
                        icon: 'glyphicon glyphicon-warning-sign'
                    },
                        {
                            type: 'warning',
                            newest_on_top: true,
                        });
                } else {

                    $.notify({
                        message: 'Ordem enviada para a fila, aguardando processamento!',
                        icon: 'glyphicon glyphicon-warning-sign'
                    },
                        {
                            type: 'info'
                        });


                    var orderToAdd = $.extend(true, {}, this.orderData);
                    this.clearFields();

                    this
                        .serv
                        .addOrder(orderToAdd)
                        .subscribe((resp: Response) => {
                            if (resp.status != 200) {
                                $.notify({
                                    message: resp.text,
                                    icon: 'glyphicon glyphicon-warning-sign'
                                },
                                    {
                                        type: 'warning',
                                        newest_on_top: true,
                                    });
                            }
                            this.loadOrders();

                        }, function (err) {

                            $.notify({
                                message: err._body,
                                icon: 'glyphicon glyphicon-warning-sign'
                            },
                                {
                                    type: 'warning',
                                    newest_on_top: true,
                                });

                        });

                }


            }, function (err) {

                $.notify({
                    message: err._body,
                    icon: 'glyphicon glyphicon-warning-sign'
                },
                    {
                        type: 'warning',
                        newest_on_top: true,
                    });

            });







    }

    private clearFields() {
        this.orderData.price = 0;
        this.orderData.quantity = '';
        this.orderData.side = '';
        this.orderData.symbol = '';
    }

}



