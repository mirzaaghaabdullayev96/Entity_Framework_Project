using LibraryManagementApplication.Business.Services;
using LibraryManagementApplication.Business.Services.Implementations;
using LibraryManagementApplication.Business.Services.Interfaces;

namespace LibraryManagementApplication.UI
{
    internal class Program
    {
        static IAuthorService authorService = new AuthorService();
        static IBookService bookService = new BookService();
        static IBorrowerService borrowerService = new BorrowerService();
        static void Main(string[] args)
        {
            Console.WriteLine("====Welcome to our Library====\n");

            while (true)
            {
                MyHelperClass.MainChoices();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Library is closing ....Byeeee");
                        Thread.Sleep(2000);
                        return;
                    case "1":
                        AuthorActions();
                        break;
                    case "2":
                        BookActions();
                        break;
                    case "3":
                        BorrowerActions();
                        break;
                    case "4":
                        BorrowerService.BorrowBook();
                        break;
                    case "5":
                        BorrowerService.ReturnBooks();
                        break;
                    case "6":
                        BookService.ShowMostBorrowedBook();
                        break;
                    case "7":
                        BorrowerService.ShowAllLateReturnedBorrowers();
                        break;
                    case "8":
                        BorrowerService.ShowAllBorrowedBooks();
                        break;
                    case "9":
                        BookService.ShowBookByTitle();
                        break;
                    case "10":
                        BookService.ShowBooksByAuthorName();
                        break;
                    default:
                        Console.WriteLine("You must choose between 0 and 10");
                        break;
                }

            }
        }

        static void AuthorActions()
        {
            while (true)
            {
                MyHelperClass.AuthorActions();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        return;
                    case "1":
                        authorService.GetAll();
                        MyHelperClass.Quitting();
                        break;
                    case "2":
                        authorService.Create();
                        break;
                    case "3":
                        authorService.Update();
                        break;
                    case "4":
                        authorService.Delete();
                        break;
                    default:
                        Console.WriteLine("You must choose between 0 and 5");
                        Thread.Sleep(1000);
                        break;

                }
            }
        }
        static void BookActions()
        {
            while (true)
            {
                MyHelperClass.BookActions();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        return;
                    case "1":
                        bookService.GetAll();
                        MyHelperClass.Quitting();
                        break;
                    case "2":
                        bookService.Create();
                        break;
                    case "3":
                        bookService.Update();
                        break;
                    case "4":
                        bookService.Delete();
                        break;
                    default:
                        Console.WriteLine("You must choose between 0 and 5");
                        Thread.Sleep(1000);
                        break;

                }
            }
        }
        static void BorrowerActions()
        {
            while (true)
            {
                MyHelperClass.BorrowerActions();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.Clear();
                        return;
                    case "1":
                        borrowerService.GetAll();
                        MyHelperClass.Quitting();
                        break;
                    case "2":
                        borrowerService.Create();
                        break;
                    case "3":
                        borrowerService.Update();
                        break;
                    case "4":
                        borrowerService.Delete();
                        break;
                    default:
                        Console.WriteLine("You must choose between 0 and 5");
                        Thread.Sleep(1000);
                        break;

                }
            }
        }

    }
}
