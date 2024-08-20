import {BookDTO} from "./book.dto";
import {GenreDTO} from "./genre.dto";
import {AuthorDTO} from "./author.dto";

export interface BookDetailsResponseDTO {
  bookDTO: BookDTO;
  genreDTO: GenreDTO;
  authorDTO: AuthorDTO;
  image: Uint8Array | null;
}
