    using MediatR;
using NorthWind.UseCases.Common.Ports;
using NorthWind.UseCasesDTOs.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderInputPort : IInputport<CreateOrderParams, int>
    {

      
        public CreateOrderParams RequestData { get ; }

        public IOutputPort<int> OutputPort { get; }

        public CreateOrderInputPort(CreateOrderParams requestData ,
            IOutputPort<int> outputPort) => (RequestData, OutputPort) = 
            (requestData, outputPort);
        
    }
}
