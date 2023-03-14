using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("questionId")]
        public int QueId { get; set; }
        public Question questionId { get; set; }

        [ForeignKey("testId")]
        public int TestId { get; set; }
        public Test testId { get; set; }


    }
}
