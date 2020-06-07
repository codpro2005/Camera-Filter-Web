import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Root } from '../../models/api';
import { Observable } from 'rxjs';
import { Named } from 'src/app/models/named';
import { ControlReference } from 'src/app/models/constants';

@Injectable({
  providedIn: 'root'
})
export class CameraFilterApiService {
  private controller = `${Root.local}camerafilter/`;

  constructor(private http: HttpClient) { }

  private getRequest<T>(action: string): Observable<T> {
    return this.http.get<T>(`${this.controller}${action}`);
  }

  private postRequest<T>(action: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.controller}${action}`, body);
  }

  public getFormData(): Observable<Named<Named<Named<string>[]>[]>> {
    return this.getRequest('getformdata');
  }

  public postFilteredData(filterIndex: number, parameters: any[], mediaBase64: string): Observable<ControlReference<string>> {
    return this.postRequest(`postfilteredmedia?filterIndex=${filterIndex}`, { parameters, mediaBase64 });
  }
}
