using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace SistemaBarbearia.Helper
{
    public static class Helper
    {
        //public const string Table = "table table-striped table-bordered";
        //public const string Table = "table table-striped table-bordered table-hover responsive";
        //public const string Table = "table-hover table table-condensed table-bordered table-striped";
        public const string InputXS = "col-lg-2 col-md-2 col-sm-2 col-xs-12";
        public const string InputSM = "col-lg-3 col-md-3 col-sm-3 col-xs-12";
        public const string InputSMMD = "col-lg-4 col-md-4 col-sm-4 col-xs-12";
        public const string InputMD = "col-lg-6 col-md-6 col-sm-6 col-xs-12";
        public const string InputMDLG = "col-lg-7 col-md-7 col-sm-7 col-xs-12";
        public const string InputLG = "col-lg-9 col-md-9 col-sm-9 col-xs-12";
        public const string Input1 = "col-lg-1 col-md-1 col-sm-1 col-xs-12";
        public const string Input2 = "col-lg-2 col-md-2 col-sm-2 col-xs-12";
        public const string Input3 = "col-lg-3 col-md-3 col-sm-3 col-xs-12";
        public const string Input4 = "col-lg-4 col-md-4 col-sm-4 col-xs-12";
        public const string Input5 = "col-lg-5 col-md-5 col-sm-5 col-xs-12";
        public const string Input6 = "col-lg-6 col-md-6 col-sm-6 col-xs-12";
        public const string Input7 = "col-lg-7 col-md-7 col-sm-7 col-xs-12";
        public const string Input9 = "col-lg-9 col-md-9 col-sm-9 col-xs-12";
        public const string Input10 = "col-lg-10 col-md-10 col-sm-10 col-xs-12";
        public const string Input11 = "col-lg-11 col-md-11 col-sm-11 col-xs-12";
        public const string Input12 = "col-lg-12 col-md-12 col-sm-12 col-xs-12";
        public const string LabelHidenXS = "control-label col-lg-2 col-md-2 col-sm-3 hidden-xs control-label-hiden-xs";
        public const string LabelHiden = "control-label hidden-lg hidden-md hidden-sm visible-xs col-xs-12";
        public const string LabelXXS = "control-label col-lg-1 col-md-1 col-sm-1 col-xs-12";
        public const string LabelXS = "control-label col-lg-2 col-md-2 col-sm-2 col-xs-12";
        public const string LabelSM = "control-label col-lg-3 col-md-3 col-sm-3 col-xs-12";
        public const string LabelMD = "control-label col-lg-4 col-md-4 col-sm-4 col-xs-12";
        public const string LabelTop = "control-label";
        public const string Label = "control-label col-lg-2 col-md-2 col-sm-3 col-xs-12";
        public const string Input8 = "col-lg-8 col-md-8 col-sm-8 col-xs-12";      
    }
    public static class MvcHtmlStringX
    {
        const string keyForBlockScript = "__key_For_Js_Block";
        const string keyForBlockStyle = "__key_For_Css_Block";

        public static MvcHtmlString AddScriptBlock(this HtmlHelper helper, Func<dynamic, System.Web.WebPages.HelperResult> template)
        {
            return AddBlock(helper, template, keyForBlockScript);
        }

        private static MvcHtmlString AddBlock(HtmlHelper helper, Func<dynamic, System.Web.WebPages.HelperResult> template, string type)
        {
            var stringBuilder = helper.ViewContext.HttpContext.Items[type] as StringBuilder ?? new StringBuilder();
            stringBuilder.Append(template(null).ToHtmlString());
            helper.ViewContext.HttpContext.Items[type] = stringBuilder;
            return new MvcHtmlString(string.Empty);
        }

    }

    public class SelectFunctions
    {
        public static string getInputId(string id, string prefixo)
        {
            return ((string.IsNullOrEmpty(prefixo) ? "" : prefixo + "_") + id).Replace('.', '_');
        }
        //public static MvcHtmlString ClientPrefixName(this HtmlHelper htmlHelper)

        public static string getInputName(string name, string prefixo)
        {
            return (string.IsNullOrEmpty(prefixo) ? "" : prefixo + ".") + name;
        }

    }
    public static partial class HtmlExtensions
    {
        public static MvcHtmlString ClientPrefixName(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(htmlHelper.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix.Replace('.', '_'));
        }
        public static MvcHtmlString ClientPrefix(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(htmlHelper.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix);
        }

        public static MvcHtmlString ClientNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)));
        }

        public static MvcHtmlString ClientIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        }

    }
   
}
public static class FatHtml
{
    const string keyForBlockScript = "__key_For_Js_Block";
    const string keyForBlockStyle = "__key_For_Css_Block";

    private static MvcHtmlString AddBlock(HtmlHelper helper, Func<dynamic, System.Web.WebPages.HelperResult> template, string type)
    {
        var stringBuilder = helper.ViewContext.HttpContext.Items[type] as StringBuilder ?? new StringBuilder();
        stringBuilder.Append(template(null).ToHtmlString());
        helper.ViewContext.HttpContext.Items[type] = stringBuilder;
        return new MvcHtmlString(string.Empty);
    }

    /// <summary>
    /// Adiciona um bloco JavaScript ao fim da lista de bloco de scripts
    /// </summary> 
    public static MvcHtmlString AddScriptBlock(this HtmlHelper helper, Func<dynamic, System.Web.WebPages.HelperResult> template)
    {
        return AddBlock(helper, template, keyForBlockScript);
    }

    /// <summary>
    /// Adiciona um bloco Css ao fim da lista de bloco de estilos
    /// </summary>
    public static MvcHtmlString AddStyleBlock(this HtmlHelper helper, Func<dynamic, System.Web.WebPages.HelperResult> template)
    {
        return AddBlock(helper, template, keyForBlockStyle);
    }

    /// <summary>
    /// Renderiza todos blocos de scripts
    /// </summary>
    public static MvcHtmlString RenderScriptBlocks(this HtmlHelper helper)
    {
        var stringBuilder = helper.ViewContext.HttpContext.Items[keyForBlockScript] as StringBuilder ?? new StringBuilder();
        return new MvcHtmlString(stringBuilder.ToString());
    }

    /// <summary>
    /// Renderiza todos blocos de estilos
    /// </summary>
    public static MvcHtmlString RenderStyleBlocks(this HtmlHelper helper)
    {
        var stringBuilder = helper.ViewContext.HttpContext.Items[keyForBlockStyle] as StringBuilder ?? new StringBuilder();
        return new MvcHtmlString(stringBuilder.ToString());
    }
}
public class JsonSelect<T>
{
    public JsonSelect() { }

    public JsonSelect(IQueryable<T> query, int? page, int? pageSize)
    {
        page = page ?? 10;
        pageSize = pageSize ?? 10;

        totalCount = query.Count();
        totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        more = (page * pageSize) < totalCount;

        results = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
    }

    public List<T> results { get; set; }
    public int totalCount { get; set; }
    public int totalPages { get; set; }
    public bool more { get; set; }
}
