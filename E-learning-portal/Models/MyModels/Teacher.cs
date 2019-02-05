using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning_portal.Models.MyModels
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [System.ComponentModel.DisplayName("Фамилия")]
        public string Surname { get; set; }
        [System.ComponentModel.DisplayName("Имя")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [System.ComponentModel.DisplayName("Кафедра")]
        public string Department { get; set; }
        public string Id { get; set; }

        public virtual ICollection<Classbook> Classbooks { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public virtual IEnumerable<Task> Tasks { get; set; }
    }
}