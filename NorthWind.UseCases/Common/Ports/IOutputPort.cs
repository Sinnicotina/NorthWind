using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCases.Common.Ports
{
    public interface IOutputPort<InteractorResponseType>
    {
        void Handle(InteractorResponseType response);
    }
}
