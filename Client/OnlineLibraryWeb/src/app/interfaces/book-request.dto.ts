import {BookDTO} from "./book.dto";

export interface BookCreateRequestDTO extends BookDTO {
  Image: File | null;
  GenreId: string;
  AuthorId: string;
}
