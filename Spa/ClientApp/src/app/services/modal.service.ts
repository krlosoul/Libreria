import { Injectable } from '@angular/core';
import * as $ from 'jquery';

@Injectable({
  providedIn: 'root'
})

export class ModalService {

  constructor() { }

  /**
   * Función encargada de mostrar un mensaje modal
   * @param message texto que deseas que aparezca en la ventana modal
   * @param type Tipo de notificación que desea mostrar 
   * @enum 1-Información
   * @enum 2-Exito
   * @enum 3-Advertencia
   * @enum 4-Error
   */
  showAlert(message: string, type: number) {
    let clase = '', titulo = '';

    if (type === 1) {
      clase = 'alert-info';
      titulo = 'Información';
    } else if (type === 2) {
      clase = 'alert-success';
      titulo = 'Exito';
    } else if (type === 3) {
      clase = 'alert-warning';
      titulo = 'Advertencia';
    } else if (type === 4) {
      clase = 'alert-danger';
      titulo = 'Error';
    }

    let bodyAlert = document.createElement('div');
    let html = '<div class="customModal alert ' + clase + ' alert-dismissible fade show" role="alert">';
    html += '<strong>' + titulo + '</strong><p>' + message + '</p>';
    html += '<button type="button" class="close" data-dismiss="alert" aria-label="Close">';
    html += '<span aria-hidden="true">&times;</span></button></div>';

    bodyAlert.innerHTML = html;

    $('body').append(bodyAlert);
    setTimeout(() => {
      $(bodyAlert).find('div').first().removeClass('show');
      $(bodyAlert).find('div').first().addClass('hidemodal');
    }, 50);
  }

  /**
   * Función encargada de mostrar una ventana de espera con un mensaje para el usuario
   * @param message Mensaje que se mostrará al usuario mientras se realiza la espera
   */
  showLoading(message) {
    $('#loadingComponent').css('display', 'inline-block');
    $('#loadingComponent').find('span').html(message);
  }

  /**
   * Función encargada de cambiar el texto mostrado en una ventana de espera
   * @param message Mensaje a cambiar en una espera abierta
   */
  changeLoadingText(message) {
    $('#loadingComponent').find('span').html(message);
  }

  /**
   * Funcíon encargada de ocultar una espera, esta tambien limpia el texto del último mensaje mostrado
   */
  hideLoading() {
    $('#loadingComponent').css('display', 'none');
    $('#loadingComponent').find('span').html('');
  }

}
