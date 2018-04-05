using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.ApiControllers.Controllers
{
    public class AutorsController : ApiController
    {
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public AutorsController(AutorService autorService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/Autors")]
        [HttpGet]
        public IHttpActionResult GetData()
        {
            var autorsDto = _autorService.Get();
            var autorsForView = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);

            return Ok(autorsForView);
        }

        [Route("api/Autors/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetDetails(int id)
        {
            var libraryStorageUnitDto = _libraryStorageUnitService.Get().Where(u => u.AutorId == id);
            var libraryStorageUnitForView = Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(libraryStorageUnitDto);

            return Ok(libraryStorageUnitForView);
        }

        [Route("api/Autors")]
        [HttpPost]
        public IHttpActionResult Edit(AutorViewModel autorFromView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _autorService.Edit(Mapper.Map<AutorViewModel, AutorDTO>(autorFromView));
            return Ok(autorFromView);
        }

        [Route("api/Autors")]
        [HttpPut]
        public IHttpActionResult Add(AutorViewModel autorFromView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _autorService.Create(Mapper.Map<AutorViewModel, AutorDTO>(autorFromView));
            return Ok(autorFromView);
        }

        [Route("api/Autors")]
        [HttpDelete]
        public IHttpActionResult Delete(AutorViewModel autorFromView)
        {
            var autorForDelete = _autorService.GetWithInclude(autorFromView.Id);

            if (autorForDelete == null)
            {
                return BadRequest();
            }
            _autorService.Delete(autorForDelete);
            return Ok(autorFromView);
        }
    }
}