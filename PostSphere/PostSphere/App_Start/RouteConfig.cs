using System.Web.Mvc;
using System.Web.Routing;

namespace PostSphere
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignorar recursos estáticos
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Rotas específicas para comentários
            routes.MapRoute(
                name: "CommentList", // Listar comentários
                url: "comentarios",
                defaults: new { controller = "Topic", action = "InteractiveRoom" }
            );

            routes.MapRoute(
                name: "NewComment", // Criar novo comentário
                url: "comentarios/novo",
                defaults: new { controller = "Topic", action = "Reply" }
            );

            routes.MapRoute(
                name: "EditComment", // Editar comentário
                url: "comentarios/editar/{id}",
                defaults: new { controller = "Topic", action = "EditComment" },
                constraints: new { id = @"\d+" } // ID deve ser numérico
            );

            routes.MapRoute(
                name: "DeleteComment", // Deletar comentário
                url: "comentarios/deletar/{id}",
                defaults: new { controller = "Topic", action = "DeleteComment" },
                constraints: new { id = @"\d+" }
            );

            // Rotas específicas para tópicos
            routes.MapRoute(
                name: "TopicList", // Listar tópicos
                url: "topicos",
                defaults: new { controller = "Topic", action = "TopicList" }
            );

            routes.MapRoute(
                name: "CreateTopic", // Criar novo tópico
                url: "topicos/novo",
                defaults: new { controller = "Topic", action = "CreateTopic" }
            );

            routes.MapRoute(
                name: "TopicDetail", // Detalhes de um tópico específico
                url: "topicos/{id}",
                defaults: new { controller = "Topic", action = "TopicDetail" },
                constraints: new { id = @"\d+" } // ID deve ser numérico
            );

            // Rota padrão
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
