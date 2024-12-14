using System;

namespace PostSphere.Models
{
    public class Topic
    {
        public int Id { get; set; } // Identificador único
        public string Title { get; set; } // Título resumido do comentário principal
        public string Author { get; set; } // Autor do comentário principal
        public DateTime CreatedAt { get; set; } // Data e hora de criação
        public int ReplyCount { get; set; } // Número de respostas
    }
}
