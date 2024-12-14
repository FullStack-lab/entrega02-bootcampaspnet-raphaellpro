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

        // GET: Exibir formulário de criação
        public ActionResult CreateTopic()
        {
            return View();
        }

        // POST: Salvar tópico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopic(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Id = topics.Count + 1; // Simular auto-incremento
                topic.CreatedAt = DateTime.Now;
                topic.ReplyCount = 0; // Inicia sem respostas

                // Reduz o texto para exibição na lista
                topic.Text = topic.Text.Length > 100
                    ? topic.Text.Substring(0, 100) + "..."
                    : topic.Text;

                topics.Add(topic);

                // Redirecionar para os detalhes do tópico criado
                return RedirectToAction("TopicDetail", new { id = topic.Id });
            }

            return View(topic);
        }

        // GET: Exibir detalhes do tópico
        public ActionResult TopicDetail(int id)
        {
            var topic = topics.Find(t => t.Id == id);
            if (topic == null)
                return HttpNotFound();

            return View(topic);
        }
    }
}
