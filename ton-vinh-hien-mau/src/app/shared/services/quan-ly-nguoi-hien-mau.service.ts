import { debounce, debounceTime } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QuanLyNguoiHienMauService {

  private _sharedHeaders = new HttpHeaders();
  constructor(
    private http: HttpClient,
    private router: Router) {
      this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
  }

  getAll(){
    return this.http.get(`${environment.apiUrl}/api/QuanLyNguoiHienMau/getAll`, {headers: this._sharedHeaders});
  }

  createNguoiHienMau(dottonvinhid: any, donviid: any, nguoihienmau: any){
    return this.http.post(`${environment.apiUrl}/api/QuanLyNguoiHienMau/Create?DottonvinhId=${dottonvinhid}&DonViId=${donviid}`,nguoihienmau,{headers: this._sharedHeaders,});
  }
  editNguoiHienMau(nguoihienmau:any){
    return this.http.post(`${environment.apiUrl}/api/QuanLyNguoiHienMau/Edit`,nguoihienmau,{headers: this._sharedHeaders,});
  }
  deleteNguoihienmau(nguoihienmauid: any){
    return this.http.put(`${environment.apiUrl}/api/QuanLyNguoiHienMau/Delete?NguoihienmauId=${nguoihienmauid}`,{headers: this._sharedHeaders,});
  }
  search(searchstring: any, gioitinh:any, namsinh:any){
    return this.http
          .get(`${environment.apiUrl}/api/QuanLyNguoiHienMau/Search?searchString=${searchstring}&gioitinh=${gioitinh}&namsinh=${namsinh}`,{headers: this._sharedHeaders,})
          .pipe(debounceTime(1200));

  }
}
