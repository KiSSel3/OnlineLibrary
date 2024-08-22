import {BookDTO} from "./book.dto";

export interface BookCreateRequestDTO {
  BookDTO: BookDTO
  Image: File | null;
  GenreId: string;
  AuthorId: string;
}
