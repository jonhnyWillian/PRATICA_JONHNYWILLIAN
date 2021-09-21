using SistemaBarbearia.Models.Categorias;
using SistemaBarbearia.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Models.Produtos
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

   
        public string dsProduto { get; set; }

      
        public string nrUnidade { get; set; }

      
        public int nrQtd { get; set; }

       
        public string codBarra { get; set; }

      
        public Decimal? vlCompra { get; set; }

       
        public Decimal? vlCusto { get; set; }

      
        public Decimal? vlVenda { get; set; }

        public int idCategoria { get; set; }

        public SistemaBarbearia.ViewModels.Categorias.SelectCategoriaVM categoria { get; set; }

        public int IdFornecedor { get; set; }

        public SistemaBarbearia.ViewModels.Fornecedores.SelectFornecedorVM fornecedor { get; set; }


        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}
