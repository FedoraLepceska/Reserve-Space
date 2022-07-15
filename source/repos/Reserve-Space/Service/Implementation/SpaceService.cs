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
    public class SpaceService : ISpaceService
    {
        private readonly IRepository<Space> _spaceRepository;
        private readonly IRepository<ReservedSpaces> _reservedSpaces;
        private readonly IUserRepository _userRepository;

        public SpaceService(IRepository<Space> spaceRepository, IRepository<ReservedSpaces> reservedSpaces, IUserRepository userRepository)
        {
            _spaceRepository = spaceRepository;
            _reservedSpaces = reservedSpaces;
            _userRepository = userRepository;
        }
        public void CreateNewSpace(Space space)
        {
            this._spaceRepository.Insert(space);
        }

        public void DeleteSpace(Guid? id)
        {
            var space = this.GetDetailsForSpace(id);
            this._spaceRepository.Delete(space);
        }

        public List<Space> GetAllSpaces()
        {
            return this._spaceRepository.GetAll().ToList();
        }

        public Space GetDetailsForSpace(Guid? id)
        {
            return this._spaceRepository.Get(id);
        }

        public ReserveSpaceDto GetReservationDetails(Guid? id)
        {
            var space = this.GetDetailsForSpace(id);
            ReserveSpaceDto model = new ReserveSpaceDto
            {
                SelectedSpace = space,
                SpaceId = space.Id,
                Quantity = 1,
                DateFrom = space.DateFrom,
                DateTo = space.DateTo
            };
            return model;
        }

        public bool Reserve(ReserveSpaceDto item, string UserId)
        {
            var user = this._userRepository.Get(UserId);

            var reservation = user.Reservation;

            if (item.SpaceId != null && reservation != null)
            {
                var space = this.GetDetailsForSpace(item.SpaceId);

                if (space != null)
                {
                    ReservedSpaces spaceToAdd = new ReservedSpaces
                    {
                        Id = Guid.NewGuid(),
                        Space = space,
                        SpaceId = space.Id,
                        Reservation = reservation,
                        ReservationId = reservation.Id,
                        Quantity = item.Quantity
                    };

                    this._reservedSpaces.Insert(spaceToAdd);
                    return true;
                }

                return false;
            }
            return false;
        }

        public void UpdateSpace(Space space)
        {
            this._spaceRepository.Update(space);
        }
    }
}
