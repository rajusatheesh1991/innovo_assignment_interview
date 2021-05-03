
using InnovoAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Contracts.Persistence
{
    public interface IUserPreferencesRepository: IAsyncRepository<UserPreferences>
    {

        UserPreferences CreateOrUpdate(UserPreferences pref);

        UserPreferences GetByUserId(int id);

    }
}
