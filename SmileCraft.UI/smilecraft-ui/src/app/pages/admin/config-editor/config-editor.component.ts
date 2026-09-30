import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfigService } from '../../../shared/services/config.service';
import { AppConfig } from '../../../shared/models/config.model';

@Component({
  selector: 'app-config-editor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './config-editor.component.html',
  styleUrl: './config-editor.component.css'
})
export class ConfigEditorComponent implements OnInit {
  config: AppConfig | null = null;
  saved = false;
  errorMessage = '';

  constructor(private configService: ConfigService) {}

  ngOnInit(): void {
    this.configService.config$.subscribe(config => {
      if (config) {
        this.config = JSON.parse(JSON.stringify(config));
      }
    });
  }

  save(): void {
    if (!this.config) return;
    localStorage.setItem('app-config-override', JSON.stringify(this.config));
    this.saved = true;
    setTimeout(() => this.saved = false, 3000);
  }

  reset(): void {
    localStorage.removeItem('app-config-override');
    this.configService.loadConfig().subscribe();
  }

  addHour(): void {
    if (this.config) {
      this.config.hours.push({ days: '', time: '' });
    }
  }

  removeHour(index: number): void {
    if (this.config) {
      this.config.hours.splice(index, 1);
    }
  }
}
