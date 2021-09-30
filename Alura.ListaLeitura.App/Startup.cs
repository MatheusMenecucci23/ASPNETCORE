using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    //essa classe se faz necessária para criar um host

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        //essa classe está configurando um Request Pipeline
        public void Configure(IApplicationBuilder app)
        {
            //Usando o serviço de roteamento do aspnet core
            //construindo rotas com RouteBuilder
            var builder = new RouteBuilder(app);

            //{"caminho/rota", referência do método que será chamado}
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            //rotas com template {}
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            //template com restrição ":", no caso o id só pode ser inteiro
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);
            builder.MapRoute("Cadastro/Incluir", ProcessaFomulario);


            var rotas = builder.Build();

            //usando a rotas
            app.UseRouter(rotas);


        }

        private Task ProcessaFomulario(HttpContext context)
        {

            var livro = new Livro()
            {
                //pegando as informações do formulário com Request.Form
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First(),
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }


        public Task NovoLivroParaLer(HttpContext context)
        {

            var livro = new Livro()
            {
                //pegando a rota pelo getrouvalue ("nome do seguimento de rota")
                Titulo = context.GetRouteValue("nome").ToString(),
                Autor = context.GetRouteValue("autor").ToString(),
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        //Rotas sem o ASP.NET CORE
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
            if (caminhosAtendidos.ContainsKey(context.Request.Path))
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
            var conteudoArquivo = CarregaArquivoHTML("para-ler");

            foreach (var livro in _repo.ParaLer.Livros)
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", "");

            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(conteudoArquivo);

        }
        public Task LivrosLendo(HttpContext context)
        {

            var conteudoArquivo = CarregaArquivoHTML("lendo");
            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

            foreach (var livro in _repo.Lendo.Livros)
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", "");
            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(conteudoArquivo);

        }
        public Task LivrosLidos(HttpContext context)
        {

            //pegando uma gama de livros no LivroRepositorioCSV
            var _repo = new LivroRepositorioCSV();

            var conteudoArquivo = CarregaArquivoHTML("lidos");

            foreach (var livro in _repo.Lidos.Livros)
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }

            conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", "");
            
            //respondendo a requisição
            //imprimindo na web o repositorio de livros
            return context.Response.WriteAsync(conteudoArquivo);

        }
    }
}