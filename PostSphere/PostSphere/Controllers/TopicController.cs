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
            new Topic { Id = 1, Text = "Descrição detalhada do problema com o sistema", Author = "Ana Souza", CreatedAt = DateTime.Now.AddHours(-5), ReplyCount = 3 },
            new Topic { Id = 2, Text = "Sugestões iniciais de como podemos melhorar o layout", Author = "Carlos Lima", CreatedAt = DateTime.Now.AddDays(-1), ReplyCount = 7 },
            new Topic { Id = 3, Text = "Ao tentar exportar, o sistema apresenta um erro de conexão", Author = "Mariana Silva", CreatedAt = DateTime.Now.AddMinutes(-30), ReplyCount = 1 },
        };

        private static List<Comment> _comments = new List<Comment>
         {
            new Comment { Id = 1, ParentId = null, Author = "Ana", Text = "Primeiro comentário", CreatedAt = DateTime.Now },
            new Comment { Id = 2, ParentId = 1, Author = "Carlos", Text = "Resposta ao primeiro comentário", CreatedAt = DateTime.Now },
            new Comment { Id = 3, ParentId = 1, Author = "Mariana", Text = "Outra resposta", CreatedAt = DateTime.Now },
            new Comment { Id = 4, ParentId = 2, Author = "Ana", Text = "Resposta aninhada", CreatedAt = DateTime.Now }
        }; // Simulando banco de dados

        // Constrói a árvore de respostas hierárquicas
        private List<Comment> BuildCommentTree(int topicId, int? parentId = null)
        {
            return _comments
                .Where(c => c.TopicId == topicId && c.ParentId == parentId)
                .Select(c => new Comment
                {
                    Id = c.Id,
                    Text = c.Text,
                    Author = c.Author,
                    CreatedAt = c.CreatedAt,
                    ParentId = c.ParentId,
                    Replies = BuildCommentTree(topicId, c.Id) // Chamado recursivamente
                }).ToList();
        }

        // Variáveis para controle de IDs únicos
        private static int lastTopicId = 3; // Deve ser o maior ID existente na lista de tópicos
        private static int lastCommentId = 4; // Deve ser o maior ID existente na lista de comentários


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
                topic.Id = ++lastTopicId; // Simular auto-incremento
                topic.CreatedAt = DateTime.Now;
                topic.ReplyCount = 0; // Inicia sem respostas

                // Reduz o texto para exibição na lista
                topic.Text = topic.Text.Length > 100
                    ? topic.Text.Substring(0, 30) + "..."
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

        // GET: Exibir formulário de edição do tópico
        public ActionResult EditTopic(int id)
        {
            var topic = topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return HttpNotFound();

            return View(topic);
        }

        // POST: Salvar edição do tópico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTopic(Topic editedTopic)
        {
            if (ModelState.IsValid)
            {
                var topic = topics.FirstOrDefault(t => t.Id == editedTopic.Id);
                if (topic == null)
                    return HttpNotFound();

                topic.Text = editedTopic.Text;
                topic.Author = editedTopic.Author; // Atualiza dados básicos
                return RedirectToAction("TopicList");
            }

            return View(editedTopic);
        }

        // GET: Confirmar exclusão do tópico (apenas visualização por enquanto)
        public ActionResult DeleteTopic(int id)
        {
            var topic = topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return HttpNotFound();

            return View(topic);
        }

        // Método para excluir um tópico e seus comentários associados
        public ActionResult ConfirmDeleteTopic(int id)
        {
            var topic = topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            // Excluir os comentários associados ao tópico
            DeleteCommentsByTopicId(id);

            // Remover o tópico da lista
            topics.Remove(topic);

            // Redirecionar para a lista de tópicos
            return RedirectToAction("TopicList");
        }

        // Método auxiliar para excluir comentários associados a um tópico
        private void DeleteCommentsByTopicId(int topicId)
        {
            var commentsToDelete = _comments.Where(c => c.TopicId == topicId).ToList();
            foreach (var comment in commentsToDelete)
            {
                DeleteCommentWithReplies(comment); // Chama a função recursiva para excluir o comentário e suas respostas
            }
        }

        // GET: InteractiveRoom (Página do Tópico)
        public ActionResult InteractiveRoom(int id)
        {
            // Buscar o tópico pelo ID
            var topic = topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                return HttpNotFound(); // Retorna erro 404 se o tópico não existir

            // Busca comentários relacionados ao tópico
            var replies = _comments.Where(c => c.TopicId == id && c.ParentId == null).ToList();

            // Criar um modelo combinado para a View
            var model = new Comment
            {
                Id = topic.Id, // O ID do tópico será usado aqui
                Text = topic.Text,
                Author = topic.Author,
                CreatedAt = topic.CreatedAt,
                Replies = BuildCommentTree(id) // Lista de respostas ao tópico principal
            };

            return View(model); // Retorna a View com o modelo combinado
        }


        // GET: Responder a Comentário
        public ActionResult Reply(int? parentId, int topicId)
        {
            if (!parentId.HasValue)
                parentId = 0;

            ViewBag.ParentId = parentId;
            ViewBag.TopicId = topicId; // Envia o ID do tópico atual
            return View();
        }

        // POST: Salvar Resposta
        [HttpPost]
        public ActionResult Reply(Comment comment)
        {
            if (ModelState.IsValid)
            {
                // Caso ParentId seja nulo, o comentário é uma resposta direta ao tópico
                if (comment.ParentId == null)
                {
                    // Verificar se TopicId foi passado
                    if (comment.TopicId == 0)
                        return HttpNotFound("Erro: TopicId inválido.");
                }
                else
                {
                    // Se for uma resposta, herdar o TopicId do comentário pai
                    var parentComment = _comments.FirstOrDefault(c => c.Id == comment.ParentId);
                    if (parentComment != null)
                    {
                        comment.TopicId = parentComment.TopicId;
                    }
                    else
                    {
                        return HttpNotFound("Comentário pai não encontrado.");
                    }
                }

                // Adicionar o comentário
                comment.Id = GenerateUniqueCommentId();
                comment.CreatedAt = DateTime.Now;
                _comments.Add(comment);

                // Atualiza o ReplyCount do tópico
                var topic = topics.FirstOrDefault(t => t.Id == comment.TopicId);
                if (topic != null)
                {
                    topic.ReplyCount = _comments.Count(c => c.TopicId == comment.TopicId);
                }

                return RedirectToAction("InteractiveRoom", new { id = comment.TopicId });
            }

            // Retorna a view com erros de validação
            return View(comment);
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

                // Atualiza o texto do comentário
                comment.Text = editedComment.Text;

                // Redireciona sempre para a página do tópico relacionado
                return RedirectToAction("InteractiveRoom", new { id = comment.TopicId });
            }

            return View(editedComment); // Retorna à view de edição se houver erros
        }


        // Método auxiliar para encontrar o ID do tópico principal
        private int FindMainTopicId(int parentId)
        {
            var parentComment = _comments.FirstOrDefault(c => c.Id == parentId);
            if (parentComment == null)
                throw new InvalidOperationException("Comentário pai não encontrado.");

            return parentComment.TopicId; // Retorna diretamente o ID do tópico associado
        }


        // GET: Topic/DeleteComment/{id}
        public ActionResult DeleteComment(int id)
        {
            var comment = FindCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment); // Exibe a página de confirmação
        }

        // POST: Topic/ConfirmDelete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            var comment = FindCommentById(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            // Excluir o comentário e suas respostas associadas
            DeleteCommentWithReplies(comment);

            // Atualiza o ReplyCount do tópico
            var topic = topics.FirstOrDefault(t => t.Id == comment.TopicId);
            if (topic != null)
            {
                topic.ReplyCount = _comments.Count(c => c.TopicId == comment.TopicId);

            }

            // Redireciona para a página do tópico relacionado
            return RedirectToAction("InteractiveRoom", "Topic", new { id = comment.TopicId });
        }

        // Método para buscar um comentário pelo ID
        private Comment FindCommentById(int id)
        {
            var matchingComments = _comments.Where(c => c.Id == id).ToList();

            if (matchingComments.Count > 1)
            {
                throw new InvalidOperationException($"Mais de um comentário encontrado com o ID {id}.");
            }

            return matchingComments.FirstOrDefault();
        }

        private int GenerateUniqueCommentId()
        {
            int newId = _comments.Count + 1;

            // Garante que o novo ID seja único
            while (_comments.Any(c => c.Id == newId))
            {
                newId++;
            }

            return ++lastCommentId;
        }


        // Método para excluir um comentário e suas respostas associadas
        private void DeleteCommentWithReplies(Comment comment)
        {
            var replies = _comments.Where(c => c.ParentId == comment.Id).ToList();
            foreach (var reply in replies)
            {
                DeleteCommentWithReplies(reply); // Exclusão recursiva
            }

            _comments.Remove(comment); // Remover o comentário atual
        }

    }
}
