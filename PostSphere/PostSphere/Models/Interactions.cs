using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostSphere.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O texto é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O texto deve ter no máximo 500 caracteres.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<Reply> Replies { get; set; } = new List<Reply>();
    }

    public class Reply
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O texto é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O texto deve ter no máximo 500 caracteres.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CommentId { get; set; }
    }
}
