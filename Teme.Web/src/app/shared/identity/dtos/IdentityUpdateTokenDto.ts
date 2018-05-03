import {IdentityDto} from "./IdentityDto";

export class IdentityUpdateTokenDto extends IdentityDto {

  public refresh_token: string;

  constructor(refresh_token) {
    super("refresh_token");
    this.refresh_token = refresh_token;
  }
}
