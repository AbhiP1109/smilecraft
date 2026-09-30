import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { AppConfig } from '../models/config.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configSubject = new BehaviorSubject<AppConfig | null>(null);
  public config$ = this.configSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(true);
  public loading$ = this.loadingSubject.asObservable();

  private errorSubject = new BehaviorSubject<string | null>(null);
  public error$ = this.errorSubject.asObservable();

  private configUrl = 'assets/config/app-config.json';
  private storageKey = 'app-config-override';

  constructor(private http: HttpClient) {}

  loadConfig(): Observable<AppConfig> {
    this.loadingSubject.next(true);
    this.errorSubject.next(null);

    // Check for localStorage override first
    const override = localStorage.getItem(this.storageKey);
    if (override) {
      try {
        const config = JSON.parse(override) as AppConfig;
        this.configSubject.next(config);
        this.loadingSubject.next(false);
        return of(config);
      } catch {
        localStorage.removeItem(this.storageKey);
      }
    }

    return this.http.get<AppConfig>(this.configUrl).pipe(
      tap({
        next: (config) => {
          this.configSubject.next(config);
          this.loadingSubject.next(false);
        },
        error: (err) => {
          console.error('Failed to load config:', err);
          this.errorSubject.next('Failed to load configuration. Please try again later.');
          this.loadingSubject.next(false);
        }
      })
    );
  }

  getConfig(): AppConfig | null {
    return this.configSubject.getValue();
  }

  getClinicName(): string {
    return this.getConfig()?.clinic?.name ?? 'SmileCraft Dental Clinic';
  }

  getTagline(): string {
    return this.getConfig()?.clinic?.tagline ?? 'Your Smile, Our Priority';
  }

  getDescription(): string {
    return this.getConfig()?.clinic?.description ?? '';
  }

  getLogo(): string {
    return this.getConfig()?.clinic?.logo ?? 'assets/images/logo.png';
  }

  getContact(): AppConfig['contact'] | null {
    return this.getConfig()?.contact ?? null;
  }

  getSocial(): AppConfig['social'] | null {
    return this.getConfig()?.social ?? null;
  }

  getHours(): AppConfig['hours'] {
    return this.getConfig()?.hours ?? [];
  }

  getMap(): AppConfig['map'] | null {
    return this.getConfig()?.map ?? null;
  }

  getSeo(): AppConfig['seo'] | null {
    return this.getConfig()?.seo ?? null;
  }
}
