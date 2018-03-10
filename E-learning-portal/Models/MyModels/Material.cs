using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_learning_portal.Models.MyModels
{
    public class Material
    {
        public int MaterialId { get; set; }
        [System.ComponentModel.DisplayName("Название")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Предмет")]
        public string Subject { get; set; }
        [System.ComponentModel.DisplayName("Курс")]
        public int Course { get; set; }
        [System.ComponentModel.DisplayName("Кафедра")]
        public string Department { get; set; }
        [System.ComponentModel.DisplayName("Факультет")]
        public string Faculty { get; set; }
        [System.ComponentModel.DisplayName("Содержимое")]
        public byte[] Content { get; set; }
        [System.ComponentModel.DisplayName("Имя файла")]
        public string FileName { get; set; }
        [System.ComponentModel.DisplayName("Текст")]
        public string Fil { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}