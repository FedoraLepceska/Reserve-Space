using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IReservationService
    {
        ReservationDto GetReservationInfo(string UserId);
        bool DeleteSpaceFromReservation(string UserId, Guid id);
        bool Reserve(string UserId);
    }
}
