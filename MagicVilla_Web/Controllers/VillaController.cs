﻿using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _mapper = mapper;
            _villaService = villaService; 
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> villaList = new();
            var response = await _villaService.ObtenerTodos<APIResponse>();
            
            if(response != null && response.IsExitoso)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado));
            }
            return View(villaList);
        }
    }
}