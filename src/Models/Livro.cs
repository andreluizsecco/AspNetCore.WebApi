using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.WebApi.Models
{
    public class Livro
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public Livro()
        {
            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        public Livro(int id, string titulo, string autor, string categoria, int quantidade, decimal preco) : this()
        {
            ID = id;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
            Quantidade = quantidade;
            Preco = preco;
        }

        public void Desativar() =>
            Ativo = false;

        public static IEnumerable<Livro> ObterLivros()
        {
            return new List<Livro>
            {
                new Livro(1, "Domain-Driven Design: Tackling Complexity in the Heart of Software", "Eric Evans", "Software", 26, 59.90m),
                new Livro(2, "Agile Principles, Patterns, and Practices in C#", "Robert C. Martin", "Software", 13, 45.90m),
                new Livro(3, "Clean Code: A Handbook of Agile Software Craftsmanship", "Robert C. Martin", "Software", 10, 33.90m),
                new Livro(4, "Implementing Domain-Driven Design", "Vaughn Vernon", "Software", 22, 59.90m),
                new Livro(5, "Patterns, Principles, and Practices of Domain-Driven Design", "Scott Millet", "Software", 15, 42.90m),
                new Livro(6, "Refactoring: Improving the Design of Existing Code", "Martin Fowler", "Software", 5, 31.90m)
            };
        }

        public static Livro ObterLivro(int id)
        {
            return ObterLivros()
                .FirstOrDefault(l => l.ID == id);
        }
    }
}