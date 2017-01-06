using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CodeSnippetApplication.Models
{
    public class CodeSnippet
    {
        public virtual int Id { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "snippet name exceeds 200 characters"), MinLength(2, ErrorMessage = "snippet name must be at least 2 characters long")]
        [Display(Name = "Snippet Name")]
        public virtual string SnippetName { get; set; }
        [AllowHtml]
        [Required]
        [MinLength(4, ErrorMessage = "snippet code must be at least 4 characters long")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Snippet Code")]
        public virtual string SnippetCode { get; set; }
    }
}