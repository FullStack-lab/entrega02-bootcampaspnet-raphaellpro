using System;
using System.ComponentModel.DataAnnotations;

namespace PostSphere.Models
{
    public class Topic
    {
        public int Id { get; set; } // Identificador único

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(500, ErrorMessage = "O comentário pode ter no máximo 500 caracteres.")]
        public string Text { get; set; } // Texto do comentário principal

        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public string Author { get; set; } // Autor do comentário principal
        public DateTime CreatedAt { get; set; } // Data e hora de criação
        public int ReplyCount { get; set; } // Número de respostas

        public string ShortenedText
        {
            get
            {
                return Text.Length > 30 ? Text.Substring(0, 30) + "..." : Text;
            }
        }

    }
}
