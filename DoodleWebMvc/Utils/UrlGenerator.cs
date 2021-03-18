using DoodleWebMvc.Controllers;
using DoodleWebMvc.Utils.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoodleWebMvc.Utils
{
    public class UrlGenerator : IUrlGenerator
    {

        public UrlGenerator()
        {

        }

        public string GetVotingPageUrl(int id, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor)
        {
            string https = httpContextAccessor.HttpContext.Request.Scheme;

            string hostAndPort = httpContextAccessor.HttpContext.Request.Host.Value;

            string controllerAction = urlHelper.Action(nameof(VoteController.Index), nameof(VoteController), new { id = id }).ToString().Replace("Controller", "");

            string output = CombineUrlParts(https, hostAndPort, controllerAction);

            return output;
        }

        private string CombineUrlParts(string https, string hostAndPort, string controllerAction)
        {
            return $@"{ https }://{ hostAndPort }{ controllerAction }";
        }
    }
}
