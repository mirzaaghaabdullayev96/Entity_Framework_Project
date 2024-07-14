using LibraryManagementApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Business.Services.Interfaces
{
    public interface IGenericService<T>
    {
        void Create();
        T GetById(int id);
        void Delete();
        void GetAll();
        void Update();
    }
}
