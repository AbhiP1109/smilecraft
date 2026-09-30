import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Doctor } from '../../models/doctor.model';

@Component({
  selector: 'app-doctor-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './doctor-card.component.html',
  styleUrl: './doctor-card.component.css'
})
export class DoctorCardComponent {
  @Input() doctor!: Doctor;

  getInitials(name: string): string {
    return name
      .replace('Dr. ', '')
      .split(' ')
      .map(n => n[0])
      .join('')
      .toUpperCase();
  }

  getAvatarGradient(id: number): string {
    const gradients = [
      'linear-gradient(135deg, #0D9488, #14B8A6)',
      'linear-gradient(135deg, #F59E0B, #FBBF24)',
      'linear-gradient(135deg, #8B5CF6, #A78BFA)',
      'linear-gradient(135deg, #EC4899, #F472B6)',
    ];
    return gradients[(id - 1) % gradients.length];
  }
}
