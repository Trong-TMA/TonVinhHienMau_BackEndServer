import { TestBed } from '@angular/core/testing';

import { DeXuatNguoiHienMauService } from './de-xuat-nguoi-hien-mau.service';

describe('DeXuatNguoiHienMauService', () => {
  let service: DeXuatNguoiHienMauService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeXuatNguoiHienMauService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
