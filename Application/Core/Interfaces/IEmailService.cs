using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        bool Send(string from, string to, string subject, string html);
    }
}
