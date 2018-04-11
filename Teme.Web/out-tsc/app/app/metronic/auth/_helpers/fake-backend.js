import { BaseRequestOptions, Http, RequestMethod, RequestOptions, Response, ResponseOptions, XHRBackend } from "@angular/http";
import { MockBackend } from "@angular/http/testing";
export function mockBackEndFactory(backend, options, realBackend) {
    // array in local storage for registered users
    var users = JSON.parse(localStorage.getItem('users')) || [];
    // fake token
    var token = 'fake-jwt-token';
    // configure fake backend
    backend.connections.subscribe(function (connection) {
        // wrap in timeout to simulate server api call
        setTimeout(function () {
            // authenticate
            if (connection.request.url.endsWith('/api/authenticate') && connection.request.method === RequestMethod.Post) {
                // get parameters from post request
                var params_1 = JSON.parse(connection.request.getBody());
                // find if any user matches login credentials
                var filteredUsers = users.filter(function (user) {
                    return user.email === params_1.email && user.password === params_1.password;
                });
                // default account
                if (params_1.email === 'demo@demo.com' && params_1.password === 'demo') {
                    filteredUsers[0] = {
                        fullName: 'Demo',
                        email: 'demo@demo.com',
                        password: 'demo',
                    };
                }
                if (filteredUsers.length) {
                    // if login details are valid return 200 OK with user details and fake jwt token
                    var user = filteredUsers[0];
                    connection.mockRespond(new Response(new ResponseOptions({
                        status: 200,
                        body: {
                            id: user.id,
                            fullName: user.fullName,
                            email: user.email,
                            token: token
                        }
                    })));
                }
                else {
                    // else return 400 bad request
                    connection.mockError(new Error('Email or password is incorrect'));
                }
                return;
            }
            // get users
            if (connection.request.url.endsWith('/api/users') && connection.request.method === RequestMethod.Get) {
                // check for fake auth token in header and return users if valid, this security
                // is implemented server side in a real application
                if (connection.request.headers.get('Authorization') === 'Bearer ' + token) {
                    connection.mockRespond(new Response(new ResponseOptions({ status: 200, body: users })));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    connection.mockRespond(new Response(new ResponseOptions({ status: 401 })));
                }
                return;
            }
            // get user by id
            if (connection.request.url.match(/\/api\/users\/\d+$/) && connection.request.method === RequestMethod.Get) {
                // check for fake auth token in header and return user if valid, this security is implemented server side in a real application
                if (connection.request.headers.get('Authorization') === 'Bearer ' + token) {
                    // find user by id in users array
                    var urlParts = connection.request.url.split('/');
                    var id_1 = parseInt(urlParts[urlParts.length - 1]);
                    var matchedUsers = users.filter(function (user) {
                        return user.id === id_1;
                    });
                    var user = matchedUsers.length ? matchedUsers[0] : null;
                    // respond 200 OK with user
                    connection.mockRespond(new Response(new ResponseOptions({ status: 200, body: user })));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    connection.mockRespond(new Response(new ResponseOptions({ status: 401 })));
                }
                return;
            }
            // create user
            if (connection.request.url.endsWith('/api/users') && connection.request.method === RequestMethod.Post) {
                // get new user object from post body
                var newUser_1 = JSON.parse(connection.request.getBody());
                // validation
                var duplicateUser = users.filter(function (user) {
                    return user.email === newUser_1.email;
                }).length;
                if (duplicateUser) {
                    return connection.mockError(new Error('Email "' + newUser_1.email + '" is already registered'));
                }
                // save new user
                newUser_1.id = users.length + 1;
                users.push(newUser_1);
                localStorage.setItem('users', JSON.stringify(users));
                // respond 200 OK
                connection.mockRespond(new Response(new ResponseOptions({ status: 200 })));
                return;
            }
            // delete user
            if (connection.request.url.match(/\/api\/users\/\d+$/) && connection.request.method === RequestMethod.Delete) {
                // check for fake auth token in header and return user if valid, this security is implemented server side in a real application
                if (connection.request.headers.get('Authorization') === 'Bearer ' + token) {
                    // find user by id in users array
                    var urlParts = connection.request.url.split('/');
                    var id = parseInt(urlParts[urlParts.length - 1]);
                    for (var i = 0; i < users.length; i++) {
                        var user = users[i];
                        if (user.id === id) {
                            // delete user
                            users.splice(i, 1);
                            localStorage.setItem('users', JSON.stringify(users));
                            break;
                        }
                    }
                    // respond 200 OK
                    connection.mockRespond(new Response(new ResponseOptions({ status: 200 })));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    connection.mockRespond(new Response(new ResponseOptions({ status: 401 })));
                }
                return;
            }
            // token verify
            if (connection.request.url.endsWith('/api/verify') && connection.request.method === RequestMethod.Get) {
                // check for fake auth token in header and return users if valid, this security
                // is implemented server side in a real application
                if (connection.request.headers.get('Authorization') === 'Bearer ' + token) {
                    connection.mockRespond(new Response(new ResponseOptions({ status: 200, body: { status: 'ok' } })));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    connection.mockRespond(new Response(new ResponseOptions({ status: 401 })));
                }
                return;
            }
            // forgot password
            if (connection.request.url.endsWith('/api/forgot-password') && connection.request.method === RequestMethod.Post) {
                // get parameters from post request
                var params_2 = JSON.parse(connection.request.getBody());
                // find if any user matches login credentials
                var filteredUsers = users.filter(function (user) {
                    return user.email === params_2.email;
                });
                if (filteredUsers.length) {
                    // in real world, if email is valid, send email change password link
                    var user = filteredUsers[0];
                    connection.mockRespond(new Response(new ResponseOptions({ status: 200 })));
                }
                else {
                    // else return 400 bad request
                    connection.mockError(new Error('User with this email does not exist'));
                }
                return;
            }
            // pass through any requests not handled above
            var realHttp = new Http(realBackend, options);
            var requestOptions = new RequestOptions({
                method: connection.request.method,
                headers: connection.request.headers,
                body: connection.request.getBody(),
                url: connection.request.url,
                withCredentials: connection.request.withCredentials,
                responseType: connection.request.responseType
            });
            realHttp.request(connection.request.url, requestOptions)
                .subscribe(function (response) {
                connection.mockRespond(response);
            }, function (error) {
                connection.mockError(error);
            });
        }, 500);
    });
    return new Http(backend, options);
}
export var fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: Http,
    deps: [MockBackend, BaseRequestOptions, XHRBackend],
    useFactory: mockBackEndFactory
};
//# sourceMappingURL=fake-backend.js.map