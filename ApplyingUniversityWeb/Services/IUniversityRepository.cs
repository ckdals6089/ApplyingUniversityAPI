using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplyingUniversityWeb.Models;
namespace ApplyingUniversityWeb.Services
{
     public interface IUniversityRepository
    {
        Task AddasyncUser(Users user);
        Task<Users> RemoveAsyncUser(int id);
        Task<IEnumerable<Users>> GetAllAsyncUser();
        Task<Users> FindAsyncUser(int id);
        Task<Users> FindAsyncUsername(string name);

        Task UpdateAsyncUser(Users user);

        Task<IEnumerable<University>> GetAllAsyncUniversity();
        //Task AddasyncApplication(Application application);
        Task AddAsyncApplication(Application application);

        Task<Application> RemoveAsyncApplication(int id);
        Task UpdateAsyncApplication (Application application);
        Task<IEnumerable<Application>> GetAllAsyncApplication();
        University FindAsyncUniversity(int id);
        Task<Application> FindAsyncApplication(int id);
        //Application AddApplication(Application application);
    }
}
