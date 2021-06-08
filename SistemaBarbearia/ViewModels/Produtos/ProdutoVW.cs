using SistemaBarbearia.Models.Categorias;
using SistemaBarbearia.Models.Fornecedores;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Produtos
{
    public class ProdutoVW
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Produto")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Produto não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsProduto { get; set; }


        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Campo Unidade não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flUnidade { get; set; }

        public static SelectListItem[] Unidade
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Text = "UN", Value = "UN" },

                };
            }
        }


        [Display(Name = "Quatidade")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Quatidade não Pode ser em Branco!", AllowEmptyStrings = false)]
        public int nrQtd { get; set; }

        [Display(Name = "Quatidade no Estoque")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Quatidade no Estoque não Pode ser em Branco!", AllowEmptyStrings = false)]
        public int qtdEstoque { get; set; }


        [Display(Name = "Codigo Barra")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Codigo Barra não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string codBarra { get; set; }


        [Display(Name = "Valor Compra")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Valor Compra  não Pode ser em Branco!", AllowEmptyStrings = false)]
        public decimal? vlCompra { get; set; }

        [Display(Name = "Valor Custo")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Valor Custo  não Pode ser em Branco!", AllowEmptyStrings = false)]
        public decimal? vlCusto { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo categoria não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Categorias.SelectCategoriaVM categoria { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Campo fornecedor não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Fornecedores.SelectFornecedorVM fornecedor { get; set; }


        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}