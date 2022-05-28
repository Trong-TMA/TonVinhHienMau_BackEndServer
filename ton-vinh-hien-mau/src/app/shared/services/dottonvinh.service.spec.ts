import { TestBed } from '@angular/core/testing';

import { DottonvinhService } from './dottonvinh.service';

describe('DottonvinhService', () => {
  let service: DottonvinhService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DottonvinhService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
