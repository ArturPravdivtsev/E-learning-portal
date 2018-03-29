using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_learning_portal.Models.MyModels
{
    public class Group
    {
        [System.ComponentModel.DisplayName("Номер группы")]
        public int GroupId { get; set; }


        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Classbook> Classbooks { get; set; }

    }
}