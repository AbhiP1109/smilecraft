import { Injectable } from '@angular/core';

import { Service } from '../models/service.model';
import { Stat } from '../models/stat.model';
import { Doctor } from '../models/doctor.model';
import { Testimonial } from '../models/testimonial.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  services: Service[] = [
    {
      id: 1,
      title: 'Dental Cleaning',
      description: 'Professional cleaning for healthy teeth.',
      image: 'assets/images/service1.jpg',
      icon: 'fa-solid fa-tooth'
    },
    {
      id: 2,
      title: 'Root Canal',
      description: 'Advanced painless root canal treatment.',
      image: 'assets/images/service2.jpg',
      icon: 'fa-solid fa-stethoscope'
    },
    {
      id: 3,
      title: 'Dental Implant',
      description: 'Permanent solution for missing teeth.',
      image: 'assets/images/service3.jpg',
      icon: 'fa-solid fa-heart'
    }
  ];

  stats: Stat[] = [
    {
      title: 'Happy Patients',
      value: '1000+',
      icon: 'fa-solid fa-users'
    },
    {
      title: 'Years Experience',
      value: '12+',
      icon: 'fa-solid fa-user-doctor'
    },
    {
      title: 'Google Rating',
      value: '4.9',
      icon: 'fa-solid fa-star'
    },
    {
      title: 'Same Day',
      value: 'Available',
      icon: 'fa-solid fa-calendar-check'
    }
  ];

  doctors: Doctor[] = [
    {
      id: 1,
      name: 'Dr. Rahul Sharma',
      designation: 'BDS, MDS',
      experience: '12 Years',
      image: 'assets/images/doctor1.png',
      rating: 5
    },
    {
      id: 2,
      name: 'Dr. Priya Patel',
      designation: 'Orthodontist',
      experience: '10 Years',
      image: 'assets/images/doctor2.png',
      rating: 5
    },
    {
      id: 3,
      name: 'Dr. Amit Verma',
      designation: 'Implantologist',
      experience: '15 Years',
      image: 'assets/images/doctor3.png',
      rating: 5
    }
  ];

  testimonials: Testimonial[] = [
    {
      id: 1,
      name: 'Rahul Patil',
      review: 'Excellent dental care with friendly staff. The treatment was painless and the clinic is very clean.',
      rating: 5,
      image: 'assets/images/doctor1.png'
    },
    {
      id: 2,
      name: 'Priya Sharma',
      review: 'Highly professional dentists and modern equipment. I am very happy with my smile makeover.',
      rating: 5,
      image: 'assets/images/doctor2.png'
    },
    {
      id: 3,
      name: 'Amit Joshi',
      review: 'The doctors explained every procedure clearly and the treatment was completely painless.',
      rating: 5,
      image: 'assets/images/doctor3.png'
    }
  ];

}
