using System;
using System.Collections.Generic;

namespace PostSphere.Models
{
    public class Comment
    {
        public int Id { get; set; } // ID do comentário
        public string Text { get; set; } // Texto do comentário
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
