import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-why-choose',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './why-choose.component.html',
  styleUrl: './why-choose.component.css'
})
export class WhyChooseComponent {

  features = [
    {
      icon: 'experienced',
      title: 'Experienced Dentists',
      description: 'Highly qualified dentists with years of experience.'
    },
    {
      icon: 'equipment',
      title: 'Modern Equipment',
      description: 'Advanced dental technology for accurate treatment.'
    },
    {
      icon: 'patient',
      title: 'Patient First',
      description: 'Comfortable and caring treatment for every patient.'
    },
    {
      icon: 'pricing',
      title: 'Affordable Pricing',
      description: 'Quality dental care at reasonable prices.'
    },
    {
      icon: 'timing',
      title: 'Flexible Timings',
      description: 'Flexible hours to suit your schedule.'
    },
    {
      icon: 'emergency',
      title: 'Emergency Care',
      description: 'Immediate treatment for dental emergencies.'
    }
  ];

}
