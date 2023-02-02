using BlogEntityFrameworkSample.Data;
using BlogEntityFrameworkSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BlogEntityFrameworkSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new BlogDataContext();

            var posts = context.Posts
                .AsNoTracking()   
                .Include(x => x.Author) // Adiciona referencia de outra tabela para também carregar seus dados.
                .Include(x => x.Category)
                    .ThenInclude(x=>x.Slug) // Subcategoria dentro de Category é mapeada através do ThenInclude
                .OrderBy(x => x.LastUpdateDate)
                .ToList();

            foreach (var post in posts)
            {
                Console.WriteLine($"{post.Title} escrito por {post.Author?.Name}");
            }
                      
        }

        static void PostWithDataAnotations()
        {
            using var context = new BlogDataContext();

            var user = new User()
            {
                Name = "Herbert Carvalho",
                Slug = "herbertcarvalho",
                Email = "hscarvalho@outlook.com.br",
                Bio = ".NET DEVELOPER EXPECIALIST",
                Image = "https://herbert.carv",
                PasswordHash = "123456"
            };

            var category = new Category()
            {
                Name = "Backend",
                Slug = "backend"
            };

            var post = new Post()
            {
                Author = user,
                Category = category,
                Body = "<p>Hello World</p>",
                Slug = "ef-core",
                Summary = "Samples com EntityFramework",
                Title = "Ef Core",
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now
            };

            context.Posts.Add(post);
            context.SaveChanges();
        }        
        static void QueryWithIncludeAndThenInclude()
        {
            using var context = new BlogDataContext();

            var posts = context.Posts
                .AsNoTracking()
                .Include(x => x.Author) // Adiciona referencia de outra tabela para também carregar seus dados.
                .Include(x => x.Category)
                    .ThenInclude(x => x.Slug) // Subcategoria dentro de Category é mapeada através do ThenInclude
                .OrderBy(x => x.LastUpdateDate)
                .ToList();

            foreach (var post in posts)
            {
                Console.WriteLine($"{post.Title} escrito por {post.Author?.Name}");
            }
        }

        static void UpdateInIncludeQuery()
        {
            using var context = new BlogDataContext();

            var posts = context.Posts
                .AsNoTracking()
                .Include(x => x.Author) // Adiciona referencia de outra tabela para também carregar seus dados.
                .Include(x => x.Category)
                    .ThenInclude(x => x.Slug) // Subcategoria dentro de Category é mapeada através do ThenInclude
                .OrderBy(x => x.LastUpdateDate)
                .ToList();

            posts.FirstOrDefault().Category.Slug = "Teste"; // troca o nome do Slug sem precisar ir na tebela de category.
        }
    }
}
