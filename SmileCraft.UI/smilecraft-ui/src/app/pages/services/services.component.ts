import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-services',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './services.component.html',
  styleUrl: './services.component.css'
})
export class ServicesComponent {

  services = [
    {
      icon: 'cleaning',
      title: 'Teeth Cleaning',
      description: 'Professional scaling and polishing to keep your teeth healthy and bright.'
    },
    {
      icon: 'smile',
      title: 'Smile Designing',
      description: 'Enhance your smile with advanced cosmetic dental treatments.'
    },
    {
      icon: 'implant',
      title: 'Dental Implant',
      description: 'Permanent and natural-looking replacement for missing teeth.'
    },
    {
      icon: 'root',
      title: 'Root Canal',
      description: 'Pain-free root canal treatment using modern technology.'
    },
    {
      icon: 'braces',
      title: 'Braces & Aligners',
      description: 'Straighten your teeth comfortably with braces or clear aligners.'
    },
    {
      icon: 'whitening',
      title: 'Teeth Whitening',
      description: 'Professional whitening treatment for a brighter and confident smile.'
    }
  ];

}
