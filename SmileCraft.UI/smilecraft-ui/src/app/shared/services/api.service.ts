import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Service } from '../models/service.model';
import { Doctor } from '../models/doctor.model';
import { Stat } from '../models/stat.model';
import { Testimonial } from '../models/testimonial.model';

export interface ClinicInfo {
  name: string;
  tagline: string;
  description: string;
  address: string;
  phone: string;
  email: string;
  workingHours: string;
}

export interface ContactMessage {
  name: string;
  email: string;
  phone?: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getClinicInfo(): Observable<ClinicInfo> {
    return this.http.get<ClinicInfo>(`${this.apiUrl}/clinicinfo`)
      .pipe(catchError(this.handleError));
  }

  getServices(): Observable<Service[]> {
    return this.http.get<Service[]>(`${this.apiUrl}/services`)
      .pipe(catchError(this.handleError));
  }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.apiUrl}/doctors`)
      .pipe(catchError(this.handleError));
  }

  getStats(): Observable<Stat[]> {
    return this.http.get<Stat[]>(`${this.apiUrl}/stats`)
      .pipe(catchError(this.handleError));
  }

  getTestimonials(): Observable<Testimonial[]> {
    return this.http.get<Testimonial[]>(`${this.apiUrl}/testimonials`)
      .pipe(catchError(this.handleError));
  }

  submitContact(message: ContactMessage): Observable<{ success: boolean; message: string }> {
    return this.http.post<{ success: boolean; message: string }>(`${this.apiUrl}/contact`, message)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An unknown error occurred. Please try again later.';

    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      if (error.status === 0) {
        errorMessage = 'Unable to connect to the server. Please check your internet connection.';
      } else if (error.status === 404) {
        errorMessage = 'The requested resource was not found.';
      } else if (error.status === 400 && error.error) {
        // Validation errors
        if (typeof error.error === 'object' && error.error.message) {
          errorMessage = error.error.message;
        } else {
          errorMessage = 'Invalid request. Please check your input.';
        }
      } else if (error.status >= 500) {
        errorMessage = 'Server error. Please try again later.';
      }
    }

    console.error('API Error:', error);
    return throwError(() => new Error(errorMessage));
  }
}
