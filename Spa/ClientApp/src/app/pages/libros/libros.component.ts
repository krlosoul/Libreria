import { Component, OnInit } from '@angular/core';
import { IEditoriales } from '../../interfaces/ieditoriales';
import { ILibros } from '../../interfaces/ilibros';
import { IAutores } from '../../interfaces/iautores';
import { IAutoresLibros } from '../../interfaces/iautores-libros';
import { ModalService } from '../../services/modal.service';
import { OnSelectTableService } from '../../services/on-select-table.service';
import { RestService } from '../../services/rest.service';
import * as jquery from 'jquery';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
  styleUrls: ['./libros.component.css']
})

export class LibrosComponent implements OnInit {

  collectionSize: number;
  page: number;
  pageSize: number;
  enable: boolean;
  editoriales: Array<IEditoriales>;
  autores: Array<IAutores>;
  model: ILibros;
  idAutor: number;
  DataGrid: Array<ILibros>;
  nullItem: any;
  isEditing: boolean;

  constructor(private rest: RestService, private modal: ModalService, private onselect: OnSelectTableService) {

    this.page = 1;
    this.pageSize = 5;
    this.collectionSize = 0;
    this.enable = false;
    this.editoriales = [];
    this.DataGrid = [];
    this.nullItem = null;
    this.isEditing = false;

    this.idAutor = 0;

    this.model = {
      isbn: null,
      titulo: '',
      genero: '',
      numeroPaginas: '',
      editorialesId: null,
      editorial: null,
      autoresLibros: null,
    };
  }

  crear() {
    this.isEditing = false;
    this.enable = true;

    this.idAutor = 0;

    this.model = {
      isbn: null,
      titulo: '',
      genero: '',
      numeroPaginas: '',
      editorialesId: null,
      editorial: null,
      autoresLibros: null,
    };
  }

  cancelar() {
    this.enable = false;
    this.isEditing = false;

    this.idAutor = 0;

    this.model = {
      isbn: null,
      titulo: '',
      genero: '',
      numeroPaginas: '',
      editorialesId: null,
      editorial: null,
      autoresLibros: null,
    };
  }

  ngOnInit() {
    this.cargarGrid();
    this.rest.get('api/Editoriales/list').then(res => {
      this.editoriales = res;
    });
    this.rest.get('api/Autores/list').then(res => {
      this.autores = res;
    });
  }

  guardar() {
    let message = '';
    if (this.model.isbn != null) {
      if (this.model.isbn.toString().length > 13) {
        message += '<li>El valor del campo "ISBN" no debe superar los 13 carácteres</li>';
      }
    }
    else {
      message += '<li>Debe ingresar un valor en el campo "ISBN"</li>';
    }

    if (this.model.titulo.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Titulo"</li>';
    }
    if (this.model.titulo.length > 45) {
      message += '<li>El valor del campo "Título" no debe superar los 45 carácteres</li>';
    }

    if (this.model.genero.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Genero"</li>';
    }
    if (this.model.genero.length > 45) {
      message += '<li>El valor del campo "Genero" no debe superar los 45 carácteres</li>';
    }

    if (this.model.numeroPaginas.trim() == '') {
      message += '<li>Debe ingresar un valor en el campo "Número de Páginas"</li>';
    }
    if (this.model.numeroPaginas.length > 45) {
      message += '<li>El valor del campo "Número de Páginas" no debe superar los 45 carácteres</li>';
    }

    if (this.model.editorialesId == null) {
      message += '<li>Debe seleccionar un valor en el campo "Editorial"</li>';
    }

    if (message == '') {
      this.modal.showLoading('Almacenando información');
      if (!this.isEditing) {
        this.rest.post('api/Libros/' + this.idAutor, this.model).then(res => {
          this.modal.showAlert('Información almacenada satisfactoriamente', 2);
          this.cargarGrid();
          this.modal.hideLoading();
          this.cancelar();
        }).catch(err => {
          this.modal.hideLoading();
        });
      } else {
        this.rest.put('api/Libros/' + this.model.isbn + '/' + this.idAutor, this.model).then(res => {
          this.modal.showAlert('Información almacenada satisfactoriamente', 2);
          this.cargarGrid();
          this.modal.hideLoading();
          this.cancelar();
        }).catch(err => {
          this.modal.hideLoading();
        });
      }
    } else {
      this.modal.showAlert(message, 3);
    }
  }

  eliminar() {
    let data = this.onselect.Select('tbl');
    if (data === null) {
      this.modal.showAlert('Debe seleccionar una fila para continuar', 3);
    } else {
      this.modal.showLoading('Eliminando información');
      this.isEditing = false;
      this.rest.delete('api/Libros/' + data.isbn).then(res => {
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
      this.isEditing = true;
      this.model = data;
      this.idAutor = data.autoresLibros[0].autoresId
      this.enable = true;
    }
  }

  cargarGrid() {
    this.modal.showLoading('Cargando Información');
    this.rest.get('api/Libros?take=' + this.pageSize + '&skip=' + (((this.page - 1) * this.pageSize))).then((res) => {
      this.DataGrid = res.data;console.log(this.DataGrid)
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
