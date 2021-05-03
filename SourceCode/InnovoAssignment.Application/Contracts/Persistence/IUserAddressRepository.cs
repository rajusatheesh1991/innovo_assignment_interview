using InnovoAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Application.Contracts.Persistence
{
    public interface IUserAddressRepository:IAsyncRepository<UserAddress>
    {

        UserAddress CreateOrUpdate(UserAddress address);
        UserAddress GetByUserId(int userId);
    }
}
