import { Component } from '@angular/core';

import { SectionTitleComponent } from '../../shared/ui/section-title/section-title.component';
import { DoctorCardComponent } from '../../shared/ui/doctor-card/doctor-card.component';
import { DataService } from '../../shared/services/data.service';

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [
    SectionTitleComponent,
    DoctorCardComponent
  ],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.css'
})
export class DoctorsComponent {

  constructor(public data: DataService) { }

}
