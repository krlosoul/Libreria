import { IAutores } from "./iautores";
import { ILibros } from "./ilibros";

export interface IAutoresLibros {
  autoresId: number;
  librosIsbn: number;
  autores: Array<IAutores>;
  libros: Array<ILibros>;
}
