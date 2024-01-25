using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Infraestructure
{
    public interface IDatabaseManager
    {
        double CalculateAverage(int userId);
    }
}
