using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Models.MyModels
{
    public class Task
    {
        public int TaskId { get; set; }
        [System.ComponentModel.DisplayName("Название")]
        [Required]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Предмет")]
        [Required]
        public string Subject { get; set; }
        [System.ComponentModel.DisplayName("Курс")]
        [Required]
        public int Course { get; set; }
        [System.ComponentModel.DisplayName("Содержимое")]
        public byte[] Content { get; set; }
        [AllowHtml]
        [Required]
        [System.ComponentModel.DisplayName("Текст")]
        public string Fil { get; set; }
        [Required]
        public bool done { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}