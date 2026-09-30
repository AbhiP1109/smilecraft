import { Component, OnInit, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigService } from '../../shared/services/config.service';
import { AppConfig } from '../../shared/models/config.model';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent implements OnInit {
  clinicName = 'SmileCraft Dental Clinic';
  description = '';
  phone = '';
  email = '';
  social: AppConfig['social'] | null = null;
  currentYear = new Date().getFullYear();
  showBackToTop = false;

  constructor(private configService: ConfigService) {}

  ngOnInit(): void {
    this.configService.config$.subscribe(config => {
      if (config) {
        this.clinicName = config.clinic.name;
        this.description = config.clinic.description;
        this.phone = config.contact.phone;
        this.email = config.contact.email;
        this.social = config.social;
      }
    });
  }

  @HostListener('window:scroll')
  onScroll() {
    this.showBackToTop = window.scrollY > 500;
  }

  scrollTo(sectionId: string): void {
    document.getElementById(sectionId)?.scrollIntoView({ behavior: 'smooth' });
  }

  scrollToTop(): void {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
