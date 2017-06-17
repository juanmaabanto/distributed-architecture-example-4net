using CatSolution.Application.MainModule.Adapters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CatSolution.DistributedServices.WebApi.MainModule.Common
{
    public class Helper
    {
        public static Dictionary<string, string> GetClaims(ClaimsIdentity identity)
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();

            foreach (Claim claim in identity.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            return claims;
        }

        public static List<object> GetTreeList(IEnumerable<SYS_AuthorizationDTO> authorizations, short? parent)
        {
            List<object> childs = new List<object>();

            var list = from auths in authorizations
                       where auths.ParentId == parent
                       select new
                       {
                           OptionId = auths.OptionId,
                           Name = auths.OptionName,
                           IconCls = auths.Icon,
                           Leaf = auths.Leaf,
                           StartDate = auths.StartDate,
                           EndDate = auths.EndDate,
                           Allowed = auths.Allowed,
                           SYS_DetailAuthorization = auths.SYS_DetailAuthorization
                       };

            foreach (var item in list)
            {
                var m = new
                {
                    optionId = item.OptionId,
                    detailOptionId = 0,
                    name = item.Name,
                    iconCls = item.Leaf ? item.IconCls : null,
                    leaf = (item.Leaf && item.SYS_DetailAuthorization.Count > 0 ? false : item.Leaf),
                    startDate = item.StartDate,
                    endDate = item.EndDate,
                    allowed = item.Leaf ? item.Allowed : new bool?(),
                    children = item.Leaf ? (item.SYS_DetailAuthorization.Count > 0 ? GetDetailTreeList(item.SYS_DetailAuthorization) : null) : GetTreeList(authorizations, item.OptionId),
                    isEdit = item.Leaf
                };

                if (item.Leaf || m.children.Count > 0)
                {
                    childs.Add(m);
                }
            }

            return childs;
        }

        public static List<object> GetDetailTreeList(IEnumerable<SYS_DetailAuthorizationDTO> details)
        {
            List<object> childs = new List<object>();

            var list = from d in details
                       select new
                       {
                           OptionId = d.OptionId,
                           DetailOptionId = d.DetailOptionId,
                           Name = d.DetailName,
                           StartDate = d.StartDate,
                           EndDate = d.EndDate,
                           Allowed = d.Allowed
                       };

            foreach (var item in list)
            {
                var m = new
                {
                    optionId = item.OptionId,
                    detailOptionId = item.DetailOptionId,
                    name = item.Name,
                    iconCls = "x-fa fa-ticket",
                    leaf = true,
                    startDate = item.StartDate,
                    endDate = item.EndDate,
                    allowed = item.Allowed,
                    isEdit = true
                };

                childs.Add(m);
            }

            return childs;
        }
    }
}