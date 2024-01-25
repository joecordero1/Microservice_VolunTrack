using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_VolunTrack.Infraestructure
{
    public interface IMessagePublisher
    {
        void PublishMessage(string message);
    }
}
