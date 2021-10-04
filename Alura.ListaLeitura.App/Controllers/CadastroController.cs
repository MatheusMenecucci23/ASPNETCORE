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
    //o sufixo Controller é obrigatório para o serviço MVC funcionar corretamente
    public class CadastroController
    {

        public string Incluir(Livro livro)
        {

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return "O livro foi adicionado com sucesso";
        }


        public IActionResult ExibeFormulario()
        {

            //var html = HtmlUtils.CarregaArquivoHTML("formulario");
            //viewResult = resultados de Actions que vão retornar HTML
            //o arquivo formulario deve ficar dentro da Pasta Views/Cadastro e com extensão cshtml
            var html = new ViewResult { ViewName = "formulario" };
            return html;
        }


    }
}
