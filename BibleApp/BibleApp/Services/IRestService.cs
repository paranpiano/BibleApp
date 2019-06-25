using BibleApp.DatabaseAccess;
using BibleApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibleApp.Services
{
    public interface IRestService<T>
    {
        Task<T> CreateBibleVerseData(string id);
    }
}
