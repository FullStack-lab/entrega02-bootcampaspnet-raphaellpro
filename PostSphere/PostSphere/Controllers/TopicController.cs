using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using PostSphere.Models;
using System.Linq;

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

        private static List<Comment> _comments = new List<Comment>(); // Simulando banco de dados

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

        // GET: InteractiveRoom (Página do Tópico)
        public ActionResult InteractiveRoom(int id)
        {
            var topic = _comments.FirstOrDefault(c => c.Id == id && c.ParentId == null);
            if (topic == null)
                return HttpNotFound();

            var replies = _comments.Where(c => c.ParentId == topic.Id).ToList();
            topic.Replies = BuildCommentTree(topic.Id);

            return View(topic);
        }

        // GET: Responder a Comentário
        public ActionResult Reply(int parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        // POST: Salvar Resposta
        [HttpPost]
        public ActionResult Reply(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = _comments.Count + 1; // Simula auto incremento
                _comments.Add(comment);
                return RedirectToAction("InteractiveRoom", new { id = comment.ParentId });
            }

            return View(comment);
        }

        // Constrói a árvore de respostas hierárquicas
        private List<Comment> BuildCommentTree(int parentId)
        {
            return _comments.Where(c => c.ParentId == parentId)
                            .Select(c => new Comment
                            {
                                Id = c.Id,
                                Text = c.Text,
                                Author = c.Author,
                                CreatedAt = c.CreatedAt,
                                ParentId = c.ParentId,
                                Replies = BuildCommentTree(c.Id)
                            }).ToList();
        }

        // GET: Exibir formulário de edição de comentário
        public ActionResult EditComment(int id)
        {
            // Localiza o comentário pelo ID
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return HttpNotFound();

            return View(comment); // Passa o comentário para a View
        }

        // POST: Salvar edição do comentário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(Comment editedComment)
        {
            if (ModelState.IsValid)
            {
                // Localiza o comentário original na lista
                var comment = _comments.FirstOrDefault(c => c.Id == editedComment.Id);
                if (comment == null)
                    return HttpNotFound();

                // Atualiza apenas o texto
                comment.Text = editedComment.Text;

                // Redireciona para a página adequada
                if (comment.ParentId == null)
                {
                    // Se for um comentário principal
                    return RedirectToAction("InteractiveRoom", new { id = comment.Id });
                }
                else
                {
                    // Se for uma resposta, redireciona para o tópico principal
                    var mainTopicId = FindMainTopicId(comment.ParentId.Value);
                    return RedirectToAction("InteractiveRoom", new { id = mainTopicId });
                }
            }

            return View(editedComment); // Retorna para edição em caso de erro
        }

        // Método auxiliar para encontrar o ID do tópico principal
        private int FindMainTopicId(int parentId)
        {
            var parentComment = _comments.FirstOrDefault(c => c.Id == parentId);
            if (parentComment == null || parentComment.ParentId == null)
                return parentComment?.Id ?? parentId;

            // Busca recursivamente até o comentário principal
            return FindMainTopicId(parentComment.ParentId.Value);
        }

    }
}
