using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_learning_portal.Models.MyModels
{
    public class Classbook
    {
        public int ClassbookId { get; set; }
        [System.ComponentModel.DisplayName("Задание")]
        public virtual Task Task { get; set; }
        [System.ComponentModel.DisplayName("Оценка")]
        public int Mark { get; set; }
        [System.ComponentModel.DisplayName("Дата")]
        DateTime Date { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}