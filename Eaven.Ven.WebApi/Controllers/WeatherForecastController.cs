using Eaven.Ven.Application;
using Eaven.Ven.Core;
using Eaven.Ven.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eaven.Ven.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        IAppUserService _appUserService;
        public WeatherForecastController(IAppUserService appUserService, ILogger<WeatherForecastController> logger)
        {
            _appUserService = appUserService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResultModel<AppUser>> LoginAsync()
        {
            try
            {
                ResultModel<AppUser> resultModel = new ResultModel<AppUser>();
                string phone = "18986899148";
                string password = "E10ADC3949BA59ABBE56E057F20F883E";
                resultModel.data = await _appUserService.Login(phone, password);
                return resultModel;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }   /// <summary>
            /// 登录
            /// </summary>
            /// <param name="loginModel"></param>
            /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultModel<bool>> Modify(int appUserId)
        {
            try
            {
                ResultModel<bool> resultModel = new ResultModel<bool>();
                resultModel.data = await _appUserService.ModifyPassword(appUserId, "");
                return resultModel;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
    }
}
