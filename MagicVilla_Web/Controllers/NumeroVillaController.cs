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

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel modelo)
		{
			if(ModelState.IsValid)
			{
				var response = await _numeroVillaService.Crear<APIResponse>(modelo.NumeroVilla);

				if(response != null && response.IsExitoso)
				{
                    TempData["exitoso"] = "Numero de Villa Creada Exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
				}
				else
				{
					if(response.ErrorMessages.Count > 0)
					{
						ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
					}
				}
			}

            var res = await _villaService.ObtenerTodos<APIResponse>();

            if (res != null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

            }

            return View(modelo);
		}
        #endregion
        #region Metodo Actualizar Numero de villa

        public async Task<IActionResult> ActualizarNumeroVilla(int villaNo)
        {
            NumeroVillaUpdateViewModel numeroVillaVM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo);

            if (response != null && response.IsExitoso)
            {
				NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
				numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(modelo);
            }

            response = await _villaService.ObtenerTodos<APIResponse>();

            if (response != null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });
				return View(numeroVillaVM);
            }
            return NotFound();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ActualizarNumeroVilla(NumeroVillaUpdateViewModel modelo)
		{
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<APIResponse>(modelo.NumeroVilla);

                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Número de Villa Actualizada Exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<APIResponse>();

            if (res != null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

            }

            return View(modelo);
        }
        #endregion

        #region Remover Numero Villa
        public async Task<IActionResult> RemoverNumeroVilla(int villaNo)
        {
            NumeroVillaDeleteViewModel numeroVillaVM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo);

            if (response != null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaVM.NumeroVilla = modelo;
            }

            response = await _villaService.ObtenerTodos<APIResponse>();

            if (response != null && response.IsExitoso)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });
                return View(numeroVillaVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
        {
            var response = await _numeroVillaService.Remover<APIResponse>(modelo.NumeroVilla.VillaNo);

            if(response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Numero de Villa Eliminada Exitosamente";
                return RedirectToAction(nameof(IndexNumeroVilla));
            }

            TempData["error"] = "Ocurrió un error";
            return View(modelo);
        }
        #endregion
    }
}

