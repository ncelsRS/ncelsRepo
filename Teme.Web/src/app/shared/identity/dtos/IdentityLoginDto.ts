import {IdentityDto} from "./IdentityDto";

export class IdentityLoginDto extends IdentityDto {

  public username: string;
  public password: string;

  constructor() {
    super("password");
  }
}
