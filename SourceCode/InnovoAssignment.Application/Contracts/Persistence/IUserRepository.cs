using InnovoAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Contracts.Persistence
{
    public interface IUserRepository:IAsyncRepository<User>
    {

        User SignupWithSp(User user);

        bool UpdateUserWithSp(User user);

        User GetByEmail(string email);

        User GetById(int id,bool includeAll);

        User GetByEmailAndPassword(string email,string password);

    }
}
