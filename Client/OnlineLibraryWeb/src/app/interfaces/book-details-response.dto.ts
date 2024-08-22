import {GenreDTO} from "./genre.dto";
import {BookResponseDTO} from "./book-response.dto";
import {AuthorResponseDTO} from "./author.response.dto";

export interface BookDetailsResponseDTO {
  bookResponseDTO: BookResponseDTO;
  genreDTO: GenreDTO;
  authorResponseDTO: AuthorResponseDTO;
  image: string;
}
