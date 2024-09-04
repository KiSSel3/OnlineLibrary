import {GenreDTO} from "./genre.dto";
import {BookResponseDTO} from "./book-response.dto";
import {AuthorResponseDTO} from "./author.response.dto";

export interface BookDetailsResponseDTO extends BookResponseDTO {
  genreResponseDTO: GenreDTO;
  authorResponseDTO: AuthorResponseDTO;
}
