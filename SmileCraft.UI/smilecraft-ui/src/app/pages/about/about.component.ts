import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../../shared/services/config.service';

@Component({
  selector: 'app-about',
  imports: [],
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent implements OnInit {
  description = '';

  constructor(private configService: ConfigService) {}

  ngOnInit(): void {
    this.configService.config$.subscribe(config => {
      if (config) {
        this.description = config.clinic.description;
      }
    });
  }
}
