using APBD_Task4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task6.Services
{
    public interface IDbService
    {
        int GetStudentByIndex(string index);

    }
}
