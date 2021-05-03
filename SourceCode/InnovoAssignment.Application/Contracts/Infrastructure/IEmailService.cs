using InnovoAssignment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {

        Task<bool> SendEmail(EmailDto email);
    }
}
