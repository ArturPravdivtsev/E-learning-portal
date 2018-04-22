using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Models.MyModels
{
    public class Material
    {
        public int MaterialId { get; set; }
        [System.ComponentModel.DisplayName("Название")]

        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Предмет")]
        [Required]
        public string Subject { get; set; }
        [System.ComponentModel.DisplayName("Курс")]
        [Required]
        public int Course { get; set; }
        [System.ComponentModel.DisplayName("Кафедра")]
        [Required]
        public string Department { get; set; }
        [System.ComponentModel.DisplayName("Факультет")]
        [Required]
        public string Faculty { get; set; }
        [AllowHtml]
        [System.ComponentModel.DisplayName("Содержимое")]
        public byte[] Content { get; set; }
        [System.ComponentModel.DisplayName("Имя файла")]
        public string FileName { get; set; }
        [AllowHtml]
        [System.ComponentModel.DisplayName("Текст")]
        public string Fil { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}