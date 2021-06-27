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

        [Required]
        public string dsProduto { get; set; }

        [Required]
        public string nrUnidade { get; set; }

        

        [Required]
        public int nrQtd { get; set; }

        [Required]
        public int qtdEstoque { get; set; }

        [Required]
        public string codBarra { get; set; }

        [Required]
        public Decimal? vlCompra { get; set; }

        [Required]
        public Decimal? vlCusto { get; set; }

        [Required]
        public Decimal? vlVenda { get; set; }

        public int idCategoria { get; set; }

        public SistemaBarbearia.ViewModels.Categorias.SelectCategoriaVM categoria { get; set; }
      

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}
