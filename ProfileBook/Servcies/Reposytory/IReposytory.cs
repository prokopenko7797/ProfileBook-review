using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Servcies.Repository
{
    public interface IRepository<T> where T : IModel, new()
    {
        Task<List<T>> GetAll();

        Task<T> FindWithQuery(string query);

        Task<List<T>> Query(string query);
        Task<T> GetById(int id);
        Task<int> Delete(int id);
        Task<int> Insert(T item);
        Task<int> Update(T item);
    }
}
