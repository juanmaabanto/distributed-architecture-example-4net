using CatSolution.CrossCutting.Security.AuthenticationAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CatSolution.CrossCutting.Security.AuthenticationAPI
{
    /// <summary>
    /// Define métodos estáticos varios.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Convierte una identidad basada en notificaciones en un diccionario.
        /// </summary>
        /// <param name="identity">Identidad que se va a convertir.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetClaims(ClaimsIdentity identity)
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();

            foreach (Claim claim in identity.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            return claims;
        }

        /// <summary>
        /// Convierte un texto en su representación de cadena equivalente, que se códifica con dígitos de base 64.
        /// </summary>
        /// <param name="input">Texto que se va a convertir.</param>
        /// <returns></returns>
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        /// <summary>
        /// Convierte una entidad SYS_Option en una lista de objetos.
        /// </summary>
        /// <param name="options">Entidad que se va a convertir.</param>
        /// <param name="parent">Id de la entidad que se convertirá.</param>
        /// <returns></returns>
        public static List<object> GetTreeList(IEnumerable<SYS_Option> options, short? parent)
        {
            List<object> childs = new List<object>();

            var list = from opts in options
                       where opts.ParentId == parent
                       select new
                       {
                           OptionId = opts.OptionId,
                           Name = opts.Name,
                           Tooltip = opts.Tooltip,
                           ViewClass = opts.ViewClass,
                           ViewType = opts.ViewType,
                           IconCls = opts.Icon,
                           Leaf = opts.Leaf
                       };

            foreach (var item in list)
            {
                var m = new
                {
                    menu = item.OptionId,
                    text = item.Name,
                    qtip = item.Tooltip,
                    viewClass = item.ViewClass,
                    viewType = item.ViewType,
                    leaf = item.Leaf,
                    iconCls = item.IconCls,
                    children = item.Leaf ? null : GetTreeList(options, item.OptionId)
                };

                if (item.Leaf || m.children.Count > 0)
                {
                    childs.Add(m);
                }
            }

            return childs;
        }
    }
}