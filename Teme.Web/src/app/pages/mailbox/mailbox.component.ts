import { Component, ViewEncapsulation } from '@angular/core';
import { Mail, MailboxService } from './mailbox.service';

@Component({
  selector: 'app-mailbox',
  templateUrl: './mailbox.component.html',
  styleUrls: ['./mailbox.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers:[ MailboxService ]
})
export class MailboxComponent {

  public mails: Array<any>;
  public mail: any;
  public newMail: boolean;
  public type:string = 'all';
  public searchText: string;

  constructor(private mailboxService: MailboxService) { }

  ngOnInit() {
      this.getMails();        
  }

  public getMails(){
      switch (this.type) {
          case 'all': 
              this.mails = this.mailboxService.getAllMails();
              break;
          case 'starred':
              this.mails =  this.mailboxService.getStarredMails();
              break;
          case 'sent':
              this.mails =  this.mailboxService.getSentMails();
              break;
          case 'drafts':
              this.mails =  this.mailboxService.getDraftMails();
              break;
          case 'trash':
              this.mails =  this.mailboxService.getTrashMails();
              break;
          default:
              this.mails =  this.mailboxService.getDraftMails();
      }  
  }

  public viewDetail(mail){
      this.mail = this.mailboxService.getMail(mail.id);    
      this.mails.forEach(m => m.selected = false);
      this.mail.selected = true;
      this.mail.unread = false;
      this.newMail = false;
  }

  public compose(){
      this.mail = null;
      this.newMail = true;
  }

  public setAsRead(){
      this.mail.unread = false;
  }

  public setAsUnRead(){
      this.mail.unread = true;
  }

  public delete() {
      this.mail.trash = true;
      this.mail.sent = false;
      this.mail.draft = false; 
      this.mail.starred = false; 
      this.getMails(); 
      this.mail = null;
  }

  public changeStarStatus() {       
      this.mail.starred = !this.mail.starred;
      this.getMails(); 
  }

  public restore(){
      this.mail.trash = false;
      this.type = 'all';
      this.getMails();
      this.mail = null; 
  }

}