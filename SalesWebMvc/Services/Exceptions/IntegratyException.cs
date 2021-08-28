using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.Exceptions
{
    public class IntegratyException : ApplicationException
    {
        public IntegratyException(string message) : base(message)
        {

        }
    } 
}
