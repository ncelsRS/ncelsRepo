import { Injectable } from '@angular/core'
import { SideChat } from './side-chat.model';

let date = new Date(),
    day = date.getDate(),
    month = date.getMonth(),
    year = date.getFullYear(),
    hour = date.getHours(),
    minute = date.getMinutes();

let chats = [
    new SideChat(
        'assets/img/profile/ashley.jpg',
        'Ashley Ahlberg', 
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/profile/bruno.jpg',
        'Bruno Vespa',
        'Do not disturb',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-3.png',
        'Andy Warhol',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/profile/julia.jpg',
        'Julia Aniston',
        'Away',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/profile/adam.jpg',
        'Adam Sandler',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-7.png',
        'Lusia Manuel',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/profile/tereza.jpg',
        'Tereza Stiles',
        'Offline',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),    
    new SideChat(
        'assets/img/users/default-user.jpg',
        'unknown',
        'Offline',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-1.png',
        'Jeremi Powell',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-8.png',
        'Calico Jack',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/profile/michael.jpg',
        'Michael Blair',
        'Online',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-5.png',
        'Michelle Ormond',
        'Away',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    ),
    new SideChat(
        'assets/img/avatars/avatar-6.png',
        'Sean Connery',
        'Offline',
        'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
        new Date(year, month, day-2, hour, minute),
        'left'
    )
]

let talks = [
    new SideChat(
        'assets/img/users/user.jpg',
        'Emilio Verdines', 
        'Online',
        'Hi, StartNG is a fully responsive, organized folder structure, clean & customizable code, easy to use and much more...',
        new Date(year, month, day-2, hour, minute+2),
        'right'
    ),    
    new SideChat(
        'assets/img/profile/ashley.jpg',
        'Ashley Ahlberg', 
        'Online',
        'Great, then I\'ll definitely buy this theme. Thanks!',
        new Date(year, month, day-2, hour, minute+3),
        'left'
    ),
]

@Injectable()
export class SideChatService {

    public getChats():Array<Object> {
        return chats;
    }

    public getTalk():Array<Object> {
        return talks;
    }

}

















// import {Injectable} from '@angular/core'

// let date = new Date(),
//     day = date.getDate(),
//     month = date.getMonth(),
//     year = date.getFullYear(),
//     hour = date.getHours(),
//     minute = date.getMinutes();

// @Injectable()
// export class SideChatService {
    

//     private chats = [
//         {
//             image: 'assets/img/profile/ashley.jpg',
//             author: 'Ashley Ahlberg', 
//             authorStatus: 'Online',
//             text: 'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
//             date: new Date(year, month, day-2, hour, minute),
//             time: '1 min ago'
//         },
//         {
//             image: 'assets/img/profile/bruno.jpg',
//             author: 'Bruno Vespa', 
//             authorStatus: 'Do not disturb',
//             text: 'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
//             date: new Date(year, month, day-2, hour, minute),
//             time: '1 min ago'
//         },
//         {
//             image: 'assets/img/profile/julia.jpg',
//             author: 'Julia Aniston', 
//             authorStatus: 'Away',
//             text: 'Hi, I\'m looking for admin template with bootstrap 4.  What do you think about StartNG Admin Template?',
//             date: new Date(year, month, day-2, hour, minute),
//             time: '1 min ago'
//         },
//         {
//             image: 'assets/img/users/default-user.jpg',
//             author: 'unknown',
//             authorStatus: 'Offline',
//             text: 'After you get up and running, you can place Font Awesome icons just about...',
//             time: '1 min ago'
//         },



//         // {
//         //     image: 'michael',
//         //     author: 'Michael Blair',
//         //     text: 'You asked, Font Awesome delivers with 40 shiny new icons in version 4.2.',
//         //     time: '2 hrs ago'
//         // },
//         // {
//         //     image: 'julia',
//         //     author: 'Julia Aniston',
//         //     text: 'Want to request new icons? Here\'s how. Need vectors or want to use on the...',
//         //     time: '10 hrs ago'
//         // },
//         // {
//         //     image: 'bruno',
//         //     author: 'Bruno Vespa',
//         //     text: 'Explore your passions and discover new ones by getting involved. Stretch your...',
//         //     time: '1 day ago'
//         // },
//         // {
//         //     image: 'tereza',
//         //     author: 'Tereza Stiles',
//         //     text: 'Get to know who we are - from the inside out. From our history and culture, to the...',
//         //     time: '1 day ago'
//         // },
//         // {
//         //     image: 'adam',
//         //     author: 'Adam Sandler',
//         //     text: 'Need some support to reach your goals? Apply for scholarships across a variety of...',
//         //     time: '2 days ago'
//         // },

      
//     ]; 
//     public getChats():Array<Object> {
//         return this.chats;
//     }

// }