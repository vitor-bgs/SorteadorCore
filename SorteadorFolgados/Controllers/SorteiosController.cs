using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.ViewModel;
using System.Collections.Generic;

namespace SorteadorFolgados.Controllers
{
    public class SorteiosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}