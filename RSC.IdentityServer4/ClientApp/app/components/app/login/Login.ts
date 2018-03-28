export class Login {
    grant_type: string;
    client_id: string;
    client_secret: string;
    scope: string;
    username: string;
    password: string;

    constructor() {
        this.grant_type = 'password';
        this.client_id = 'identity';
        this.client_secret = '5aff96a2179fec9ec2b30441b15a59b8995649eee4cc4b4a957efbd33545951f';
        this.scope = 'offline_access';
    }
}