export interface AppConfig {
  clinic: ClinicConfig;
  contact: ContactConfig;
  social: SocialConfig;
  hours: WorkingHour[];
  map: MapConfig;
  seo: SeoConfig;
}

export interface ClinicConfig {
  name: string;
  tagline: string;
  description: string;
  logo: string;
  favicon: string;
}

export interface ContactConfig {
  address: string;
  phone: string;
  phoneSecondary: string;
  email: string;
  emergencyPhone: string;
}

export interface SocialConfig {
  facebook: string;
  instagram: string;
  twitter: string;
  linkedin: string;
  youtube: string;
  whatsapp: string;
}

export interface WorkingHour {
  days: string;
  time: string;
}

export interface MapConfig {
  embedUrl: string;
  link: string;
}

export interface SeoConfig {
  title: string;
  description: string;
  keywords: string;
}
