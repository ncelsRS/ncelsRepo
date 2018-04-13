import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-ext-imn-set',
    templateUrl: './ext-imn-set.component.html',
    styleUrls: ['./ext-imn-set.component.css']
})
export class ExtImnSetComponent implements OnInit {

    public data = [];
    public table1Settings = {
        selectMode: 'single',  //single|multi
        hideHeader: false,
        hideSubHeader: false,
        actions: {
            columnTitle: 'Действия',
            add: true,
            edit: true,
            delete: true,
            custom: [],
            position: 'right' // left|right
        },
        add: {
            addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
            createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
            cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
        },
        edit: {
            editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
            saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
            cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
        },
        delete: {
            deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
            confirmDelete: true
        },
        noDataMessage: 'Нет данных',
        columns: {
            id: {
                title: '№\n' +
                '\n' +
                'п\\п',
                editable: false,
                width: '60px',
                type: 'html',
                valuePrepareFunction: (value) => {
                    return '<div class="text-center">' + value + '</div>';
                }
            },
            type: {
                title: 'Тип',
                type: 'string',
                filter: true
            },
            name: {
                title: 'Наименование',
                type: 'string'
            },
            ID: {
                title: 'ID',
                type: 'number'
            },
            model: {
                title: 'Модель',
                type: 'string'
            },
            user: {
                title: 'Производитель',
                type: 'string'
            },
            country: {
                title: 'Страна',
                type: 'string'
            }
        },
        pager: {
            display: true,
            perPage: 10
        }
    };

    public table2Settings = {
        selectMode: 'single',  //single|multi
        hideHeader: false,
        hideSubHeader: false,
        actions: {
            columnTitle: 'Действия',
            add: true,
            edit: true,
            delete: true,
            custom: [],
            position: 'right' // left|right
        },
        add: {
            addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
            createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
            cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
        },
        edit: {
            editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
            saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
            cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
        },
        delete: {
            deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
            confirmDelete: true
        },
        noDataMessage: 'Нет данных',
        columns: {
            id: {
                title: '№\n' +
                '\n' +
                'п\\п',
                editable: false,
                width: '60px',
                type: 'html',
                valuePrepareFunction: (value) => {
                    return '<div class="text-center">' + value + '</div>';
                }
            },
            type: {
                title: 'Вид (первичная или вторичная)\n' +
                '\n',
                type: 'string',
                filter: true
            },
            name: {
                title: 'Наименование',
                type: 'string'
            },
            ID: {
                title: 'Значение',
                type: 'string'
            },
            model: {
                title: 'Ед.изм.\n' +
                '\n',
                type: 'string'
            },
            user: {
                title: 'Кол-во ед. в упаковке\n' +
                '\n',
                type: 'string'
            },
            country: {
                title: 'Краткое описание\n' +
                '\n',
                type: 'string'
            }
        },
        pager: {
            display: true,
            perPage: 10
        }
    };

    constructor() {
        // this.getData((data) => {
        //     this.data = data;
        // });
    }

    ngOnInit() {
    }

    public getData(data) {
        const req = new XMLHttpRequest();
        req.open('GET', 'assets/data/users.json');
        req.onload = () => {
            data(JSON.parse(req.response));
        };
        req.send();
    }

    public onDeleteConfirm(event): void {
        if (window.confirm('Are you sure you want to delete?')) {
            event.confirm.resolve();
        } else {
            event.confirm.reject();
        }
    }

    public onRowSelect(event) {
        // console.log(event);
    }

    public onUserRowSelect(event) {
        //console.log(event);   //this select return only one page rows
    }

    public onRowHover(event) {
        //console.log(event);
    }

}
