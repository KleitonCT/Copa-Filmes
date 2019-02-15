using CopaFilmes.Domain;
using System.Collections.Generic;

namespace CopaFilmes.Presentation.Models
{
    public class FilmeViewModel
    {
        public FilmeViewModel()
        {
            Filme = new Filme();
            Filmes = new List<Filme>();
        }

        public Filme Filme { get; set; }
        public List<Filme> Filmes { get; set; }
    }
}