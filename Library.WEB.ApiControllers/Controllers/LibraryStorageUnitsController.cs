using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.ApiControllers.Controllers
{
    public class LibraryStorageUnitsController : ApiController
    {
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public LibraryStorageUnitsController(LibraryStorageUnitService libraryStorageUnitService)
        {
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/LibraryStorageUnits")]
        [HttpGet]
        public IHttpActionResult GetData()
        {
            var libraryStorageUnitDto = _libraryStorageUnitService.Get();
            var libraryStorageUnitForView = Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(libraryStorageUnitDto);

            return Ok(libraryStorageUnitForView);
        }
    }
}