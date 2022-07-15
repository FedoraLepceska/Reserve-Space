using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reserve_Space.Controllers
{
    public class SpaceController : Controller
    {
        private readonly ISpaceService _spaceService;

        public SpaceController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }
        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var spaces = from s in _spaceService.GetAllSpaces()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                spaces = spaces.Where(s => s.name.Contains(searchString));
            }
            var allSpaces = this._spaceService.GetAllSpaces();
            return View(spaces.ToList());
        }

        [Authorize]
        public IActionResult ReserveSpace(Guid? id)
        {
            var model = this._spaceService.GetReservationDetails(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ReserveSpace([Bind("SpaceId", "Quantity")] ReserveSpaceDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._spaceService.Reserve(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Space");
            }

            return View(item);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var space = this._spaceService.GetDetailsForSpace(id);

            if (space == null)
            {
                return NotFound();
            }

            return View(space);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, name, price, image, address, DateFrom, DateTo, longitude, latitude")] Space space)
        {
            if (ModelState.IsValid)
            {
                this._spaceService.CreateNewSpace(space);

                return RedirectToAction(nameof(Index));
            }
            return View(space);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var space = this._spaceService.GetDetailsForSpace(id);
            if (space == null)
            {
                return NotFound();
            }
            return View(space);
        }
        private bool SpaceExists(Guid id)
        {
            return this._spaceService.GetDetailsForSpace(id) != null;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id, name, price, image, address, DateFrom, DateTo, longitude, latitude")] Space space)
        {
            if (id != space.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._spaceService.UpdateSpace(space);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceExists(space.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(space);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var space = this._spaceService.GetDetailsForSpace(id);

            if (space == null)
            {
                return NotFound();
            }

            return View(space);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            this._spaceService.DeleteSpace(id);

            return RedirectToAction(nameof(Index));
        }

        public override ViewResult View()
        {
            return base.View();
        }
    }
}
