using System.Collections.Generic;
using System.Data.Entity;
using Library.DAL.Enums;
using Library.DAL.Models;

namespace Library.DAL
{
    public class LibraryDBContextInitializer : DropCreateDatabaseIfModelChanges<LibraryDBContext>
    {
        protected override void Seed(LibraryDBContext db)
        {
            var autor1 = new Autor { Name = "Leo", Surname = "Tolstoy" };
            var autor2 = new Autor { Name = "Gustave", Surname = "Flaubert" };
            var autor3 = new Autor { Name = "F.Scott", Surname = "Fitzgerald" };
            var autor4 = new Autor { Name = "Vladimir", Surname = "Nabokov" };
            var autor5 = new Autor { Name = "George", Surname = "Eliot" };
            var autor6 = new Autor { Name = "Mark", Surname = "Twain" };
            var autor7 = new Autor { Name = "Anton", Surname = "Chekhov" };
            var autor8 = new Autor { Name = "Marcel", Surname = "Proust" };
            var autor9 = new Autor { Name = "William", Surname = "Shakespeare" };
            var autor10 = new Autor { Name = "National Geographic", FoundingDate = 1888 };
            var autor11 = new Autor { Name = "Elsevier", FoundingDate = 1880 };
            var autor12 = new Autor { Name = "Time Inc.", FoundingDate = 1922 };
            var autor13 = new Autor { Name = "Guardian Media Group", FoundingDate = 1907 };
            var autor14 = new Autor { Name = "Washington Post Company", FoundingDate = 1877 };
            var autor15 = new Autor { Name = "News Corp", FoundingDate = 2013 };

            db.Autors.AddRange(new List<Autor>() { autor1, autor2, autor3, autor4, autor5, autor6, autor7,
                autor8, autor9, autor10, autor11, autor12, autor13, autor14, autor15});

            var unit1 = new LibraryStorageUnit { Title = "Anna Karenina", Autor = autor1, UnitName = nameof(Book) };
            var unit2 = new LibraryStorageUnit { Title = "Madame Bovary", Autor = autor2, UnitName = nameof(Book) };
            var unit3 = new LibraryStorageUnit { Title = "War and Peace", Autor = autor1, UnitName = nameof(Book) };
            var unit4 = new LibraryStorageUnit { Title = "The Great Gatsby ", Autor = autor3, UnitName = nameof(Book) };
            var unit5 = new LibraryStorageUnit { Title = "Lolita", Autor = autor4, UnitName = nameof(Book) };
            var unit6 = new LibraryStorageUnit { Title = "Middlemarch ", Autor = autor5, UnitName = nameof(Book) };
            var unit7 = new LibraryStorageUnit { Title = "The Adventures of Huckleberry", Autor = autor6, UnitName = nameof(Book) };
            var unit8 = new LibraryStorageUnit { Title = "The Stories of Anton Chekhov", Autor = autor7, UnitName = nameof(Book) };
            var unit9 = new LibraryStorageUnit { Title = "In Search of Lost Time", Autor = autor8, UnitName = nameof(Book) };
            var unit10 = new LibraryStorageUnit { Title = "Hamlet ", Autor = autor9, UnitName = nameof(Book) };
            var unit11 = new LibraryStorageUnit { Title = "National Geographic Traveler", Autor = autor10, UnitName = nameof(Magazine) };
            var unit12 = new LibraryStorageUnit { Title = "Cell", Autor = autor11, UnitName = nameof(Magazine) };
            var unit13 = new LibraryStorageUnit { Title = "Chemical Engineering Journal", Autor = autor11, UnitName = nameof(Magazine) };
            var unit14 = new LibraryStorageUnit { Title = "Time", Autor = autor12, UnitName = nameof(Magazine) };
            var unit15 = new LibraryStorageUnit { Title = "Life", Autor = autor12, UnitName = nameof(Magazine) };
            var unit16 = new LibraryStorageUnit { Title = "Arctica Guide", Autor = autor10, UnitName = nameof(Brochure) };
            var unit17 = new LibraryStorageUnit { Title = "The Guardian", Autor = autor13, UnitName = nameof(Newspaper) };
            var unit18 = new LibraryStorageUnit { Title = "The Washington Post", Autor = autor14, UnitName = nameof(Newspaper) };
            var unit19 = new LibraryStorageUnit { Title = "The Wall Street Journal", Autor = autor15, UnitName = nameof(Newspaper) };
            var unit20 = new LibraryStorageUnit { Title = "New York Post", Autor = autor15, UnitName = nameof(Newspaper) };

            db.LibraryStorageUnits.AddRange(new List<LibraryStorageUnit>()
            {unit1, unit2, unit3, unit4, unit5, unit6, unit7, unit8,
                unit9, unit10, unit11, unit12, unit13, unit14, unit15,
            unit16, unit17, unit18, unit19, unit20});

            var book1 = new Book { Unit = unit1, Genre = BookGenre.RealisticFiction, ReleaseDate = 2008 };
            var book2 = new Book { Unit = unit2, Genre = BookGenre.RomanceNovel, ReleaseDate = 2005 };
            var book3 = new Book { Unit = unit3, Genre = BookGenre.Novel, ReleaseDate = 1989 };
            var book4 = new Book { Unit = unit4, Genre = BookGenre.Novel, ReleaseDate = 2011 };
            var book5 = new Book { Unit = unit5, Genre = BookGenre.Novel, ReleaseDate = 2001 };
            var book6 = new Book { Unit = unit6, Genre = BookGenre.Novel, ReleaseDate = 1976 };
            var book7 = new Book { Unit = unit7, Genre = BookGenre.Satire, ReleaseDate = 1979 };
            var book8 = new Book { Unit = unit8, Genre = BookGenre.Collections, ReleaseDate = 1985 };
            var book9 = new Book { Unit = unit9, Genre = BookGenre.Modernist, ReleaseDate = 2004 };
            var book10 = new Book { Unit = unit10, Genre = BookGenre.Tragedy, ReleaseDate = 1988 };
            var book11 = new Book { Unit = unit11, Genre = BookGenre.Tragedy, ReleaseDate = 1988 };

            db.Books.AddRange(new List<Book>()
            { book1, book2, book3, book4, book5, book6, book7, book7, book8, book9, book10, book11 });

            var magazine1 = new Magazine
            {
                IssueNumber = 60,
                ReleaseDate = 2017,
                Style = StylesOfPublications.PopularScience,
                Unit = unit11
            };

            var magazine2 = new Magazine
            {
                IssueNumber = 8,
                ReleaseDate = 2017,
                Style = StylesOfPublications.IndustrialAndPractical,
                Unit = unit12
            };

            var magazine3 = new Magazine
            {
                IssueNumber = 6,
                ReleaseDate = 2016,
                Style = StylesOfPublications.IndustrialAndPractical,
                Unit = unit13
            };

            var magazine4 = new Magazine
            {
                IssueNumber = 9,
                ReleaseDate = 2014,
                Style = StylesOfPublications.TheSocioPolitical,
                Unit = unit14
            };

            var magazine5 = new Magazine
            {
                IssueNumber = 12,
                ReleaseDate = 2017,
                Style = StylesOfPublications.TheSocioPolitical,
                Unit = unit15
            };

            db.Magazines.AddRange(new List<Magazine>() { magazine1, magazine2, magazine3, magazine4, magazine5 });

            var brochure1 = new Brochure
            {
                ReleaseDate = 2015,
                Unit = unit16,
                Type = BrochureType.Advertising
            };

            db.Brochures.Add(brochure1);

            var newspaper1 = new Newspaper
            {
                Unit = unit17,
                IssueNumber = 26,
                ReleaseDate = 2017,
                Type = NewspaperType.Daily
            };

            var newspaper2 = new Newspaper
            {
                Unit = unit18,
                IssueNumber = 34,
                ReleaseDate = 2017,
                Type = NewspaperType.Daily
            };

            var newspaper3 = new Newspaper
            {
                Unit = unit19,
                IssueNumber = 16,
                ReleaseDate = 2016,
                Type = NewspaperType.Daily
            };

            var newspaper4 = new Newspaper
            {
                Unit = unit20,
                IssueNumber = 42,
                ReleaseDate = 2017,
                Type = NewspaperType.Daily
            };

            db.Newspapers.AddRange(new List<Newspaper>() { newspaper1, newspaper2, newspaper3, newspaper4 });

            db.SaveChanges();
        }
    }
}