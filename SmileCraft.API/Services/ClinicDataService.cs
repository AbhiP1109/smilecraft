using SmileCraft.API.Models;

namespace SmileCraft.API.Services;

/// <summary>
/// Singleton service that provides hardcoded clinic data.
/// </summary>
public class ClinicDataService
{
    private static readonly Lazy<ClinicDataService> _instance = new(() => new ClinicDataService());

    public static ClinicDataService Instance => _instance.Value;

    private ClinicDataService() { }

    public ClinicInfo GetClinicInfo() => new()
    {
        Name = "SmileCraft Dental Clinic",
        Tagline = "Your Smile, Our Priority",
        Description = "SmileCraft Dental Clinic is a state-of-the-art dental facility in Mumbai providing comprehensive dental care for the entire family. Our team of experienced dentists uses the latest technology to ensure you get the best treatment in a comfortable, caring environment.",
        Address = "123 Dental Street, Andheri West, Mumbai, Maharashtra 400053",
        Phone = "+91 7057893131",
        Email = "hello@smilecraft.in",
        WorkingHours = "Mon-Sat: 9:00 AM - 8:00 PM, Sun: 10:00 AM - 2:00 PM"
    };

    public List<Service> GetAllServices() => new()
    {
        new Service
        {
            Id = 1,
            Title = "Dental Cleaning",
            Description = "Professional teeth cleaning to remove plaque, tartar, and stains, keeping your gums healthy and your smile bright.",
            Icon = "fas fa-tooth"
        },
        new Service
        {
            Id = 2,
            Title = "Root Canal",
            Description = "Painless root canal treatment to save infected teeth and relieve pain using modern rotary endodontic techniques.",
            Icon = "fas fa-procedures"
        },
        new Service
        {
            Id = 3,
            Title = "Teeth Whitening",
            Description = "Advanced teeth whitening treatments that safely brighten your smile by several shades in just one session.",
            Icon = "fas fa-star"
        },
        new Service
        {
            Id = 4,
            Title = "Dental Implants",
            Description = "Permanent tooth replacement solutions with titanium implants that look, feel, and function like natural teeth.",
            Icon = "fas fa-teeth"
        },
        new Service
        {
            Id = 5,
            Title = "Orthodontics",
            Description = "Braces and clear aligner treatments (Invisalign) to straighten teeth and correct bite problems for all ages.",
            Icon = "fas fa-smile"
        },
        new Service
        {
            Id = 6,
            Title = "Pediatric Dentistry",
            Description = "Gentle and friendly dental care designed specifically for children, making their dental visits fun and stress-free.",
            Icon = "fas fa-child"
        }
    };

    public List<Doctor> GetAllDoctors() => new()
    {
        new Doctor
        {
            Id = 1,
            Name = "Dr. Rahul Sharma",
            Designation = "Chief Dental Surgeon",
            Experience = 15,
            Image = "/assets/doctors/doctor-1.jpg",
            Rating = 4.9
        },
        new Doctor
        {
            Id = 2,
            Name = "Dr. Priya Patel",
            Designation = "Orthodontist",
            Experience = 10,
            Image = "/assets/doctors/doctor-2.jpg",
            Rating = 4.8
        },
        new Doctor
        {
            Id = 3,
            Name = "Dr. Amit Verma",
            Designation = "Pediatric Dentist",
            Experience = 8,
            Image = "/assets/doctors/doctor-3.jpg",
            Rating = 4.7
        }
    };

    public List<Stat> GetAllStats() => new()
    {
        new Stat { Title = "Years Experience", Value = "15+", Icon = "fas fa-award" },
        new Stat { Title = "Happy Patients", Value = "10,000+", Icon = "fas fa-smile-beam" },
        new Stat { Title = "Dental Procedures", Value = "25,000+", Icon = "fas fa-teeth-open" },
        new Stat { Title = "Expert Doctors", Value = "5+", Icon = "fas fa-user-md" }
    };

    public List<Testimonial> GetAllTestimonials() => new()
    {
        new Testimonial
        {
            Id = 1,
            Name = "Sneha Kulkarni",
            Review = "Best dental clinic in Mumbai! Dr. Rahul Sharma is extremely professional and made my root canal completely painless. Highly recommended!",
            Rating = 5,
            Image = "/assets/testimonials/patient-1.jpg"
        },
        new Testimonial
        {
            Id = 2,
            Name = "Rajesh Mehta",
            Review = "I got Invisalign treatment from Dr. Priya Patel and the results are amazing. The staff is very supportive and the clinic is spotlessly clean.",
            Rating = 5,
            Image = "/assets/testimonials/patient-2.jpg"
        },
        new Testimonial
        {
            Id = 3,
            Name = "Anita Desai",
            Review = "Took my 6-year-old daughter to Dr. Amit Verma. He was so patient and gentle with her. She actually looks forward to her dental visits now!",
            Rating = 4,
            Image = "/assets/testimonials/patient-3.jpg"
        }
    };
}
