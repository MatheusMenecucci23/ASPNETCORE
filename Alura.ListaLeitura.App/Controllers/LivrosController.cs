using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    //Herdando da classe Controller
    //o sufixo Controller é obrigatório para o serviço MVC funcionar corretamente
    public class LivrosController : Controller
    {
        public IEnumerable<Livro> Livros { get; set; }

        private static string CarregaLista(IEnumerable<Livro> livros)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lista");
            return conteudoArquivo.Replace("#NOVO-ITEM#","");
          
        }


        //IActionResult
        public IActionResult ParaLer()
        {

            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

            //var html = CarregaLista(_repo.ParaLer.Livros);

            ViewBag.Livros = _repo.ParaLer.Livros;

            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return View("Lista");

        }


        public IActionResult Lendo()
        {
            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

            ViewBag.Livros = _repo.Lendo.Livros;

            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return View("Lista");


        }



        public IActionResult Lidos()
        {

            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

          ViewBag.Livros = _repo.Lidos.Livros;

            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return View("Lista");

        }


        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return livro.Detalhes();
        }

        public string Teste()
        {
            return "Nova funcionalidade implementada";
        }
    }
}
