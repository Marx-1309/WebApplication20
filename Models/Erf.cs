using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCDemo.Models
{
    [MetadataType(typeof(ErfMetaData))]
    public partial class User
    {
    }

    public class ErfMetaData
    {
        [Remote("IsErfNumberAvailable", "App_Erf",HttpMethod ="POST", ErrorMessage = "ErfNumber already in use.")]
        public string ErfComment { get; set; }
    }
}
