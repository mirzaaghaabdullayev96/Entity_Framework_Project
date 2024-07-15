using LibraryManagementApplication.Business.Services.Interfaces;
using LibraryManagementApplication.Core.Models;
using LibraryManagementApplication.Core.Repositories;
using LibraryManagementApplication.Data.DAL;
using LibraryManagementApplication.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Business.Services.Implementations
{
    public class BorrowerService : IBorrowerService
    {
        private IBorrowerRepository _borrowerRepository;
        private IBookRepository _bookRepository;
        public BorrowerService()
        {
            _bookRepository = new BookRepository();
            _borrowerRepository = new BorrowerRepository();
        }
        public void Create()
        {
            Console.WriteLine("Write name of Borrower");
            string name = Console.ReadLine();
            bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            while (!validName)
            {
                Console.WriteLine("Borrower name must be only letters and less than 100 characters. Write correctly");
                name = Console.ReadLine();
                validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            }

            Console.WriteLine("Write an email");
            string email = Console.ReadLine();
            while (!email.Contains("@") || email.Length >= 150)
            {
                Console.WriteLine("Email must contain @ and max length is 150. Write new email");
                email = Console.ReadLine();
            }
            Console.WriteLine("New Borrower was added to database successfully!");
            Thread.Sleep(800);
            _borrowerRepository.Insert(new Borrower() { Name = name, Email = email });
            _borrowerRepository.Commit();
        }

        public void Delete()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of Borrower to delete or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBorrower = _borrowerRepository.GetById(Convert.ToInt32(idString));

                    if (myBorrower != null)
                    {
                        myBorrower.IsDeleted = true;
                        _borrowerRepository.Commit();
                        Console.WriteLine("Borrower was deleted");
                        Thread.Sleep(800);
                        return;
                    }
                    if (myBorrower == null)
                    {
                        Console.WriteLine("Borrower by this Id was not found, type the correct id or quit");
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
            var allBorrowers = _borrowerRepository.GetAllWhere(x => x.IsDeleted == false).ToList();
            foreach (var Borrower in allBorrowers)
            {
                Console.WriteLine($"Id - {Borrower.Id}, Borrower name - {Borrower.Name}, Email - {Borrower.Email}");
            }
        }

        public Borrower GetById(int id)
        {
            return _borrowerRepository.GetById(id);
        }

        public void Update()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of borrower to update or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBook = _borrowerRepository.GetById(Convert.ToInt32(idString));

                    if (myBook != null)
                    {
                        while (true)
                        {

                            Console.WriteLine("What would you like to update? Name or email? Type N for name and E for email or Q for quitting");
                            string input = Console.ReadLine();
                            if (input.ToLower() == "q")
                                return;

                            switch (input.ToLower())
                            {
                                case "n":
                                    Console.WriteLine("Type new name for borrower");
                                    string name = Console.ReadLine();
                                    bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name);
                                    while (!validName)
                                    {
                                        Console.WriteLine("Borrower name must be only letters and less than 100 characters. Write correctly");
                                        name = Console.ReadLine();
                                        validName = name.Length <= 100 && !string.IsNullOrEmpty(name);
                                    }
                                    myBook.Name = name;
                                    Console.WriteLine("Borrower name was updated successfully!");
                                    Thread.Sleep(1000);
                                    _borrowerRepository.Commit();
                                    return;
                                case "e":
                                    Console.WriteLine("Write a new email");
                                    string email = Console.ReadLine();
                                    while (email.Length >= 700)
                                    {
                                        Console.WriteLine("Max length of email is 150 characters. Write new email");
                                        email = Console.ReadLine();
                                    }
                                    myBook.Email = email;
                                    Console.WriteLine("Borrower email was updated successfully!");
                                    Thread.Sleep(1000);
                                    _borrowerRepository.Commit();
                                    return;
                                default:
                                    Console.WriteLine("You should type T or D only or Q for quitting");
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

        public static void BorrowBook()
        {
            BookService bookService = new BookService();
            AppDbContext appDbContext = new AppDbContext();
        here:
            Console.Clear();
            var allBorrowers = appDbContext.Borrowers.Where(x => x.IsDeleted == false).ToList();
            foreach (var Borrower in allBorrowers)
            {
                Console.WriteLine($"Id - {Borrower.Id}, Borrower name - {Borrower.Name}, Email - {Borrower.Email}");
            }
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of borrower or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBorrower = appDbContext.Borrowers.Find(Convert.ToInt32(idString));

                    if (myBorrower != null)
                    {
                        Loan myLoan = new Loan() { Borrower = myBorrower };
                    //appDbContext.Loans.Add(new Loan() { BorrowerId = myBorrower.Id });
                    //appDbContext.SaveChanges();
                    Book:
                        while (true)
                        {

                            var availableBook = appDbContext.Books.Where(x => x.IsDeleted == false).ToList().Where(x => x.IsAvailable == true);
                            List<LoanItem> loanItems = new List<LoanItem>();
                        ListOfBooks:
                            Console.WriteLine("Choose a book to borrow by id or Q for quitting");
                            foreach (var book in availableBook)
                            {
                                Console.WriteLine($"Id - {book.Id}, Book name - {book.Title}, Description - {book.Description}, Published year - {book.PublishedYear}");
                            }

                            string input = Console.ReadLine();
                            if (input.ToLower() == "q")
                                return;
                            if (MyHelperClass.IdChecker(input))
                            {
                                var myBook = appDbContext.Books.Find(Convert.ToInt32(input));
                                if (myBook != null && myBook.IsAvailable == true)
                                {
                                    myBook.IsAvailable = false;
                                    myBook.BorrowedTimes++;
                                    myBook.Borrower = myBorrower;
                                    myBook.BorrowerId = myBorrower.Id;
                                    loanItems.Add(new LoanItem() { BookId = myBook.Id });
                                    //var loan = appDbContext.Loans.OrderBy(x => x.Id).LastOrDefault();
                                    //appDbContext.LoanItems.Add(new LoanItem() { BookId = myBook.Id, LoanId = loan.Id });

                                }
                                else
                                {
                                    Console.WriteLine("Book by this Id was not found, type the correct id or quit");
                                    Thread.Sleep(1200);
                                    goto Book;
                                }

                                while (true)
                                {
                                    Console.WriteLine("Do you want to loan another book?. Type Yes or No");
                                    string answer = Console.ReadLine();
                                    if (answer.ToLower() == "yes")
                                    {
                                        goto ListOfBooks;
                                    }
                                    if (answer.ToLower() == "no")
                                    {
                                        myLoan.LoanItems = loanItems;
                                        appDbContext.Loans.Add(myLoan);
                                        foreach (var item in loanItems)
                                        {
                                            appDbContext.LoanItems.Add(item);
                                        }
                                        appDbContext.SaveChanges();
                                        Console.WriteLine("Books were loaned");
                                        Thread.Sleep(1000);
                                        return;
                                    }
                                }


                            }

                        }

                    }

                    if (myBorrower == null)
                    {
                        Console.WriteLine("Borrower by this Id was not found, type the correct id or quit");
                        Thread.Sleep(1200);
                        if (idString.ToLower() == "q")
                            return;
                        goto here;
                    }

                }
                Console.WriteLine("Id must be number");

            }

        }

        public static void ShowAllLateReturnedBorrowers()
        {
            AppDbContext appDbContext = new AppDbContext();
            var lateBorrowers = appDbContext.LateReturnedBorrowers.ToList();
            if (lateBorrowers.Count > 0)
            {
                foreach (var late in lateBorrowers)
                {
                    Console.WriteLine($"Id - {late.Id}, Name - {late.Name}, Email - {late.Email}");
                }

                Thread.Sleep(1200);
            }

            if (lateBorrowers.Count == 0)
                Console.WriteLine("The list is empty!");
            Thread.Sleep(1200);
        }

        public static void ReturnBooks()
        {
            Random rnd = new Random();
            IBookService bookService = new BookService();
            AppDbContext appDbContext = new AppDbContext();

            Console.Clear();
            var allBorrowers = appDbContext.Borrowers.Where(x => x.Books.Count > 0).ToList();
            if (allBorrowers.Count == 0)
            {
                Console.WriteLine("No borrowers exist with book");
                Thread.Sleep(1000);
                return;
            }

            foreach (var Borrower in allBorrowers)
            {
                Console.WriteLine($"Id - {Borrower.Id}, Borrower name - {Borrower.Name}, Email - {Borrower.Email}");
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose borrower by Id to return books or quit pressing Q");
                string idString = Console.ReadLine();

                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myBorrower = appDbContext.Borrowers.Where(x => x.Books.Count > 0).FirstOrDefault(x => x.Id == Convert.ToInt32(idString));

                    if (myBorrower != null)
                    {

                        foreach (var book in appDbContext.Books.Where(x => x.BorrowerId == myBorrower.Id))
                        {
                            book.IsAvailable = true;
                            book.BorrowerId = null;
                        }
                        var myLoan = appDbContext.Loans.Where(x => x.BorrowerId == myBorrower.Id).FirstOrDefault();
                        if (myLoan is null)
                        {
                            Console.WriteLine("No loans were created!");
                            break;
                        }
                        myLoan.ReturnDate = DateTime.UtcNow.AddDays(rnd.Next(13, 18));
                        if (myLoan.ReturnDate > myLoan.MustReturnDate)
                        {
                            appDbContext.LateReturnedBorrowers.Add(new LateReturnedBorrower() { Name = myBorrower.Name, Email = myBorrower.Email });
                        }

                        appDbContext.SaveChanges();
                        Console.WriteLine($"Borrower by Id - {myBorrower.Id} has returned all books {(myLoan.ReturnDate > myLoan.MustReturnDate ? "late" : "in time")}");

                        Thread.Sleep(1500);
                        return;
                    }


                }
                if (idString.Any(char.IsLetter))
                {
                    Console.WriteLine("Id must be only numbers");
                    continue;
                }
                Console.WriteLine("Borrower by this id was not found in the list");
            }

        }

        public static void ShowAllBorrowedBooks()
        {
            IBookService bookService = new BookService();
            using (AppDbContext appDbContext = new AppDbContext())
            {
                var borrowerBooks = appDbContext.Loans
               .Include(x => x.LoanItems)
               .ThenInclude(x => x.Book)
               .Include(x => x.Borrower)
               .ToList();

                if (borrowerBooks.Count == 0)
                {
                    Console.WriteLine("List is empty");
                    Thread.Sleep(1200);
                }

                foreach (var borrower in borrowerBooks)
                {
                    foreach (var item in borrower.LoanItems)
                    {
                        var book = item.Book;
                        Console.WriteLine(book.Title + " was borrowed by " + borrower.Borrower.Name);

                    }
                }
                Console.WriteLine();
                Thread.Sleep(800);
            }
        }
    }
}
