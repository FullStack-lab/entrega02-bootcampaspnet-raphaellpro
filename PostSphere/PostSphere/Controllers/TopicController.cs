using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PostSphere.Models;

namespace PostSphere.Controllers
{
    public class TopicController : Controller
    {
        // Simulação de uma lista de tópicos
        private static List<Topic> topics = new List<Topic>
        {
            new Topic { Id = 1, Title = "Problema com acesso ao sistema", Author = "Ana Souza", CreatedAt = DateTime.Now.AddHours(-5), ReplyCount = 3 },
            new Topic { Id = 2, Title = "Sugestões de melhorias no layout", Author = "Carlos Lima", CreatedAt = DateTime.Now.AddDays(-1), ReplyCount = 7 },
            new Topic { Id = 3, Title = "Erro ao exportar relatório", Author = "Mariana Silva", CreatedAt = DateTime.Now.AddMinutes(-30), ReplyCount = 1 },
        };

        // Action para exibir a lista de tópicos
        public ActionResult TopicList()
        {
            return View(topics);
        }
    }
}
