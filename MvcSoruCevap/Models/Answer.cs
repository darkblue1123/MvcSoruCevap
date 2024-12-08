﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcSoruCevap.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

}
