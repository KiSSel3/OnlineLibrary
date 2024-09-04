import {BookDTO} from "./book.dto";

export interface BookResponseDTO extends BookDTO {
  id: string;
  image: string;
}
