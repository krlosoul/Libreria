import { IAutoresLibros } from "./iautores-libros";

export interface IAutores {
  id: number;
  nombreCompleto: string;
  fechaNacimiento: string;
  ciudad: string;
  correo: string;
  autoresLibros: Array<IAutoresLibros>;
}
