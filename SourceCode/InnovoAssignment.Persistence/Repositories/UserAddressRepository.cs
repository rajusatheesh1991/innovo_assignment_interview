using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Domain.Entities;
using InnovoAssignment.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InnovoAssignment.Persistence.Repositories
{
    public class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
    {
        private readonly InnovoAssignmentContext _innovoAssignmentContext;
        public UserAddressRepository(InnovoAssignmentContext innovoAssignmentContext) : base(innovoAssignmentContext)
        {
            this._innovoAssignmentContext = innovoAssignmentContext;
        }

        public UserAddress CreateOrUpdate(UserAddress address)
        {
            try
            {
                var returnVal = new SqlParameter("@RID", SqlDbType.Int) { Direction = ParameterDirection.Output };
               

                var action = new SqlParameter("@Action", address.Id==0?"INSERT":"UPDATE");
                var id = new SqlParameter("@Id", address.Id);

                var userId = new SqlParameter("@UserId", address.UserId);
                var addLine1 = new SqlParameter("@AddLine1", address.AddressLine1);
                var addLine2 = new SqlParameter("@AddLine2", address.AddressLine2);
                var city = new SqlParameter("@City", address.City);
                var state = new SqlParameter("@State", address.State);
                var zipCode = new SqlParameter("@ZipCode", address.ZipCode);
                var country = new SqlParameter("@Country", address.Country);
               
              

                _innovoAssignmentContext.Database.ExecuteSqlRaw(SqlConstants.ADDRESS_CREATE_UPDATE_SP,
                    action, id,userId, addLine1, addLine2, city, state, zipCode, country,returnVal);
                int count = (int)returnVal.Value;
                if (action.Equals("INSERT"))
                {
                    address.Id = count;
                }
                else
                {
                  
                    //count > 0 ? true : false;
                }



                return address;


            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UserAddress GetByUserId(int userId)
        {
            return _innovoAssignmentContext.UserAddresses.Where(a => a.UserId == userId).AsNoTracking().FirstOrDefault();
        }
    }
}
