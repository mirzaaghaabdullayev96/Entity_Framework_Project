using LibraryManagementApplication.Business.Services.Interfaces;
using LibraryManagementApplication.Core.Models;
using LibraryManagementApplication.Core.Repositories;
using LibraryManagementApplication.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Business.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        public AuthorService()
        {
            _authorRepository = new AuthorRepository();
        }
        public void Create()
        {
            Console.WriteLine("Write name of author");
            string name = Console.ReadLine();
            bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            while (!validName)
            {
                Console.WriteLine("Author name must be only letters and less than 100 characters. Write correctly");
                name = Console.ReadLine();
                validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
            }

            Console.WriteLine("New author was added to database successfully!");
            Thread.Sleep(800);
            _authorRepository.Insert(new Author() { Name = name });
            _authorRepository.Commit();
        }

        public void Delete()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of author to delete or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myAuthor = _authorRepository.GetById(Convert.ToInt32(idString));

                    if (myAuthor != null)
                    {
                        myAuthor.IsDeleted = true;
                        _authorRepository.Commit();
                        Console.WriteLine("Author was deleted");
                        Thread.Sleep(800);
                        return;
                    }
                    if (myAuthor == null)
                    {
                        Console.WriteLine("Author by this Id was not found, type the correct id or quit");
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
            var allAuthors = _authorRepository.GetAllWhere(x => x.IsDeleted == false).ToList();
            foreach (var author in allAuthors)
            {
                Console.WriteLine($"Id - {author.Id}, Author name - {author.Name}");
            }
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public void Update()
        {
        here:
            Console.Clear();
            GetAll();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Type id of author to update or Q for quitting");
                string idString = Console.ReadLine();
                if (idString.ToLower() == "q")
                    return;
                if (MyHelperClass.IdChecker(idString))
                {
                    var myAuthor = _authorRepository.GetById(Convert.ToInt32(idString));

                    if (myAuthor != null)
                    {
                        Console.WriteLine("Type new name for author");
                        string name = Console.ReadLine();
                        bool validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
                        while (!validName)
                        {
                            Console.WriteLine("Author name must be only letters and less than 100 characters. Write correctly");
                            name = Console.ReadLine();
                            validName = name.Length <= 100 && !string.IsNullOrEmpty(name) && name.All(char.IsLetter);
                        }
                        myAuthor.Name = name;
                        Console.WriteLine("Author name was updated successfully!");
                        Thread.Sleep(1000);
                        _authorRepository.Commit();
                        return;
                    }
                    if (myAuthor == null)
                    {
                        Console.WriteLine("Author by this Id was not found, type the correct id or quit");
                        Thread.Sleep(1200);
                        if (idString.ToLower() == "q")
                            return;
                        goto here;
                    }

                }
                Console.WriteLine("Id must be number");

            }
        }
    }
}
