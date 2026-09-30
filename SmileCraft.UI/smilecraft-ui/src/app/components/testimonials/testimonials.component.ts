import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-testimonials',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './testimonials.component.html',
  styleUrl: './testimonials.component.css'
})
export class TestimonialsComponent {

  testimonials = [
    {
      name: 'Rahul Patil',
      location: 'Mumbai',
      rating: 5,
      review: 'Excellent dental care with friendly staff. The treatment was painless and the clinic is very clean.'
    },
    {
      name: 'Priya Sharma',
      location: 'Pune',
      rating: 5,
      review: 'Highly professional dentists and modern equipment. I am very happy with my smile makeover.'
    },
    {
      name: 'Amit Joshi',
      location: 'Nashik',
      rating: 5,
      review: 'The doctors explained every procedure clearly and the treatment was completely painless.'
    }
  ];

}
