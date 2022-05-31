using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Controller.Controllers
{
    class ApplicationAPIController : BaseController
    {

        /// <summary>
        /// /pets
        /// </summary>
        [HttpGet("/pets")]
        public void Get
        {

        }

        /// <summary>
        /// /pets/{petId}
        /// </summary>
        [HttpGet("/pets/{petId}")]
        public void Get
        {

        }

    }
}
