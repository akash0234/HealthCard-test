using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCard.Entity.Models;
using HealthCard.Entity.ViewModels;

namespace HealthCard.BAL.Services
{
    public interface IUserService
    {
        public Task<UserVM> CreateAppUserAsync(User appUser);
       
    }
}
