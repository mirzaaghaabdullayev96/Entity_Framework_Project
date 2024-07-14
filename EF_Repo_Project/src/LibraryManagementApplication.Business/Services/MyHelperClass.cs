using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Business.Services
{
    public static class MyHelperClass
    {
        public static void MainChoices()
        {
            Console.WriteLine("""
                0. Exit
                1. Author actions
                2. Book actions
                3. Borrower actions
                4. Borrow book
                5. Return book
                6. Show most borrowed book
                7. Show late book returned borrowers
                8. Borrowers and their borrowed books
                9. Filter books by title
                10. Filter books by author
                """);
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
        }

        public static void AuthorActions()
        {
            Console.Clear();
            Console.WriteLine("""
                0. Exit
                1. Authors list
                2. Create author
                3. Update author
                4. Delete author
                """);
        }

        public static void BorrowerActions()
        {
            Console.Clear();
            Console.WriteLine("""
                0. Exit
                1. Borrower list
                2. Create borrower
                3. Update borrower
                4. Delete borrower
                """);
        }

        public static void BookActions()
        {
            Console.Clear();
            Console.WriteLine("""
                0. Exit
                1. Book list
                2. Create book
                3. Update book
                4. Delete book
                """);
        }

        public static void Quitting()
        {
            Console.WriteLine("Type Q for quitting");
            string quit = Console.ReadLine();
            while (quit.ToLower() != "q")
            {
                Console.WriteLine("You can quit by typing only Q");
                quit = Console.ReadLine();
            }
            if (quit.ToLower() == "q")
                return;
        }

        public static bool IdChecker(string id)
        {
            if (id.All(char.IsNumber))
                return true;
            return false;
        }

    }
}
