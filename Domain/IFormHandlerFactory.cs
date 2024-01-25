using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Domain
{
    public interface IFormHandlerFactory
    {
        IFormHandler GetHandler(string formType);
    }
}
