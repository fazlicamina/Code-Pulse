using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet <BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }


        public static void Initialize(IServiceProvider serviceProvider, bool isDevelopment)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (isDevelopment)
            {
                context.Database.EnsureCreated();

                
                if (!context.BlogPosts.Any() && !context.Categories.Any())
                {

                    var categories = new List<Category>
                    {
                         new Category { Id = Guid.NewGuid(), Name = "Programiranje", UrlHandle = "programiranje" },
                         new Category { Id = Guid.NewGuid(), Name = "Razvoj softvera", UrlHandle = "razvoj-softvera" },
                         new Category { Id = Guid.NewGuid(), Name = "Tehnologija", UrlHandle = "tehnologija" },
                         new Category { Id = Guid.NewGuid(), Name = "IT industrija", UrlHandle = "it-industrija" },
                         new Category { Id = Guid.NewGuid(), Name = "Kariijera", UrlHandle = "kariijera" },
                         new Category { Id = Guid.NewGuid(), Name = "Razvoj vještina", UrlHandle = "razvoj-vjestina" },
                         new Category { Id = Guid.NewGuid(), Name = "Online resursi", UrlHandle = "online-resursi" },
                         new Category { Id = Guid.NewGuid(), Name = "Alati", UrlHandle = "alati" },
                         new Category { Id = Guid.NewGuid(), Name = "Open Source", UrlHandle = "open-source" },
                         new Category { Id = Guid.NewGuid(), Name = "Hackathoni", UrlHandle = "hackathoni" }
                     };

                    context.Categories.AddRange(categories);

                    context.BlogPosts.AddRange(
                        new BlogPost
                        {
                            Id = Guid.NewGuid(),
                            Title = "Kako Efikasno Učiti Programiranje: Savjeti i Resursi",
                            ShortDescription = "Učenje programiranja može biti izazovno, ali uz pravi pristup možete brzo napredovati. Ovaj post nudi savjete za efikasno učenje, resurse i platforme koje vam mogu pomoći.",
                            Content = "Učenje programiranja je put koji može izgledati dug i zahtjevan, ali s pravim planom i resursima možete ga učiniti mnogo lakšim. Za početak, važno je odabrati pravi jezik za učenje. Ako ste početnik, Python je odličan izbor zbog svoje jednostavnosti i svestranosti. Zatim, odaberite online resurse koji vam omogućuju učenje u vlastitom tempu. Platforme poput Codecademy, freeCodeCamp i Udemy nude besplatne i plaćene tečajeve koji pokrivaju sve od osnova pa do naprednih tema.\r\n\r\nJedan od najvažnijih savjeta je stalno vježbanje. Programiranje se najbolje uči kroz praksu. Stvaranje vlastitih projekata, čak i jednostavnih, pomoći će vam da razumijete kako teorija funkcioniše u stvarnim situacijama. Kroz greške ćete naučiti najviše, pa nemojte se bojati pogriješiti.\r\n\r\nTakođer, ne zaboravite na zajednicu. Forumima poput StackOverflow i Reddit možete postavljati pitanja i razmjenjivati iskustva s drugim programerima, što će vam pomoći da brže napredujete.",
                            FeaturedImageUrl = "https://images.pexels.com/photos/5483077/pexels-photo-5483077.jpeg",
                            UrlHandle = "ucenje-programiranja",
                            Author = "Amina Fazlić",
                            PublishedDate = DateTime.Now,
                            IsVisible = true,
                            Categories = new List<Category> { categories[1], categories[4], categories[5], categories[2] }
                        },
                        new BlogPost
                        {
                            Id = Guid.NewGuid(),
                            Title = "Učenje Kroz Projekte: Zašto je Praksa Ključ za Razvoj IT Vještina",
                            ShortDescription = "Teorijsko znanje je važno, ali prava snaga dolazi kroz praksu. U ovom postu, istražujemo kako vam projekti mogu pomoći da razvijete svoje IT vještine.",
                            Content = "Učenje programiranja kroz projekte je jedan od najučinkovitijih načina da usavršite svoje vještine. Teorija vam može dati temelje, ali prava praksa dolazi kada radite na stvarnim projektima. Bez obzira jeste li početnik ili iskusan programer, rad na projektima omogućava vam da implementirate naučeno i prevaziđete izazove koje će vam teorija teško prikazati.\r\n\r\nJedan od najboljih načina da počnete jest stvaranje jednostavnih aplikacija. To mogu biti web stranice, to-do liste, blogovi ili čak igre. Kroz ovaj proces ćete naučiti kako upravljati projektom, raditi s bazama podataka, razumjeti sigurnosne aspekte i naučiti najbolje prakse kodiranja.\r\n\r\nAko želite dodatno ubrzati svoje učenje, sudjelujte u open-source projektima. Kroz open-source projekte možete raditi sa stručnjacima, dobiti povratne informacije i raditi na realnim problemima. Hackathoni su također odličan način da testirate svoje vještine pod pritiskom i u timu.\r\n\r\nNa kraju, izgradnja portfolija s projektima koji pokazuju vaše vještine može biti ključan faktor prilikom traženja posla. Poslodavci često žele vidjeti stvarne projekte koji pokazuju vašu sposobnost da rješavate stvarne probleme, a ne samo teorijska znanja.",
                            FeaturedImageUrl = "https://images.pexels.com/photos/7367/startup-photo.jpg",
                            UrlHandle = "ucenje-kroz-praksu",
                            Author = "Amina Fazlić",
                            PublishedDate = DateTime.Now,
                            IsVisible = true,

                            Categories = new List<Category> { categories[3], categories[5] }
                        }
                    );

                    
                  

                    context.SaveChanges(); 
                }
            }
        }

    }
}
