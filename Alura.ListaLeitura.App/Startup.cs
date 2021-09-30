using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    //essa classe se faz necessária para criar um host
   
    public class Startup
    {
        //essa classe está configurando um Request Pipeline
        public void Configure(IApplicationBuilder app)
        {
            //faz a requisição
            //executando as requisições
            app.Run(Roteamento);
        }

        //Rotas
        public Task Roteamento(HttpContext context)
        {
            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

            //criando as requisições/rotas
            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                //{"caminho/rota", referência do método que será chamado}
                {"/Livros/ParaLer", LivrosParaLer },
                {"/Livros/Lendo", LivrosLendo },
                { "/Livros/Lidos", LivrosLidos }
                
            };

            //verificando se o caminho requerido faz parte do nosso dicionário de rotas
            if(caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                //redirecionando para o caminho requerido com o context.Request.Path
                var metodo = caminhosAtendidos[context.Request.Path];
                //invocando um método Request
                return metodo.Invoke(context);
            }

            //mudando o StatusCode
            context.Response.StatusCode = 404;

            //se o caminho requerido não existir, exibir isso
            return context.Response.WriteAsync("Caminho inexistente.");
        }


        // criando um RequestDelegate com Task
        //criando os métodos que serão referenciado pelas rotas
        public Task LivrosParaLer(HttpContext context)
        {
            
            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();


            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(_repo.ParaLer.ToString());

        }
        public Task LivrosLendo(HttpContext context)
        {

            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();


            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(_repo.Lendo.ToString());

        }
        public Task LivrosLidos(HttpContext context)
        {

            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();


            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(_repo.Lidos.ToString());

        }
    }
}