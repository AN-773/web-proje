using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    private IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Clinic> Clinics { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Hospital> Hospitals { get; set; } = null!;

    public DbSet<HospitalClinic> HospitalClinics { get; set; } = null!;

    // public DbSet<HospitalDoctor> HospitalDoctors { get; set; } = null!;
    public DbSet<HospitalSpecialities> HospitalSpecialities { get; set; } = null!;
    public DbSet<Speciality> Specialities { get; set; } = null!;
    public DbSet<WorkingTimes> WorkingTimes { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(_configuration.GetConnectionString("default"),
            ServerVersion.AutoDetect(_configuration.GetConnectionString("default")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new List<IdentityRole>
            {
                new IdentityRole
                {
                    ConcurrencyStamp = "sdfjdsklfjs", Id = "admin", Name = "Admin", NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    ConcurrencyStamp = "fjsahlfkjss", Id = "user", Name = "User", NormalizedName = "USER"
                },
                new IdentityRole
                {
                    ConcurrencyStamp = "jfdsklfjsad", Id = "doctor", Name = "Doctor", NormalizedName = "DOCTOR"
                },
            }
        );

        var hasher = new PasswordHasher<User>();
        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new User
                {
                    Id = "1", Name = "admin", UserName = "admin", Email = "B201210562@sakarya.edu.tr",
                    NormalizedUserName = "B201210562@sakarya.edu.tr", EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "sau")
                },
                new User
                {
                    Id = "2", Name = "admin2", UserName = "admin2", Email = "B201210566@sakarya.edu.tr",
                    NormalizedUserName = "G201210568@sakarya.edu.tr", EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "sau")
                }
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = "admin", UserId = "1",
                },
                new IdentityUserRole<string>
                {
                    RoleId = "admin", UserId = "2",
                }
            }
        );

        //UserAppointment
        modelBuilder.Entity<User>()
            .HasMany(u => u.Appointments)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<Appointment>()
            .HasOne(u => u.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(u => u.UserId);

        //HospitalDoctor   
        // modelBuilder.Entity<Hospital>()
        //     .HasMany(h => h.Doctors)
        //     .WithMany(d => d.Hospitals)
        //     .UsingEntity<HospitalDoctor>();
        //
        // modelBuilder.Entity<Doctor>()
        //     .HasMany(h => h.Hospitals)
        //     .WithMany(d => d.Doctors)
        //     .UsingEntity<HospitalDoctor>();

        //HospitalClinic
        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Clinics)
            .WithMany(c => c.Hospitals)
            .UsingEntity<HospitalClinic>();

        modelBuilder.Entity<Clinic>()
            .HasMany(h => h.Hospitals)
            .WithMany(d => d.Clinics)
            .UsingEntity<HospitalClinic>();

        //HospitalSpecialities
        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Specialities)
            .WithMany(d => d.Hospitals)
            .UsingEntity<HospitalSpecialities>();

        modelBuilder.Entity<Speciality>()
            .HasMany(h => h.Hospitals)
            .WithMany(d => d.Specialities)
            .UsingEntity<HospitalSpecialities>();

        //Create Users
        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new User
                {
                    Id = "3", Name = "doktor1", UserName = "d1@d.com", NormalizedUserName = "d1@d.com",
                    Email = "d1@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "1.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "4", Name = "doktor2", UserName = "d2@d.com", NormalizedUserName = "d2@d.com",
                    Email = "d2@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "2.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "5", Name = "doktor3", UserName = "d3@d.com", NormalizedUserName = "d3@d.com",
                    Email = "d3@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "3.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "6", Name = "doktor4", UserName = "d4@d.com", NormalizedUserName = "d4@d.com",
                    Email = "d4@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "4.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "7", Name = "doktor5", UserName = "d5@d.com", NormalizedUserName = "d5@d.com",
                    Email = "d5@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "5.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "8", Name = "doktor6", UserName = "d6@d.com", NormalizedUserName = "d6@d.com",
                    Email = "d6@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "6.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "9", Name = "doktor7", UserName = "d7@d.com", NormalizedUserName = "d7@d.com",
                    Email = "d7@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "7.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "10", Name = "doktor8", UserName = "d8@d.com", NormalizedUserName = "d8@d.com",
                    Email = "d8@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "8.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "11", Name = "doktor9", UserName = "d9@d.com", NormalizedUserName = "d9@d.com",
                    Email = "d9@d.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0, ImageName = "9.jpg", TwoFactorEnabled = false
                },
                new User
                {
                    Id = "12", Name = "user1", UserName = "u1@u.com", NormalizedUserName = "u1@u.com",
                    Email = "u1@u.com", EmailConfirmed = true, PasswordHash = hasher.HashPassword(null, "123"),
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false
                },
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "3" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "4" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "5" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "6" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "7" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "8" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "9" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "10" },
                new IdentityUserRole<string> { RoleId = "doctor", UserId = "11" },
                new IdentityUserRole<string> { RoleId = "user", UserId = "12" },
            }
        );
    }
}