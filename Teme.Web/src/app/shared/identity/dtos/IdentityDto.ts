export class IdentityDto {

  public grant_type: string;
  public client_id: string = 'angular';
  public client_secret: string = '7a6214ae07c233ab52255efad0a76b18199318100266d58d231bf3972c4fc8b0';

  constructor(grant_type: string) {
    this.grant_type = grant_type;
  }
}
