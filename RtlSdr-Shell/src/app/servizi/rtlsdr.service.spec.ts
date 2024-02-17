import { TestBed } from '@angular/core/testing';

import { RtlsdrService } from './rtlsdr.service';

describe('RtlsdrService', () => {
  let service: RtlsdrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RtlsdrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
