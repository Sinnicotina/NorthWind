using MediatR;
using NorthWind.Entities.Enums;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderInteractor : AsyncRequestHandler<CreateOrderInputPort>
    {
        readonly IOrderRepository OrderRepository;

        readonly IOrderDetailRepository OrderDetailRepository;

        readonly IUnitOfWork UnitOfWork;
        public CreateOrderInteractor(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWor) =>
            (OrderRepository, OrderDetailRepository, UnitOfWork)
            = (orderRepository, orderDetailRepository, unitOfWor);
        protected override async Task Handle(CreateOrderInputPort request,
                CancellationToken cancellationToken)
        {
            Order Order = new Order()
            {
                CustomerId = request.RequestData.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = request.RequestData.ShipAddress,
                ShipCity = request.RequestData.ShipCity,
                ShipCountry = request.RequestData.ShipCountry,
                ShipPostaCode = request.RequestData.ShipPostalCode,
                ShippingType = Entities.Enums.ShippingType.Road,
                DiscountType = Entities.Enums.DiscountType.Percentage,
                Discount = 10
            };

            OrderRepository.Create(Order);
            foreach(var Item in request.RequestData.OrderDetails)
            {
                OrderDetailRepository.Create(new OrderDetail 
                {
                    Order=Order,
                    ProductId=Item.ProducId,
                    UnitPrice=Item.UnitPrice,
                    Quantity=Item.Quantity,
                });
            }
            try 
            {

                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new GeneralException("Error al crear la orden.",
                    ex.Message);
            }
            request.OutputPort.Handle(Order.Id);



        }
    }


}
