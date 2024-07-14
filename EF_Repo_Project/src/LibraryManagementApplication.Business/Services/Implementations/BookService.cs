using LibraryManagementApplication.Business.Services.Interfaces;
using LibraryManagementApplication.Core.Models;
using LibraryManagementApplication.Core.Repositories;
using LibraryManagementApplication.Data.DAL;
using LibraryManagementApplication.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        public BookService()
        {
            _bookRepository = new BookRepository();
        }
        public void Create()
        {
            Random rnd = new Random();
            Console.WriteLine("Write name of Book");
            string name = Console.ReadLine();
            bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            while (!validName)
            {
                Console.WriteLine("Book name must be only letters and less than 100 characters. Write correctly");
                name = Console.ReadLine();
                validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            }
            Console.WriteLine("Write a little description");
            string description = Console.ReadLine();
            while (description.Length >= 700)
            {
                Console.WriteLine("Max length of description is 700 characters. Write new description");
                description = Console.ReadLine();
            }


            Console.WriteLine("New Book was added to database successfully!");
            Thread.Sleep(800);
            _bookRepository.Insert(new Book() { Title = name, Description = description, PublishedYear = rnd.Next(2005, 2020) });
            _bookRepository.Commit();
        }

        public void Delete()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of Book to delete or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBook = _bookRepository.GetById(Convert.ToInt32(idString));

                    if (myBook != null)
                    {
                        myBook.IsDeleted = true;
                        _bookRepository.Commit();
                        Console.WriteLine("Book was deleted");
                        Thread.Sleep(800);
                        return;
                    }
                    if (myBook == null)
                    {
                        Console.WriteLine("Book by this Id was not found, type the correct id or quit");
                        Thread.Sleep(1200);
                        if (idString.ToLower() == "q")
                            return;
                        goto here;
                    }

                }
                Console.WriteLine("Id must be number");

            }
        }

        public void GetAll()
        {
            var allBooks = _bookRepository.GetAllWhere(x => x.IsDeleted == false).ToList();
            foreach (var Book in allBooks)
            {
                Console.WriteLine($"Id - {Book.Id}, Book name - {Book.Title}, Description - {Book.Description}, Published year - {Book.PublishedYear}");
            }
        }
        public void GetAllAvailable()
        {
            var availableBook = _bookRepository.GetAll().Where(x => x.IsDeleted == false).Where(x => x.IsAvailable == true).ToList();
            foreach (var Book in availableBook)
            {
                Console.WriteLine($"Id - {Book.Id}, Book name - {Book.Title}, Description - {Book.Description}, Published year - {Book.PublishedYear}");
            }
        }


        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void Update()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of Book to update or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBook = _bookRepository.GetById(Convert.ToInt32(idString));

                    if (myBook != null)
                    {
                        while (true)
                        {

                            Console.WriteLine("What would you like to update? Title or description? Type T for title and D for description or Q for quitting");
                            string input = Console.ReadLine();
                            if (input.ToLower() == "q")
                                return;

                            switch (input.ToLower())
                            {
                                case "t":
                                    Console.WriteLine("Type new title for Book");
                                    string name = Console.ReadLine();
                                    bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name);
                                    while (!validName)
                                    {
                                        Console.WriteLine("Book title must be only letters and less than 100 characters. Write correctly");
                                        name = Console.ReadLine();
                                        validName = name.Length <= 100 && !string.IsNullOrEmpty(name);
                                    }
                                    myBook.Title = name;
                                    Console.WriteLine("Book title was updated successfully!");
                                    Thread.Sleep(1000);
                                    _bookRepository.Commit();
                                    return;
                                case "r":
                                    Console.WriteLine("Write a new little description");
                                    string description = Console.ReadLine();
                                    while (description.Length >= 700)
                                    {
                                        Console.WriteLine("Max length of description is 700 characters. Write new description");
                                        description = Console.ReadLine();
                                    }
                                    myBook.Description = description;
                                    Console.WriteLine("Book description was updated successfully!");
                                    Thread.Sleep(1000);
                                    _bookRepository.Commit();
                                    return;
                                default:
                                    Console.WriteLine("You should type T or D only or Q for quitting\"");
                                    break;
                            }
                        }
                    }
                    if (myBook == null)
                    {
                        Console.WriteLine("Book by this Id was not found, type the correct id or quit");
                        Thread.Sleep(1200);
                        if (idString.ToLower() == "q")
                            return;
                        goto here;
                    }

                }
                Console.WriteLine("Id must be number");

            }
        }

        public static void ShowMostBorrowedBook()
        {
            AppDbContext db = new AppDbContext();
            var mostBorrowedBook = db.Books
                    .OrderByDescending(b => b.BorrowedTimes)
                    .FirstOrDefault();
            Console.WriteLine("Most borrowed book is this one");
            Console.WriteLine($"Id - {mostBorrowedBook.Id}, Book name - {mostBorrowedBook.Title}, Description - {mostBorrowedBook.Description}, Published year - {mostBorrowedBook.PublishedYear}, Count - {mostBorrowedBook.BorrowedTimes}\n");
            Thread.Sleep(1000);
        }

        public static void ShowBookByTitle()
        {
            AppDbContext db = new AppDbContext();
            Console.WriteLine("Type title");
            string title = Console.ReadLine();
            var mostBorrowedBook = db.Books.FirstOrDefault(x => x.Title == title);
            if (mostBorrowedBook != null)
            {
                Console.WriteLine($"Id - {mostBorrowedBook.Id}, Book name - {mostBorrowedBook.Title}, Description - {mostBorrowedBook.Description}, Published year - {mostBorrowedBook.PublishedYear}, Count - {mostBorrowedBook.BorrowedTimes}\n");
            }

            if (mostBorrowedBook == null)
            {
                Console.WriteLine("There is no book with this title");
            }

            Console.WriteLine("Press any button to exit");
            string quit = Console.ReadLine();
            Thread.Sleep(1000);
        }

        public static void ShowBooksByAuthorName()
        {
            AppDbContext db = new AppDbContext();
            Console.WriteLine("Type author name");
            string title = Console.ReadLine();
            var booksByAuthor = db.Books
                    .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                    .Where(b => b.AuthorBooks.Any(ab => ab.Author.Name == title))
                    .ToList();

            if (booksByAuthor.Count>0)
            {
                foreach (var book in booksByAuthor)
                {
                    Console.WriteLine(book.Title);
                }
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("Author name is incorrect!!\n");
                Thread.Sleep(1500);
                return;
            }
        }
    }
}
