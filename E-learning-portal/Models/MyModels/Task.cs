using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}