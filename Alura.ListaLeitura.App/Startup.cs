using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Alura.ListaLeitura.App
{
    //essa classe se faz necessária para criar um host
    //Classe de inicialização
   
    public class Startup
    {

        //Esse código disponibiliza o serviço de roteamento padrão do Asp.Net Core no servidor.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.AddRouting(); serviço 

            //configurando e adicionando o padrão de roteamento MVC
            services.AddMvc();
        }


        //essa classe está configurando um Request Pipeline
        public void Configure(IApplicationBuilder app)
        {
            //Usando o serviço de roteamento do aspnet core
            //construindo rotas com RouteBuilder
            //var builder = new RouteBuilder(app);

            //o serviço MVC já faz isso automaticamente
            //builder.MapRoute("{controller}/{action}", RoteamentoPadrao.TratamentoPadrao);

            //{"caminho/rota", referência do método que será chamado}
            //builder.MapRoute("Livros/ParaLer", LivrosLogica.ParaLer);
            //builder.MapRoute("Livros/Lendo", LivrosLogica.Lendo);
            //builder.MapRoute("Livros/Lidos", LivrosLogica.Lidos);
            //builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.Detalhes);
            //builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            //rotas com template { }
            //builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivro);
            //template com restrição ":", no caso o id só pode ser inteiro
            //builder.MapRoute("Cadastro/Incluir", CadastroLogica.Incluir);


            //var rotas = builder.Build();

            //com essa linha, é mostrado os erros na aplicação web
            app.UseDeveloperExceptionPage();

            //usando o serviço MVC do ASPNET
            app.UseMvcWithDefaultRoute();

        }
    }
}