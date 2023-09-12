using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repositorio;
using Microsoft.AspNetCore.Mvc;


namespace MagicVilla_API.Controllers.v2
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NumeroVillaController : ControllerBase
    {
        #region Variables Globales
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        #endregion

        #region Constructor
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new APIResponse();
        }
        #endregion
        #region Método Get Obtener todas las villas
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }
        #endregion
    }
}
