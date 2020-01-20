using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplyingUniversityWeb.Models;
using ApplyingUniversityWeb.Services;
using Microsoft.EntityFrameworkCore;

namespace ApplyingUniversityWeb.Services

{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ProjectDatabaseContext _context;
        private readonly ConcurrentDictionary<int, Application> _application = new ConcurrentDictionary<int, Application>();

        public UniversityRepository(ProjectDatabaseContext context)
        {
            _context = context;
        }
        
      
        public async Task<IEnumerable<Users>> GetAllAsyncUser()
        {

            IEnumerable<Users> user = _context.Users.AsEnumerable();

            return await Task.FromResult(user.ToList());
        }

        public Task<Users> FindAsyncUser(int id)
        {
            // return _context.Users.FirstOrDefault(m => m.UserId == id);

            var user = _context.Users
                .FirstOrDefault(m => m.UserId == id);
            return Task.FromResult(user);

        }
        public Task<Users> FindAsyncUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(m => m.Username == username);
            return Task.FromResult(user);
        }
        public Task AddasyncUser(Users user)
        {
                _context.Add(user);
                _context.SaveChanges();
                return Task.CompletedTask;
        }
        public Task UpdateAsyncUser(Users user)
        {
            var beforeUpdate = _context.Users.Single(s => s.UserId == user.UserId);
            beforeUpdate.Username = user.Username;
            beforeUpdate.Password = user.Password;
            beforeUpdate.Roll = user.Roll;
            beforeUpdate.FirstName = user.FirstName;
            beforeUpdate.LastName = user.LastName;
            beforeUpdate.Email = user.Email;

            _context.Users.Update(beforeUpdate);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
      
        public Task<Users> RemoveAsyncUser(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return Task.FromResult(user);
        }


        public async Task<IEnumerable<Application>> GetAllAsyncApplication()
        {
            IEnumerable<Application> application = _context.Application
                       .AsEnumerable();
            return await Task.FromResult(application.ToList());
        }
        public Task<Application> FindAsyncApplication(int id)
        { 
                var application = _context.Application
                                .FirstOrDefault(m => m.ApplicationId == id);
                return Task.FromResult(application);   
        }
        
        public Task<Application> RemoveAsyncApplication(int id)
        {
            var application = _context.Application.Find(id);
            _context.Application.Remove(application);
            _context.SaveChanges();

            return Task.FromResult(application);
        }
        public Task UpdateAsyncApplication(Application application)
        {
            var beforeUpdate = _context.Application.Single(s => s.UserId == application.ApplicationId);
            beforeUpdate.UserId = application.UserId;
            beforeUpdate.UniversityId = application.UniversityId;
            beforeUpdate.AppliedDate = application.AppliedDate;
         
            _context.Application.Update(beforeUpdate);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddAsyncApplication(Application application)
        {
            _context.Add(application);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    

        public University FindAsyncUniversity(int id)
        {
            return _context.University.FirstOrDefault(m => m.UniversityId == id);
        }


        public async Task<IEnumerable<University>> GetAllAsyncUniversity()
        {
            
            IEnumerable<University> university = _context.University.AsEnumerable();
            
            return await Task.FromResult(university.ToList());
        }

        


        //public Task AddasyncApplication(Application application)
        //{
        //    _context.Add(application);
        //    _context.SaveChanges();
        //    return Task.CompletedTask;
        //}

        //public Application AddApplication(Application application)
        //{
        //    _application.Add(application.ApplicationId, application);
        //    return application;
        //}
    }
}
