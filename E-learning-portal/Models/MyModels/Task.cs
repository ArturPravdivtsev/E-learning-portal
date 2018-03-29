using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning_portal.Models.MyModels
{
    public class Task
    {
        public int TaskId { get; set; }
        [System.ComponentModel.DisplayName("Название")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Предмет")]
        public string Subject { get; set; }
        [System.ComponentModel.DisplayName("Курс")]
        public int Course { get; set; }
        [System.ComponentModel.DisplayName("Содержимое")]
        public byte[] Content { get; set; }
        [AllowHtml]
        [System.ComponentModel.DisplayName("Текст")]
        public string Fil { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}