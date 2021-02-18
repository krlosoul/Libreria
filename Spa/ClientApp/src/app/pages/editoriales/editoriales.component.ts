import { Component, OnInit } from '@angular/core';
import { IEditoriales } from '../../interfaces/ieditoriales';
import { ModalService } from '../../services/modal.service';
import { OnSelectTableService } from '../../services/on-select-table.service';
import { RestService } from '../../services/rest.service';

@Component({
  selector: 'app-editoriales',
  templateUrl: './editoriales.component.html',
  styleUrls: ['./editoriales.component.css']
})

export class EditorialesComponent implements OnInit {

  collectionSize: number;
  page: number;
  pageSize: number;
  enable: boolean;
  model: IEditoriales;
  DataGrid: Array<IEditoriales>;

  constructor(private rest: RestService, private modal: ModalService, private onselect: OnSelectTableService) {

    this.page = 1;
    this.pageSize = 5;
    this.collectionSize = 0;
    this.enable = false;

    this.DataGrid = [];

    this.model = {
      id: 0,
      nombre: '',
      direccionCorrespondencia: '',
      telefono: '',
      correo: '',
      maxLibros: -1,
      libros : null
    };

  }

  crear() {
    this.enable = true;

    this.model = {
      id: 0,
      nombre: '',
      direccionCorrespondencia: '',
      telefono: '',
      correo: '',
      maxLibros: -1,
      libros: null
    };
  }

  cancelar() {
    this.enable = false;

    this.model = {
      id: 0,
      nombre: '',
      direccionCorrespondencia: '',
      telefono: '',
      correo: '',
      maxLibros: -1,
      libros: null
    };
  }

  ngOnInit() {
    this.cargarGrid();
  }

  guardar() {
    let message = '';
    if (this.model.nombre.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Nombre"</li>';
    }
    if (this.model.nombre.length > 45) {
      message += '<li>El valor del campo "Nombre" no debe superar los 45 carácteres</li>';
    }

    if (this.model.direccionCorrespondencia.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Direccion de Correspondencia"</li>';
    }
    if (this.model.direccionCorrespondencia.length > 45) {
      message += '<li>El valor del campo "Direccion de Correspondencia" no debe superar los 45 carácteres</li>';
    }

    if (this.model.telefono.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Telefono"</li>';
    }
    if (this.model.telefono.length > 45) {
      message += '<li>El valor del campo "Telefono" no debe superar los 45 carácteres</li>';
    }

    if (this.model.correo.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Correo"</li>';
    }
    if (this.model.correo.length > 45) {
      message += '<li>El valor del campo "Correo" no debe superar los 45 carácteres</li>';
    }


    if (message == '') {
      this.modal.showLoading('Almacenando información');
      if (this.model.id == 0) {
        this.rest.post('api/Editoriales', this.model).then(res => {
          this.modal.showAlert('Información almacenada satisfactoriamente', 2);
          this.cargarGrid();
          this.modal.hideLoading();
          this.cancelar();
        }).catch(err => {
          this.modal.hideLoading();
        });
      } else {
        this.rest.put('api/Editoriales/' + this.model.id, this.model).then(res => {
          this.modal.showAlert('Información almacenada satisfactoriamente', 2);
          this.cargarGrid();
          this.modal.hideLoading();
          this.cancelar();
        }).catch(err => {
          this.modal.hideLoading();
        });
      }
    }
  }

  eliminar() {
    let data = this.onselect.Select('tbl');
    if (data === null) {
      this.modal.showAlert('Debe seleccionar una fila para continuar', 3);
    } else {
      this.modal.showLoading('Eliminando información');
      this.rest.delete('api/Editoriales/' + data.id).then(res => {
        this.modal.showAlert('Información eliminada satisfactoriamente', 2);
        this.cargarGrid();
        this.modal.hideLoading();
      }).catch(err => {
        this.modal.hideLoading();
      });
    }
  }

  editar() {
    let data = this.onselect.Select('tbl');
    if (data === null) {
      this.modal.showAlert('Debe seleccionar una fila para continuar', 3);
    } else {
      this.model = data;
      this.enable = true;
    }
  }

  cargarGrid() {
    this.modal.showLoading('Cargando Información');
    this.rest.get('api/editoriales?take=' + this.pageSize + '&skip=' + (((this.page - 1) * this.pageSize))).then((res) => {
      this.DataGrid = res.data;
      this.collectionSize = res.count;
      this.modal.hideLoading();
    }).catch(err => {
      this.modal.hideLoading();
    });
  }

  onChange(e) {
    this.page = e;
    this.cargarGrid();
  }

  onSelect(e: MouseEvent, item: any) {
    this.onselect.onSelect(e, item);
  }
}
