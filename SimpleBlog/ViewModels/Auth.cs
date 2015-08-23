using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/* 
 *  File contains all view models for controller Auth
 */
namespace SimpleBlog.ViewModels
{
    public class AuthLogin
    {
        [Required]
        public string UserName { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}