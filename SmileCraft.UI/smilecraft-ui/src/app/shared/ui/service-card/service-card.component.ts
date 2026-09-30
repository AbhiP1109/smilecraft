import { Component, Input } from '@angular/core';
import { Service } from '../../models/service.model';
import { CardComponent } from '../card/card.component';

@Component({
  selector: 'app-service-card',
  standalone: true,
  imports: [CardComponent],
  templateUrl: './service-card.component.html',
  styleUrl: './service-card.component.css'
})
export class ServiceCardComponent {

  @Input() service!: Service;

}
