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
    public class NewspapersController : ApiController
    {
        private readonly NewspaperService _newspaperService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public NewspapersController(NewspaperService newspaperService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _newspaperService = newspaperService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/Newspapers/{id:int?}")]
        [HttpGet]
        public IHttpActionResult GetData(int? id = null)
        {
            var newspapersDto = _newspaperService.Get();
            if (id != null)
            {
                newspapersDto = _newspaperService.Get().Where(b => b.Id == id);
            }

            var newspapersForView = Mapper.Map<IEnumerable<NewspaperDTO>, List<NewspaperViewModel>>(newspapersDto);

            return Ok(newspapersForView);
        }

        [Route("api/Newspapers/GenreList")]
        [HttpGet]
        public IHttpActionResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(NewspaperTypeDTO));
            return Ok(genreList);
        }

        [Route("api/Newspapers")]
        [HttpPost]
        public IHttpActionResult Edit(NewspaperViewModel newspaperFromView)
        {
            if (newspaperFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<NewspaperViewModel, LibraryStorageUnitDTO>(newspaperFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var newspaperForEdit = Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperFromView);
                _newspaperService.Edit(newspaperForEdit);
            }
            catch (ObjectNotFoundException)
            {
                return BadRequest();
            }

            return Ok(newspaperFromView);
        }

        [Route("api/Newspapers")]
        [HttpPut]
        public IHttpActionResult Add(NewspaperViewModel newspaperFromView)
        {
            if (newspaperFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var libraryStorageUnitForAdd = Mapper.Map<NewspaperViewModel, LibraryStorageUnitDTO>(newspaperFromView);

            var newspaperForAdd = Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperFromView);
            newspaperForAdd.Unit = libraryStorageUnitForAdd;
            _newspaperService.Create(newspaperForAdd);

            return Ok(newspaperFromView);
        }

        [Route("api/Newspapers")]
        [HttpDelete]
        public IHttpActionResult Delete(NewspaperViewModel newspaperFromView)
        {
            var newspaperForDelete = _newspaperService.Get().FirstOrDefault(n => n.Id == newspaperFromView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == newspaperFromView.UnitId);

            if (newspaperForDelete == null || unitForDelete == null)
            {
                return BadRequest();
            }
            _newspaperService.Delete(newspaperForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);

            return Ok(newspaperFromView);
        }
    }
}