import {HttpInterceptor, HttpInterceptorFn} from "@angular/common/http";
import {inject} from "@angular/core";
import {AuthService} from "../services/auth.service";
import {catchError} from "rxjs";

export const authTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const accessToken = inject(AuthService).accessToken;

  if (!accessToken) {
    return next(req);
  }

  req = req.clone({
    setHeaders: {
      Authorization: `Bearer ${accessToken}`
    }
  })

  return next(req);
}
