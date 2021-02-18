import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { ModalService } from './modal.service';


@Injectable({
  providedIn: 'root'
})
export class RestService {

  private baseUrl: string;
  private corsHeaders: HttpHeaders;

  constructor(private http: HttpClient, private modal: ModalService) {
    this.baseUrl = environment.baseUrl;
    this.corsHeaders = new HttpHeaders({
      'Access-Control-Allow-Request-Method': 'GET,POST,PUT,DELETE,OPTIONS',
      'Allow': 'GET,POST,PUT,DELETE,OPTIONS',
    });
  }

  get(url, showError = true): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get(this.baseUrl + url, { headers: this.corsHeaders }).subscribe(data => {
        resolve(data);
      }, error => {
        if (showError)
          this.errorHandler(error);
        reject(error);
      });
    });
  };

  post(url, data, showError = true) {
    return new Promise((resolve, reject) => {
      this.http.post(this.baseUrl + url, data, { headers: this.corsHeaders }).subscribe(data => {
        resolve(data);
      }, error => {
        if (showError)
          this.errorHandler(error);
        reject(error);
      });
    });
  };

  put(url, data, showError = true) {
    return new Promise((resolve, reject) => {
      this.http.put(this.baseUrl + url, data, { headers: this.corsHeaders }).subscribe(data => {
        resolve(data);
      }, error => {
        if (showError)
          this.errorHandler(error);
        reject(error);
      });
    });
  };

  delete(url, showError = true) {
    return new Promise((resolve, reject) => {
      this.http.delete(this.baseUrl + url, { headers: this.corsHeaders }).subscribe(data => {
        resolve(data);
      }, error => {
        if (showError)
          this.errorHandler(error);
        reject(error);
      });
    });
  };

  private errorHandler(error): void {
    let type = 4;
    let message = '';
    if (error.error) {
      message = error.error.message;
      if (error.error.type == 501) {
        type = 3;
      }
    } else {
      message = 'Se ha presentado una excepción no controlada, por favor contacte a soporte técnico.';
    }
    this.modal.showAlert(message, type);
  }
}
