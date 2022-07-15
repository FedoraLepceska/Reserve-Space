using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ISpaceService
    {
        List<Space> GetAllSpaces();
        Space GetDetailsForSpace(Guid? id);
        void CreateNewSpace(Space space);
        void DeleteSpace(Guid? id);
        void UpdateSpace(Space space);
        ReserveSpaceDto GetReservationDetails(Guid? id);
        bool Reserve(ReserveSpaceDto item, string UserId);
    }
}
