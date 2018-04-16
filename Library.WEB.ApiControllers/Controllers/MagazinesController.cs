using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.ApiControllers.Controllers
{
    public class MagazinesController : ApiController
    {
        private readonly MagazineService _magazineService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public MagazinesController(MagazineService magazineService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _magazineService = magazineService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/Magazines/{id:int?}")]
        [HttpGet]
        public IHttpActionResult GetData(int? id = null)
        {
            var magazinesDto = _magazineService.Get();
            if (id != null)
            {
                magazinesDto = _magazineService.Get().Where(b => b.Id == id);
            }

            var magazinesForView = Mapper.Map<IEnumerable<MagazineDTO>, List<MagazineViewModel>>(magazinesDto);

            return Ok(magazinesForView);
        }

        [Route("api/Magazines/GenreList")]
        [HttpGet]
        public IHttpActionResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(StylesOfPublicationsDTO));
            return Ok(genreList);
        }

        [Route("api/Magazines")]
        [HttpPost]
        public IHttpActionResult Edit(MagazineViewModel magazineFromView)
        {
            if (magazineFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<MagazineViewModel, LibraryStorageUnitDTO>(magazineFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var magazineForEdit = Mapper.Map<MagazineViewModel, MagazineDTO>(magazineFromView);
                _magazineService.Edit(magazineForEdit);
            }
            catch (ObjectNotFoundException)
            {
                return BadRequest();
            }

            return Ok(magazineFromView);
        }

        [Route("api/Magazines")]
        [HttpPut]
        public IHttpActionResult Add(MagazineViewModel magazineFromView)
        {
            if (magazineFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var libraryStorageUnitForAdd = Mapper.Map<MagazineViewModel, LibraryStorageUnitDTO>(magazineFromView);

            var magazineForAdd = Mapper.Map<MagazineViewModel, MagazineDTO>(magazineFromView);
            magazineForAdd.Unit = libraryStorageUnitForAdd;
            _magazineService.Create(magazineForAdd);

            return Ok(magazineFromView);
        }

        [Route("api/Magazines")]
        [HttpDelete]
        public IHttpActionResult Delete(MagazineViewModel magazineFromView)
        {
            var magazineForDelete = _magazineService.Get().FirstOrDefault(m => m.Id == magazineFromView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == magazineFromView.UnitId);

            if (magazineForDelete == null || unitForDelete == null)
            {
                return BadRequest();
            }
            _magazineService.Delete(magazineForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);

            return Ok(magazineFromView);
        }
    }
}