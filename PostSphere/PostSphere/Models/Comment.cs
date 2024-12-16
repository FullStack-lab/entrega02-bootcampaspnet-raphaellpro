using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostSphere.Models
{
    public class Comment
    {
        public int Id { get; set; } // ID do comentário
        public int TopicId { get; set; } // ID do tópico ao qual o comentário pertence
        
        [Required(ErrorMessage = "O comentário é obrigatório")]
        [StringLength(500, ErrorMessage = "O comentário pode ter no máximo 500 caracteres.")]
        public string Text { get; set; } // Texto do comentário
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public string Author { get; set; } // Nome do autor
        public DateTime CreatedAt { get; set; } // Data de criação
        public int? ParentId { get; set; } // Referência ao comentário pai
        public Comment ParentComment { get; set; } // Comentário pai
        public List<Comment> Replies { get; set; } // Respostas ao comentário

        public Comment()
        {
            Replies = new List<Comment>();
            CreatedAt = DateTime.Now;
        }
    }
}
