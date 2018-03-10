using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_learning_portal.Models.MyModels
{
    public class Student
    {
        public int StudentId { get; set; }
        [System.ComponentModel.DisplayName("Фамилия")]
        public string Surname { get; set; }
        [System.ComponentModel.DisplayName("Имя")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Номер Группы")]
        public string GroupNumber { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Classbook> Classbooks { get; set; }
    }
}