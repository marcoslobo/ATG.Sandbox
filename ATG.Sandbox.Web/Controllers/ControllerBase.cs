using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATG.Sandbox.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public string GetModelErrors()
        {
            var errorReturn = new List<string>();

            errorReturn.Add("Erro de validação, verifique as mensagens");

            foreach (ModelStateEntry modelState in ViewData.ModelState.Values)
            {

                foreach (ModelError error in modelState.Errors)
                {
                    errorReturn.Add(error.ErrorMessage);

                }
            }
            string delimiter = "<br />";
            return errorReturn.Select(i => i).Aggregate((i, j) => i + delimiter + j);
            
        }
    }
}
