import { Component, OnInit } from '@angular/core';
import {RegisterType} from '../RegisterType';

@Component({
    selector: 'app-ext-general-information',
    templateUrl: './ext-general-information.component.html',
    styleUrls: ['./ext-general-information.component.css']
})
export class ExtGeneralInformationComponent implements OnInit {
    selectedLevel: string;
    levels: Array<RegisterType> = [
        {code: 'Registration', name: 'Регистрация'},
        {code: 'Reregistration', name: 'Перерегистрация'},
        {code: 'Edit', name: 'Внесение изменений'},

    ];
    public data = [];
    public changeTypeSettings = {
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
            changes: {
                title: 'Изменение',
                type: 'string',
                filter: true
            },
            type: {
                title: 'Тип',
                type: 'string'
            },
            reduction: {
                title: 'Редакция до внесения изменений',
                type: 'string'
            },
            incomeChanges: {
                title: 'Вносимые изменения',
                type: 'string'
            }
        },
        pager: {
            display: true,
            perPage: 10
        }
    };
    public termSettings = {
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
            view: {
                title: 'Вид',
                type: 'string',
                filter: true
            },
            term: {
                title: 'Срок хранения\\Гарантийный срок эксплуатации',
                type: 'string'
            },
            measure: {
                title: 'Ед.изм',
                type: 'string'
            },
            eternal: {
                title: 'Бессрочно',
                type: 'string'
            }
        },
        pager: {
            display: true,
            perPage: 10
        }
    };
    public registerSettings = {
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
            country: {
                title: 'Страна',
                type: 'string',
                filter: true
            },
            registrationNumber: {
                title: '№ регистрационного удостоверения (указывается при наличии)',
                type: 'string'
            },
            issueDate: {
                title: 'Дата выдачи',
                type: 'string'
            },
            term: {
                title: 'Срок действия',
                type: 'string'
            },
            eternal: {
                title: 'Бессрочно',
                type: 'string'
            }
        },
        pager: {
            display: true,
            perPage: 10
        }
    };

    changeLevel(lev: RegisterType) {
        this.selectedLevel = lev.name;
    }

    constructor() {
        this.selectedLevel = 'Registration';
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
