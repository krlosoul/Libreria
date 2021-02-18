import { IAutoresLibros } from "./iautores-libros";
import { IEditoriales } from "./ieditoriales";

export interface ILibros {
  isbn: number;
  titulo: string;
  genero: string;
  numeroPaginas: string;
  editorialesId: number;
  editorial: IEditoriales;
  autoresLibros: IAutoresLibros;
}
