using CopaFilmes.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CopaFilmes.Business
{
    public class FilmeBusiness
    {
        List<Filme> filmesParticipantes = new List<Filme>();
        List<Filme> filmesVencedoresRodada1 = new List<Filme>();
        List<Filme> filmesVencedoresRodada2 = new List<Filme>();
        Filme filmeVencedor = new Filme();

        public List<Filme> GetFilmes()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<string> response = httpClient.GetStringAsync(new Uri("http://copadosfilmes.azurewebsites.net/api/filmes"));
                return JsonConvert.DeserializeObject<List<Filme>>(response.Result);
            }
        }

        public List<Filme> IniciarCampeonato(List<Filme> _filmes)
        {
            _filmes.Sort((x, y) => x.Titulo.CompareTo(y.Titulo));

            return ContagemPontos(_filmes);
        }

        private List<Filme> ContagemPontos(List<Filme> _filmes)
        {
            List<Filme> filmesVencedores = new List<Filme>();

            if (_filmes.Count != 2)
            {
                for (int i = 1; i <= _filmes.Count / 2; i++)
                {
                    filmesVencedores.Add(_filmes[_filmes.Count - i].Nota <= _filmes[i - 1].Nota
                                                             ? _filmes[i - 1]
                                                             : _filmes[_filmes.Count - i]);
                }
                return ContagemPontos(filmesVencedores.OrderBy(x => x.Titulo).ToList());
            }
            else
            {
                if (_filmes[0].Nota == _filmes[1].Nota)
                    return _filmes;
                else
                {
                    var _filmesOrdered = _filmes.OrderByDescending(x => x.Nota).ToList();
                    return _filmesOrdered;
                }
            }
        }
    }
}
