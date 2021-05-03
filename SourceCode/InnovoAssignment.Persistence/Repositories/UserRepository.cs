using InnovoAssignment.Application.Contracts.Persistence;
using InnovoAssignment.Domain.Entities;
using InnovoAssignment.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InnovoAssignment.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly InnovoAssignmentContext _innovoAssignmentContext;
        public UserRepository(InnovoAssignmentContext innovoAssignmentContext) : base(innovoAssignmentContext)
        {
            this._innovoAssignmentContext = innovoAssignmentContext;
        }

        public int AuthenticateWithEmailAndPassword(string email, string password)
        {
            int userId = 0;
            try
            {
                var returnVal = new SqlParameter("@RID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                
                var eid = new SqlParameter("@Email", email);

                var pw = new SqlParameter("@Password", password);

                _innovoAssignmentContext.Database.ExecuteSqlRaw(SqlConstants.USER_AUTHENTICATE_SP, eid, pw, returnVal);



                userId=(int)returnVal.Value;
            }
            catch (Exception e)
            {
                userId= - 1;
            }
            return userId;
        }

        public User GetByEmail(string email)
        {
            return _innovoAssignmentContext.Users.Where(a => a.Email == email).AsNoTracking().FirstOrDefault();
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _innovoAssignmentContext.Users.Where(a => a.Email == email&&a.Password==password).AsNoTracking().FirstOrDefault();
        }

        public User GetById(int id, bool includeAll)
        {
            if(includeAll)
            return _innovoAssignmentContext.Users.Include(a=>a.UserAddress).
                Include(a => a.UserPreferences).Where(a=>a.Id==id).AsNoTracking().FirstOrDefault();
            else
                return _innovoAssignmentContext.Users.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
        }

        public User SignupWithSp(User user)
        {


            try
            {
                var returnVal= new SqlParameter("@RID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var emailId = new SqlParameter("@Email", user.Email);
             
                var password = new SqlParameter("@Password", user.Password);
              
                _innovoAssignmentContext.Database.ExecuteSqlRaw(SqlConstants.USERS_SIGNUP_SP, emailId,  password,returnVal);

              
                user.Id = (int)returnVal.Value;
                return user;
            }
            catch (Exception e)
            {
                return null;
            }



            
        }

        public bool UpdateUserWithSp(User user)
        {
            try
            {
                var returnVal = new SqlParameter("@ROWCOUNT", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var phone = new SqlParameter("@Phone", user.PhoneNumber);
                var id = new SqlParameter("@Id", user.Id);
                var name = new SqlParameter("@Name", user.FullName);

                _innovoAssignmentContext.Database.ExecuteSqlRaw(SqlConstants.USERS_UPDATE_SP,id, phone, name, returnVal);


                 int count= (int)returnVal.Value;
                return count==1? true : false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
