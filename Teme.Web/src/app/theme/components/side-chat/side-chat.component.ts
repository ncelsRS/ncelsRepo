import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';
import { SideChatService } from './side-chat.service';
import { SideChat } from './side-chat.model';

@Component({
  selector: 'app-side-chat',
  templateUrl: './side-chat.component.html',
  styleUrls: ['./side-chat.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [ SideChatService ]
})
export class SideChatComponent implements OnInit {
  public settings: Settings;
  public showHoverableChatItem: boolean = false;
  public showChatWindow: boolean = false;
  public chats: Array<any>;
  public talks: Array<any>;
  public interlocutor: string; 
  public searchText: string;
  public newChatText: string = '';

  constructor(public appSettings:AppSettings, private sideChatService:SideChatService) { 
      this.settings = this.appSettings.settings;
      this.chats = sideChatService.getChats();
      this.talks = this.sideChatService.getTalk();
  }

  ngOnInit() { }

  public back(){
      this.showChatWindow = false
      this.talks.shift();
      this.talks.length = 2;
  }
  public getChat(chat){
      this.searchText = '';
      this.showChatWindow = true;
      this.interlocutor = chat.author;      
      this.talks.forEach(item => {
        if(item.side == 'left'){
          item.image = chat.image;
        }
      });
      this.talks.unshift(chat);     
  }

  public addChatItem($event) {     
      if (($event.which === 1 || $event.which === 13) && this.newChatText.trim() != '') {         
          this.talks.push(
              new SideChat(
                'assets/img/users/user.jpg', 
                'Emilio Verdines', 
                'online', 
                this.newChatText,
                new Date(), 
                'right')
          )
          this.newChatText = '';
          let chatContainer = document.querySelector('.chat-talk-list');
          setTimeout(() => {
              var nodes = chatContainer.querySelectorAll('.media');
              let newChatTextHeight = nodes[nodes.length- 1];
              chatContainer.scrollTop = chatContainer.scrollHeight + newChatTextHeight.clientHeight;
          });           
      }
  }

}