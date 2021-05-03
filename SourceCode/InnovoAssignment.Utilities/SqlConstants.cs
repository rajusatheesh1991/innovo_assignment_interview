using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Utilities
{
    public class SqlConstants
    {

        public const string USERS_SIGNUP_SP = "EXEC Users_Signup @email, @password,@RID OUTPUT";

        public const string USERS_UPDATE_SP = "EXEC Users_Update @Id, @Name,@Phone,@ROWCOUNT OUTPUT";

        public const string ADDRESS_CREATE_UPDATE_SP = "EXEC Address_Create_Update @Action, @Id,@UserId, @AddLine1,@AddLine2,@City,@State,@ZipCode,@Country,@RID OUTPUT";

        public const string PREF_CREATE_UPDATE_SP = "EXEC Pref_Create_Update @Action, @Id,@UserId, @Enable,@RID OUTPUT";


        
    }
}
