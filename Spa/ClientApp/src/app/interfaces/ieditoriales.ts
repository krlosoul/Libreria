import { ILibros } from "./ilibros";

export interface IEditoriales {
  id: number;
  nombre: string;
  direccionCorrespondencia: string;
  telefono: string;
  correo: string;
  maxLibros: number
  libros: Array<ILibros>
}
