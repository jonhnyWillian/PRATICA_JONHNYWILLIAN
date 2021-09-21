﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.ContaPagar
{
    public class ContaPagar
    {
        [Key]
        public int IdContaPagar { get; set; }
        public short nrModelo { get; set; }

        public short nrSerie { get; set; }

        public int nrNumero { get; set; }

        public int nrParcela{ get; set; }

        public decimal? vlParcela { get; set; }

        public DateTime? dtVencimento { get; set; }

        public DateTime? dtPagamento { get; set; }

        public decimal? txJuro { get; set; }

        public decimal? txMulta { get; set; }

        public decimal? txDesconto { get; set; }

        public decimal? vlPago { get; set; }

        public DateTime? dtEmissao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

        public ViewModels.Fornecedores.SelectFornecedorVM Fornecedor { get; set; }

        public ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPag { get; set; }



    }
}