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
    public class BrochuresController : ApiController
    {
        private readonly BrochureService _brochureService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public BrochuresController(BrochureService brochureService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _brochureService = brochureService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/Brochures/{id:int?}")]
        [HttpGet]
        public IHttpActionResult GetData(int? id = null)
        {
            var brochuresDto = _brochureService.Get();
            if (id != null)
            {
                brochuresDto = _brochureService.Get().Where(b => b.Id == id);
            }

            var brochuresForView = Mapper.Map<IEnumerable<BrochureDTO>, List<BrochureViewModel>>(brochuresDto);

            return Ok(brochuresForView);
        }

        [Route("api/Brochures/GenreList")]
        [HttpGet]
        public IHttpActionResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BrochureTypeDTO));
            return Ok(genreList);
        }

        [Route("api/Brochures")]
        [HttpPost]
        public IHttpActionResult Edit(BrochureViewModel brochureFromView)
        {
            if (brochureFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<BrochureViewModel, LibraryStorageUnitDTO>(brochureFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var brochureForEdit = Mapper.Map<BrochureViewModel, BrochureDTO>(brochureFromView);
                _brochureService.Edit(brochureForEdit);
            }
            catch (ObjectNotFoundException)
            {
                return BadRequest();
            }

            return Ok(brochureFromView);
        }

        [Route("api/Brochures")]
        [HttpPut]
        public IHttpActionResult Add(BrochureViewModel brochureFromView)
        {
            if (brochureFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var libraryStorageUnitForAdd = Mapper.Map<BrochureViewModel, LibraryStorageUnitDTO>(brochureFromView);

            var brochureForAdd = Mapper.Map<BrochureViewModel, BrochureDTO>(brochureFromView);
            brochureForAdd.Unit = libraryStorageUnitForAdd;
            _brochureService.Create(brochureForAdd);

            return Ok(brochureFromView);
        }

        [Route("api/Brochures")]
        [HttpDelete]
        public IHttpActionResult Delete(BrochureViewModel brochureFromView)
        {
            var brochureForDelete = _brochureService.Get().FirstOrDefault(b => b.Id == brochureFromView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == brochureFromView.UnitId);

            if (brochureForDelete == null || unitForDelete == null)
            {
                return BadRequest();
            }
            _brochureService.Delete(brochureForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);

            return Ok(brochureFromView);
        }
    }
}