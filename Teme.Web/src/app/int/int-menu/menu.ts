import { Menu } from './menu.model';

export const verticalMenuItems = [
    new Menu (1, 'Не распределенные', '/pages/dashboard', null, 'tachometer', null, false, 0),
    new Menu (2, 'В работе', '/pages/membership', null, 'users', null, false, 0),
    new Menu (3, 'На карректировке', null, null, 'laptop', null, true, 0),
    new Menu (4, 'Требует согдасования', '/pages/ui/buttons', null, 'keyboard-o', null, false, 3),
    new Menu (5, 'Согласованные', '/pages/ui/cards', null, 'address-card-o', null, false, 3),
    new Menu (6, 'Не согласованные', '/pages/ui/components', null, 'creative-commons', null, false, 3),
    new Menu (7, 'Активные', '/pages/ui/icons', null, 'font-awesome', null, false, 3),
    new Menu (8, 'Истекшие', '/pages/ui/list-group', null, 'th-list', null, false, 3),
    new Menu (9, 'Все', '/pages/ui/media-objects', null, 'object-group', null, false, 3),
    ]

export const horizontalMenuItems = [
    new Menu (2, 'Отчеты', '/pages/membership', null, 'users', null, false, 0),
    new Menu (3, 'Договоры', null, null, 'laptop', null, true, 0),
    new Menu (4, 'Договоры', '/int/spa/contracts', null, 'keyboard-o', null, false, 3),
    new Menu (5, 'Договоры', '/int/spa/contracts', null, 'address-card-o', null, false, 3),
    new Menu (20, 'Заявки', null, null, 'pencil-square-o', null, true, 0),
    new Menu (21, 'Заявки', '/int/spa/declarations', null, 'check-square-o', null, false, 20),
    new Menu (22, 'Заявки', '/int/spa/declarations', null, 'th-large', null, false, 20),
   ]
