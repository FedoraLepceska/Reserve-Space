using Domain.Dto;
using Domain.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<SpacesInOrder> _spacesInOrderRepository;

        public ReservationService(IRepository<Reservation> reservationRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<SpacesInOrder> spacesInOrderRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _spacesInOrderRepository = spacesInOrderRepository;
        }

        public bool DeleteSpaceFromReservation(string UserId, Guid id)
        {
            if (!string.IsNullOrEmpty(UserId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(UserId);

                var reservation = loggedInUser.Reservation;

                var spaceToDelete = reservation.ReservedSpaces
                    .Where(z => z.SpaceId.Equals(id))
                    .FirstOrDefault();

                reservation.ReservedSpaces.Remove(spaceToDelete);

                this._reservationRepository.Update(reservation);

                return true;
            }
            return false;
        }

        public ReservationDto GetReservationInfo(string UserId)
        {
            var loggedInUser = this._userRepository.Get(UserId);

            var reservation = loggedInUser.Reservation;

            var allSpaces = reservation.ReservedSpaces.ToList();


            var allSpacesPrice = allSpaces.Select(z => new
            {
                price = z.Space.price,
                quantity = z.Quantity,
                dateFrom = z.Space.DateFrom,
                dateTo = z.Space.DateTo
            }).ToList();

            double total = 0;

            foreach (var item in allSpacesPrice)
            {
                total += item.price * item.quantity;
            }


            ReservationDto reservationDto = new ReservationDto
            {
                ReservedSpaces = allSpaces,
                TotalPrice = (float)total
            };

            return reservationDto;
        }

        public bool Reserve(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {

                var loggedInUser = this._userRepository.Get(UserId);

                var reservation = loggedInUser.Reservation;

                //MAIL
                //EmailMessage message = new EmailMessage()
                //{
                //    MailTo = loggedInUser.Email,
                //    Subject = "Successfully created order",
                //    Status = false
                //};


                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    userId = UserId,
                    OrderedBy = loggedInUser
                };

                this._orderRepository.Insert(orderItem);

                List<SpacesInOrder> spaceInOrders = new List<SpacesInOrder>();

                var result = reservation.ReservedSpaces.Select(z => new SpacesInOrder
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderItem.Id,
                    SpaceId = z.Space.Id,
                    SelectedSpace = z.Space,
                    Quantity = z.Quantity,
                    Order = orderItem,
                    DateFrom = z.Space.DateFrom,
                    DateTo = z.Space.DateTo


                }).ToList();

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("Your order is completed. The order contains: ");

                double totalPrice = 0;
                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.SelectedSpace.price;
                    //sb.AppendLine(i.ToString() + ". " + item.SelectedSpace.name + " with a price of: " + item.SelectedSpace.price + "MKD and a quantity of: " + item.Quantity);
                }
                //sb.AppendLine("Your Total Price: " + totalPrice.ToString());

                //message.Content = sb.ToString();

                spaceInOrders.AddRange(result);

                foreach (var item in spaceInOrders)
                {
                    this._spacesInOrderRepository.Insert(item);
                }

                loggedInUser.Reservation.ReservedSpaces.Clear();

                //this._mailRepository.Insert(message);

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
