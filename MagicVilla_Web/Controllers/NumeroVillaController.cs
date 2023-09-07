using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModel;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
	public class NumeroVillaController : Controller
	{
        #region variables
        private readonly INumeroVillaService _numeroVillaService;
		private readonly IVillaService _villaService;
		private readonly IMapper _mapper;
        #endregion
        #region constructor
        public NumeroVillaController(INumeroVillaService numeroVillaService, IVillaService villaService, IMapper mapper)
		{
			_numeroVillaService = numeroVillaService;
			_villaService = villaService;
			_mapper = mapper;
		}
        #endregion
        #region Obtener todos
        public async Task<IActionResult> IndexNumeroVilla()
		{
			List<NumeroVillaDto> numeroVillaList = new();
			var response = await _numeroVillaService.ObtenerTodos<APIResponse>();

			if(response != null && response.IsExitoso)
			{
				numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));
            }
			return View(numeroVillaList);
		}
		#endregion
		#region Crear Numero de villa
		public async Task<IActionResult> CrearNumeroVilla()
		{
			NumeroVillaViewModel numeroVillaVM = new();
			var response = await _villaService.ObtenerTodos<APIResponse>();

			if(response != null && response.IsExitoso)
			{
				numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado))
											.Select(v => new SelectListItem
											{
												Text = v.Nombre,
												Value = v.Id.ToString()
											});

            }
			return View(numeroVillaVM);
		}

        #endregion
    }
}

