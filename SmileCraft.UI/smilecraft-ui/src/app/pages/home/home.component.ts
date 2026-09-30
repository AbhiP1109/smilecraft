import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AboutComponent } from '../about/about.component';
import { ServicesComponent } from '../services/services.component';
import { StatsComponent } from '../../components/stats/stats.component';
import { TestimonialsComponent } from '../../components/testimonials/testimonials.component';
import { ContactComponent } from '../../components/contact/contact.component';
import { WhyChooseComponent } from '../../components/why-choose/why-choose.component';
import { DoctorsComponent } from '../../components/doctors/doctors.component';
import { ConfigService } from '../../shared/services/config.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    StatsComponent,
    AboutComponent,
    ServicesComponent,
    TestimonialsComponent,
    ContactComponent,
    WhyChooseComponent,
    DoctorsComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  tagline = '';
  description = '';
  phone = '';

  constructor(private configService: ConfigService) {}

  ngOnInit(): void {
    this.configService.config$.subscribe(config => {
      if (config) {
        this.tagline = config.clinic.tagline;
        this.description = config.clinic.description;
        this.phone = config.contact.phone;
      }
    });
  }
}
