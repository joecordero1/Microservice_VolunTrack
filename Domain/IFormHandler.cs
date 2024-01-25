using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Domain
{
    public interface IFormHandler
    {
        double Handle(int userId);
    }
}
