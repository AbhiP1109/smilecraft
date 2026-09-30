using SmileCraft.API.Models;

namespace SmileCraft.API.Data;

/// <summary>
/// In-memory data store for clinic data.
/// In production, replace with a real database (EF Core, Dapper, etc.)
/// </summary>
public class ClinicDataStore
{
    public ClinicInfo ClinicInfo { get; set; } = new()
    {
        Name = "SmileCraft Dental Clinic",
        Tagline = "Your Smile, Our Priority",
        Description = "SmileCraft Dental Clinic provides world-class dental care with a personal touch. Our team of experienced dentists uses the latest technology to ensure you get the best treatment possible.",
        Address = "123 Dental Street, Medical District, Mumbai 400001",
        Phone = "+91 98765 43210",
        Email = "care@smilecraft.in",
        WorkingHours = "Mon - Sat: 9:00 AM - 8:00 PM, Sun: 10:00 AM - 2:00 PM"
    };

    public List<Service> Services { get; set; } = new()
    {
        new Service { Id = 1, Title = "Dental Cleaning", Description = "Professional cleaning for healthy teeth and gums.", Icon = "fa-solid fa-tooth" },
        new Service { Id = 2, Title = "Root Canal", Description = "Advanced painless root canal treatment.", Icon = "fa-solid fa-stethoscope" },
        new Service { Id = 3, Title = "Dental Implant", Description = "Permanent solution for missing teeth.", Icon = "fa-solid fa-heart" },
        new Service { Id = 4, Title = "Teeth Whitening", Description = "Professional whitening for a brighter smile.", Icon = "fa-solid fa-star" },
        new Service { Id = 5, Title = "Orthodontics", Description = "Braces and aligners for perfectly aligned teeth.", Icon = "fa-solid fa-teeth" },
        new Service { Id = 6, Title = "Cosmetic Dentistry", Description = "Complete smile makeover with veneers and bonding.", Icon = "fa-solid fa-face-smile" }
    };

    public List<Doctor> Doctors { get; set; } = new()
    {
        new Doctor { Id = 1, Name = "Dr. Rahul Sharma", Designation = "BDS, MDS", Experience = 12, Image = "assets/images/doctor1.png", Rating = 5 },
        new Doctor { Id = 2, Name = "Dr. Priya Patel", Designation = "Orthodontist", Experience = 10, Image = "assets/images/doctor2.png", Rating = 5 },
        new Doctor { Id = 3, Name = "Dr. Amit Verma", Designation = "Implantologist", Experience = 15, Image = "assets/images/doctor3.png", Rating = 5 }
    };

    public List<Stat> Stats { get; set; } = new()
    {
        new Stat { Title = "Happy Patients", Value = "1000+", Icon = "fa-solid fa-users" },
        new Stat { Title = "Years Experience", Value = "12+", Icon = "fa-solid fa-user-doctor" },
        new Stat { Title = "Google Rating", Value = "4.9", Icon = "fa-solid fa-star" },
        new Stat { Title = "Same Day", Value = "Available", Icon = "fa-solid fa-calendar-check" }
    };

    public List<Testimonial> Testimonials { get; set; } = new()
    {
        new Testimonial { Id = 1, Name = "Sneha Gupta", Review = "Amazing experience! The staff is very caring and the treatment was completely painless. Best dental clinic in Mumbai!", Rating = 5, Image = "assets/images/doctor.png" },
        new Testimonial { Id = 2, Name = "Rajesh Kumar", Review = "Dr. Rahul is fantastic. He explained everything clearly and the root canal was done smoothly. Highly recommended!", Rating = 5, Image = "assets/images/doctor.png" },
        new Testimonial { Id = 3, Name = "Priya Singh", Review = "Got my teeth whitening done here. Results are incredible! The clinic is clean and well-equipped.", Rating = 5, Image = "assets/images/doctor.png" },
        new Testimonial { Id = 4, Name = "Vikram Mehta", Review = "Very professional team. They made my dental implant feel like a breeze. Great follow-up care too.", Rating = 5, Image = "assets/images/doctor.png" }
    };

    public List<ContactMessage> ContactMessages { get; set; } = new();
}
