import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfigService } from '../../shared/services/config.service';
import { ApiService } from '../../shared/services/api.service';
import { AppConfig } from '../../shared/models/config.model';
import { SafeUrlPipe } from '../../shared/pipes/safe-url.pipe';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, SafeUrlPipe],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {
  phone = '';
  email = '';
  address = '';
  hours: AppConfig['hours'] = [];
  mapLink = '';
  mapEmbedUrl = '';
  whatsappLink = '';
  contactForm: FormGroup;
  submitted = false;
  isSubmitting = false;
  errorMessage = '';

  constructor(
    private configService: ConfigService,
    private apiService: ApiService,
    private fb: FormBuilder
  ) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      message: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.configService.config$.subscribe(config => {
      if (config) {
        this.phone = config.contact.phone;
        this.email = config.contact.email;
        this.address = config.contact.address;
        this.hours = config.hours;
        this.mapLink = config.map.link;
        this.mapEmbedUrl = config.map.embedUrl;
        this.whatsappLink = config.social?.whatsapp || '';
      }
    });
  }

  onSubmit() {
    if (this.contactForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      this.errorMessage = '';
      this.submitted = false;

      const formData = {
        name: this.contactForm.value.name,
        email: this.contactForm.value.email,
        phone: this.contactForm.value.phone,
        message: this.contactForm.value.message
      };

      this.apiService.submitContact(formData).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          this.submitted = true;
          this.contactForm.reset();
          setTimeout(() => {
            this.submitted = false;
          }, 5000);
        },
        error: (error) => {
          this.isSubmitting = false;
          this.errorMessage = error.message || 'Something went wrong. Please try again.';
          console.error('Contact form submission error:', error);
        }
      });
    }
  }
}
