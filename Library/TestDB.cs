using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class ApplicationContext : DbContext
    {
        //Данные на случай заполнения базы данной

        /*
       static List<Reader> DBReaders = new List<Reader>
       {
           new Reader(0, "Иван"),
           new Reader(1, "Василий"),
           new Reader(2, "Михаил")
       };

       static List<Book> DBBooks = new List<Book>
       {
           new Book(0, "Совершенный код", "Стив Макконнелл", "Если вы планируете построить успешную карьеру программиста, то это та книга, которую прочитать вы просто обязаны. Абсолютно неважно, в какой среде вы планируете работать, какой ваш уровень подготовки, новичок вы или уже руководитель – здесь найдётся полезная информация для каждого, кто хоть как-то связан с процессом создания кода. Совершенного кода.",
               0),
           new Book(1, "Чистый код. Создание, анализ и рефакторинг","Роберт К. Мартин","Кажется, это тот случай, когда даже не совсем правильный перевод названия книги (в оригинале «Clean Code: A Handbook of Agile Software Craftsmanship»), вполне чётко отражает её содержимое. Роберт Мартин в своём творении, опираясь на личный опыт и, что даже важнее, конкретные примеры из своей практики, рассказывает о том, как нужно кодить. Принципиальное отличие от книги Макконелла заключается в том, что здесь очень мало статистических обоснований правильности тех или иных действий, только код (занимающий почти треть книги) и рекомендации автора.",
               0),
           new Book(2, "Программист-прагматик. Путь от подмастерья к мастеру","Эндрю Хант, Дэвид Томас","Ещё один представитель программистской литературы, где на трёхстах страницах методично описываются основные принципы создания качественного кода и условия, при которых вы будете получать удовольствие от работы, а клиент от результатов. Написана книга приятным языком, поэтому много времени на её освоение не уйдёт.",
               0)
       };
        */

        //Объявление структуры через которые будем запрашивать данные с БД
        public DbSet<Book> DBBooks { get; set; }
        public DbSet<Reader> DBReaders { get; set; }

        public ApplicationContext()
        {
            //Создание БД
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Library;Username=postgres;Password=1");
        }
    }

    public class Reader
    {
        public Reader(string Name)
        {
            this.Name = Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Book
    {
        public Book(string Name, string Author, string Info, Statuses Status)
        {
            this.Name = Name;
            this.Author = Author;
            this.Info = Info;
            this.Status = Status;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Info { get; set; }
        public Statuses Status { get; set; }

        public int? IdReader { get; set; }

    }
    public enum Statuses
    {
        InLibrary = 0,
        UseOnHands = 1,
        UseInLibrary = 2

    }
    public static class TestDB
    {
        public static void CreateDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.DBBooks.AddRange(
                new Book("Совершенный код", "Стив Макконнелл", "Если вы планируете построить успешную карьеру программиста, то это та книга, которую прочитать вы просто обязаны. Абсолютно неважно, в какой среде вы планируете работать, какой ваш уровень подготовки, новичок вы или уже руководитель – здесь найдётся полезная информация для каждого, кто хоть как-то связан с процессом создания кода. Совершенного кода.",
            0),
                new Book("Чистый код. Создание, анализ и рефакторинг", "Роберт К. Мартин", "Кажется, это тот случай, когда даже не совсем правильный перевод названия книги (в оригинале «Clean Code: A Handbook of Agile Software Craftsmanship»), вполне чётко отражает её содержимое. Роберт Мартин в своём творении, опираясь на личный опыт и, что даже важнее, конкретные примеры из своей практики, рассказывает о том, как нужно кодить. Принципиальное отличие от книги Макконелла заключается в том, что здесь очень мало статистических обоснований правильности тех или иных действий, только код (занимающий почти треть книги) и рекомендации автора.",
            0),
                new Book("Программист-прагматик. Путь от подмастерья к мастеру", "Эндрю Хант, Дэвид Томас", "Ещё один представитель программистской литературы, где на трёхстах страницах методично описываются основные принципы создания качественного кода и условия, при которых вы будете получать удовольствие от работы, а клиент от результатов. Написана книга приятным языком, поэтому много времени на её освоение не уйдёт.",
            0)
                );

                db.DBReaders.AddRange(
                    new Reader("Иван"),
                    new Reader("Василий"),
                    new Reader("Михаил")
                    );
                db.SaveChanges();
            }
        }
        public static List<Book> ReturnBooksAtStatus(Statuses status)
        {
            List<Book> ret = new List<Book>();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach(Book b in db.DBBooks.ToList())
                {
                    if (b.Status == status)
                        ret.Add(b);
                }
                    return ret;
            }
        }
        public static List<Reader> ReturnReaders()
        {
            List<Reader> ret = new List<Reader>();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (Reader r in db.DBReaders.ToList())
                {
                    ret.Add(r);
                }
                return ret;
            };
        }

        public static void GiveOnHand(int IdBook, int IdReader)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().Status = Statuses.UseOnHands;
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().IdReader = IdReader;
                db.SaveChanges();
            };
            
            //Для локальных данных
            //DBBooks.Find(b => b.Id == IdBook).Status = Statuses.UseOnHands;
            //DBBooks.Find(b => b.Id == IdBook).IdReader = IdReader;
        }

        public static void GiveInLibrary(int IdBook, int IdReader)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().Status = Statuses.UseInLibrary;
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().IdReader = IdReader;
                db.SaveChanges();
            };

            //Для локальных данных
            //DBBooks.Find(b => b.Id == IdBook).Status = Statuses.UseInLibrary;
            //DBBooks.Find(b => b.Id == IdBook).IdReader = IdReader;
        }
        public static void ReturnBook(int IdBook)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().Status = Statuses.InLibrary;
                db.DBBooks.FromSqlRaw("SELECT * FROM public.\"DBBooks\" WHERE \"Id\" = " + IdBook.ToString()).ToList().First().IdReader = null;
                db.SaveChanges();
            };

            //Для локальных данных
            //DBBooks.Find(b => b.Id == IdBook).Status = Statuses.InLibrary;
            //DBBooks.Find(b => b.Id == IdBook).IdReader = null;
        }
    }
}
