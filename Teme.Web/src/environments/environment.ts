// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  urls: {
    identity: 'http://localhost:9433',
    extContract: 'http://localhost:9101',
    admin: 'http://localhost:9121',
    payment: 'http://localhost:60825/Payment/',
    //reference: 'http://localhost:5121/api/reference/',
    reference: 'http://localhost:9001/Admin/api/reference/',
    contract: 'http://localhost:62559/Contract/',
    //contract: 'http://localhost:9001/Contract/Contract/'
    icon: 'http://localhost:49980/Icon/',
  }
};
