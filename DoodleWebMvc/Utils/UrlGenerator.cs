using DoodleWebMvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoodleWebMvc.Utils
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public UrlGenerator(IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
        }

        public string GetVotingPageUrl(int id)
        {
            string https = _httpContextAccessor.HttpContext.Request.Scheme;

            string hostAndPort = _httpContextAccessor.HttpContext.Request.Host.Value;

            string controllerAction = _urlHelper.Action(nameof(VoteController.Index), nameof(VoteController), new { id = id }).ToString().Replace("Controller", "");

            string output = CombineUrlParts(https, hostAndPort, controllerAction);

            return output;
        }

        private string CombineUrlParts(string https, string hostAndPort, string controllerAction)
        {
            return $@"{ https }://{ hostAndPort }{ controllerAction }";
        }
    }
}
