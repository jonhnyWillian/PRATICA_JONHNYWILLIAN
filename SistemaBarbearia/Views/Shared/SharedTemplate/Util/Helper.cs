//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace SistemaBarbearia.Views.Shared.SharedTemplate.Util
//{
//    public static class Helper
//    {
//        public const string Table = "table-hover table table-condensed table-bordered table-striped";
//        public const string InputXS = "col-lg-2 col-md-2 col-sm-2 col-xs-12";
//        public const string InputSM = "col-lg-3 col-md-3 col-sm-3 col-xs-12";
//        public const string InputSMMD = "col-lg-4 col-md-4 col-sm-4 col-xs-12";
//        public const string InputMD = "col-lg-6 col-md-6 col-sm-6 col-xs-12";
//        public const string InputMDLG = "col-lg-7 col-md-7 col-sm-7 col-xs-12";
//        public const string InputLG = "col-lg-9 col-md-9 col-sm-9 col-xs-12";
//        public const string Input1 = "col-lg-1 col-md-1 col-sm-1 col-xs-12";
//        public const string Input2 = "col-lg-2 col-md-2 col-sm-2 col-xs-12";
//        public const string Input3 = "col-lg-3 col-md-3 col-sm-3 col-xs-12";
//        public const string Input4 = "col-lg-4 col-md-4 col-sm-4 col-xs-12";
//        public const string Input5 = "col-lg-5 col-md-5 col-sm-5 col-xs-12";
//        public const string Input6 = "col-lg-6 col-md-6 col-sm-6 col-xs-12";
//        public const string Input7 = "col-lg-7 col-md-7 col-sm-7 col-xs-12";
//        public const string Input9 = "col-lg-9 col-md-9 col-sm-9 col-xs-12";
//        public const string Input10 = "col-lg-10 col-md-10 col-sm-10 col-xs-12";
//        public const string Input11 = "col-lg-11 col-md-11 col-sm-11 col-xs-12";
//        public const string Input12 = "col-lg-12 col-md-12 col-sm-12 col-xs-12";
//        public const string LabelHidenXS = "control-label col-lg-2 col-md-2 col-sm-3 hidden-xs control-label-hiden-xs";
//        public const string LabelHiden = "control-label hidden-lg hidden-md hidden-sm visible-xs col-xs-12";
//        public const string LabelXXS = "control-label col-lg-1 col-md-1 col-sm-1 col-xs-12";
//        public const string LabelXS = "control-label col-lg-2 col-md-2 col-sm-2 col-xs-12";
//        public const string LabelSM = "control-label col-lg-3 col-md-3 col-sm-3 col-xs-12";
//        public const string LabelMD = "control-label col-lg-4 col-md-4 col-sm-4 col-xs-12";
//        public const string LabelTop = "control-label";
//        public const string Label = "control-label col-lg-2 col-md-2 col-sm-3 col-xs-12";
//        public const string Input8 = "col-lg-8 col-md-8 col-sm-8 col-xs-12";


//        public static string UserName { get; }
//        public static string UserEmail { get; }
//        public static int UserId { get; }
//        public static string UserLogin { get; }
//        public static SelectListItem[] UF { get; }

//        public static MvcHtmlString BindParam(this HtmlHelper helper, NameValueCollection querystring, string notBind);
//        public static string Concat(string str1, string str2);
//        public static SelectListItem[] DiaSemana();
//        public static Form Editor(this HtmlHelper helper);
//        public static DateTime GetDtHr(DateTime? date, string hour, bool setHora = true);
//        public static string getInputId(string id, string prefixo);
//        public static string getInputName(string name, string prefixo);
//        public static object KeepFilter(this Controller controller, string filter = null);
//        public static SelectListItem[] Mes();
//        public static SelectListItem[] NaoSim(eNaoSim? defaultValue = null);
//        public static SelectListItem[] SimNao(eSimNao? defaultValue = null);
//        public static SelectListItem[] SimNaoUpperCase(eSimNao? defaultValue = null);
//        public static SelectListItem[] SN(eSN? defaultValue = null);
//        public static SelectListItem[] TipoPessoa(tipoPessoa? defaultValue = null);
//    }
//}