import { TestBed } from '@angular/core/testing';

import { QuanLyNguoiHienMauService } from './quan-ly-nguoi-hien-mau.service';

describe('QuanLyNguoiHienMauService', () => {
  let service: QuanLyNguoiHienMauService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuanLyNguoiHienMauService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
