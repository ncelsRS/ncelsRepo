import { TestBed, inject } from '@angular/core/testing';

import { IdentityInterceptorService } from './identity-interceptor.service';

describe('IdentityInterceptorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IdentityInterceptorService]
    });
  });

  it('should be created', inject([IdentityInterceptorService], (service: IdentityInterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
