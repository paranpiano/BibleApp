using BibleApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibleApp.Services
{
    public interface IRestService<T>
    {
        Task<IEnumerable<T>> RefreshDataAsync(string id);

        //Task SaveTodoItemAsync(T item, bool isNewItem);
        //Task DeleteTodoItemAsync(string id);
    }
}
