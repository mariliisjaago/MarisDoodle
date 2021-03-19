using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoodleWebMvc.Utils.Contracts
{
    public interface IUrlGenerator
    {
        string GetVotingPageUrl(int id, IUrlHelper urlHelper, IHttpContextAccessor httpContextAccessor);
        string GetResultsPageUrl(int id, IUrlHelper url, IHttpContextAccessor httpContextAccessor);
    }
}