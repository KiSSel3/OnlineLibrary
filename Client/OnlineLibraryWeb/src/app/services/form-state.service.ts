import { Injectable } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class FormStateService {
  private formStateSubject = new BehaviorSubject<FormGroup>(new FormGroup({}));
  formState$ = this.formStateSubject.asObservable();

  updateFormState(form: FormGroup) {
    this.formStateSubject.next(form);
  }
}
