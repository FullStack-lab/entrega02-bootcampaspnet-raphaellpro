using PostSphere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PostSphere.Controllers
{
    public class CommentController : Controller
    {
        private static List<Comment> comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Text = "Este é um comentário inicial.",
                Author = "John Doe",
                CreatedAt = DateTime.Now,
                Replies = new List<Reply>
                {
                    new Reply { Id = 1, Text = "Esta é uma resposta.", Author = "Jane Doe", CreatedAt = DateTime.Now, CommentId = 1 }
                }
            }
        };

        public ActionResult Index()
        {
            return View(comments);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Comment model)
        {
            if (ModelState.IsValid)
            {
                model.Id = comments.Count + 1;
                model.CreatedAt = DateTime.Now;
                comments.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
