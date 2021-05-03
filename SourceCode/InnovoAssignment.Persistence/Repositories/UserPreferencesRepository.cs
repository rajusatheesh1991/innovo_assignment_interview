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
    public class UserPreferencesRepository : BaseRepository<UserPreferences>, IUserPreferencesRepository
    {
        private readonly InnovoAssignmentContext _innovoAssignmentContext;
        public UserPreferencesRepository(InnovoAssignmentContext innovoAssignmentContext) : base(innovoAssignmentContext)
        {
            this._innovoAssignmentContext = innovoAssignmentContext;
        }

        public UserPreferences CreateOrUpdate(UserPreferences pref)
        {
            try
            {
                var returnVal = new SqlParameter("@RID", SqlDbType.Int) { Direction = ParameterDirection.Output };


                var action = new SqlParameter("@Action", pref.Id == 0 ? "INSERT" : "UPDATE");
                var id = new SqlParameter("@Id", pref.Id);

                var userId = new SqlParameter("@UserId", pref.UserId);
                var enable = new SqlParameter("@Enable", pref.EnablePromotionNotifications);
                


                _innovoAssignmentContext.Database.ExecuteSqlRaw(SqlConstants.PREF_CREATE_UPDATE_SP,
                    action, id, userId, enable, returnVal);
                int count = (int)returnVal.Value;
                if (action.Equals("INSERT"))
                {
                    pref.Id = count;
                }
                else
                {

                    //count > 0 ? true : false;
                }



                return pref;


            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UserPreferences GetByUserId(int userId)
        {
            return _innovoAssignmentContext.UserPreferences.Where(a => a.UserId == userId).AsNoTracking().FirstOrDefault();
        }
    }
}
