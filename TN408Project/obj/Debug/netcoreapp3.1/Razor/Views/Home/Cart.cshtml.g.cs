#pragma checksum "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6626e67ec5ae51074575956dfd9fcd67c61bcc60"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Cart), @"mvc.1.0.view", @"/Views/Home/Cart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\_ViewImports.cshtml"
using TN408Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\_ViewImports.cshtml"
using TN408Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6626e67ec5ae51074575956dfd9fcd67c61bcc60", @"/Views/Home/Cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d2611990321c45f8e01c486cd6ae5b96afc7cb22", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Cart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TN408Project.Models.CartContent>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
  
    ViewData["Title"] = "Cart";
    Layout = "~/Views/Shared/_Layout.cshtml";
    double total = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script src=""https://code.jquery.com/jquery-3.6.0.js"" integrity=""sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk="" crossorigin=""anonymous""></script>

<script>
    async function changeQuality(productid, quantity) {
        await $.ajax({
            type: 'POST', url: 'updatecart', data: { productid: productid, quantity: quantity }, success: function (response) {
                if (response.success) {
                }
            },
            error: function (response) {
                alert(""error!"");  //
            }
        });
        location.reload();
    }
    async function addDH() {
        await $.ajax({
            type: 'POST', url: 'addDonHang', data: {}, success: function (response) {

            }
        }); alert(""Cập nhật đơn hàng của bạn thành công!"");
        window.location = ""/"";
    }
</script>

<nav aria-label=""breadcrumb"" class=""mb-3"">
    <ol class=""breadcrumb"">
        <li class=""breadcrumb-item""><a href=""/"">Trang chủ</a></li>
        <li class=");
            WriteLiteral("\"breadcrumb-item active\" aria-current=\"page\">Giỏ hàng của tôi</li>\r\n    </ol>\r\n</nav>\r\n\r\n<div class=\"album bg-light mt-3\">\r\n    <div class=\"container\">\r\n        <div class=\"row row-cols-1 row-cols-sm-3 row-cols-md-4 g-3\">\r\n");
#nullable restore
#line 43 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col\">\r\n                    <div class=\"card shadow-sm\" style=\"height:100%\">\r\n                        <img");
            BeginWriteAttribute("src", " src=\"", 1602, "\"", 1662, 1);
#nullable restore
#line 47 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
WriteAttributeValue("", 1608, Url.Content("~/Myfiles/" + item.anhspdeltail.LinkAnh), 1608, 54, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"bd-placeholder-img card-img-top\" width=\"100%\" height=\"225\" />\r\n\r\n                        <div class=\"card-body\">\r\n                            <h4>");
#nullable restore
#line 50 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
                           Write(item.sanphamdetail.TenSanPham);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            <div class=\"d-flex justify-content-between align-items-center\">\r\n                                <p class=\"text-danger fw-bold\">");
#nullable restore
#line 52 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
                                                          Write(item.sanphamdetail.Gia);

#line default
#line hidden
#nullable disable
            WriteLiteral(" VNĐ</p>\r\n                                <div>\r\n                                    SL:\r\n                                    <input");
            BeginWriteAttribute("value", " value=\"", 2165, "\"", 2181, 1);
#nullable restore
#line 55 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
WriteAttributeValue("", 2173, item.SL, 2173, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"number\" id=\"quality\" name=\"quality\" class=\"quanlity text-center\"");
            BeginWriteAttribute("onchange", " onchange=\"", 2253, "\"", 2316, 4);
            WriteAttributeValue("", 2264, "changeQuality(", 2264, 14, true);
#nullable restore
#line 55 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
WriteAttributeValue("", 2278, item.sanphamdetail.MaSanPham, 2278, 29, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 2307, ",", 2308, 2, true);
            WriteAttributeValue(" ", 2309, "value)", 2310, 7, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"hidden\">\r\n");
#nullable restore
#line 59 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
                                   double val = Convert.ToDouble(item.sanphamdetail.Gia) * item.SL;
                                    total += val;
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
#nullable restore
#line 62 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
                           Write(val);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n");
#nullable restore
#line 68 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"d-flex justify-content-between align-items-center mt-4\">\r\n            <p class=\"text-danger fw-bold\">Tổng giá: ");
#nullable restore
#line 72 "E:\TN408(NLCN)\TN408Project\TN408Project\TN408Project\Views\Home\Cart.cshtml"
                                                Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral(" .000VND</p>\r\n            <button onclick=\"addDH()\" class=\"button-29\" style=\"width:200px\">Thanh Toán</button>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TN408Project.Models.CartContent>> Html { get; private set; }
    }
}
#pragma warning restore 1591