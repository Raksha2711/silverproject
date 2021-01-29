#pragma checksum "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e76"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PurchaseOrder_Index), @"mvc.1.0.view", @"/Views/PurchaseOrder/Index.cshtml")]
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
#line 1 "G:\silverproject\Admin.Web\Views\_ViewImports.cshtml"
using Admin.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\silverproject\Admin.Web\Views\_ViewImports.cshtml"
using Admin.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7c5ee5c1d4ef258edcde1839737ecdaef2e3e76", @"/Views/PurchaseOrder/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9171cc58583ccf403de820b78368d59e28bad3e", @"/Views/_ViewImports.cshtml")]
    public class Views_PurchaseOrder_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Command.Entity1.BillMaster>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "PickUp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "Delivery", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "CDC", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "PDC", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "PurchaseOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
   ViewData["Title"] = "Purchase Order";
    Layout = "~/Views/Shared/AdminTemplate/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<style>\r\n    .hidden\r\n    {\r\n        display:none;\r\n    }\r\n</style>\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n");
            WriteLiteral(@"            <div class=""col-sm-12"">
                <ol class=""breadcrumb float-sm-right"">
                    <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                    <li class=""breadcrumb-item active"">Purchase Order</li>
                </ol>
            </div>
        </div>
    </div><!-- /.container-fluid -->
</section>

<div class=""content"">
    <div class=""container-fluid"">
        <div class=""card card-primary"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e766685", async() => {
                WriteLiteral("\r\n                <div class=\"card-header\">\r\n                    <h3 class=\"card-title\">Purchase Order</h3>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <div class=\"row\">\r\n");
                WriteLiteral(@"                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>PO No</b></h5></label>
                        </div>
                        <div class=""col-lg-3 col-md-4 col-sm-4 col-xs-6"">
                            ");
#nullable restore
#line 42 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
                       Write(Html.TextBox("POId", (object)ViewBag.LatestId));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            <input type=\"text\" id=\"POId\" class=\"form-control\" placeholder=\"Enter\" readonly>\r\n                        </div>\r\n");
                WriteLiteral(@"                    </div>
                    <div class=""row"">
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>Date</b></h5></label>
                        </div>
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <input id=""Date"" name=""Date"" type=""text"" class=""form-control datetimepicker-input"" data-target=""#reservationdate"">
                            <div class=""input-group-append"" data-target=""#reservationdate"" data-toggle=""datetimepicker"">
                                <div class=""input-group-text""><i class=""fa fa-calendar""></i></div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>SalesPerson Name</b></h5></label>
                        </div>
                      ");
                WriteLiteral("  <div class=\"col-lg-3 col-md-4 col-sm-4 col-xs-6\">\r\n                            ");
#nullable restore
#line 63 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.SalesPersonName, ViewBag.SalesPerson as SelectList, "Select Sales Person", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""row"">

                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>Vendor Name</b></h5></label>
                        </div>
                        <div class=""col-lg-3 col-md-4 col-sm-4 col-xs-6"">
                            ");
#nullable restore
#line 72 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.VendorName, ViewBag.Vendor as SelectList, "Select Vendor", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>

                    </div>
                    <div class=""row"">
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>Pick Up/Delivery</b></h5></label>
                        </div>
                        <div class=""col-lg-3 col-md-4 col-sm-4 col-xs-6"">
                            <select class=""form-control"" name=""DelieveryPlace"" id=""DelieveryPlace"">");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7610667", async() => {
                    WriteLiteral("Select");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7611852", async() => {
                    WriteLiteral("PickUp");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7613037", async() => {
                    WriteLiteral("Delivery");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"</select>
                        </div>
                    </div>
                    <div class=""row hidden"" id=""divaddress"" >
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>Place of Delivery</b></h5></label>
                        </div>
                        <div class=""col-lg-3 col-md-4 col-sm-4 col-xs-6"">
");
                WriteLiteral("                            ");
#nullable restore
#line 90 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
                       Write(Html.DropDownListFor(model => model.DelieveryPlaceId, ViewBag.WareHouse as SelectList, "Select Address", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-lg-2 col-md-3 col-sm-3 col-xs-12"">
                            <label><h5><b>Payment Term</b></h5></label>
                        </div>
                        <div class=""col-lg-3 col-md-4 col-sm-4 col-xs-6"">
                            <select class=""form-control"" name=""PaymentTerm"" id=""PaymentTerm"">");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7615535", async() => {
                    WriteLiteral("Select");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7616720", async() => {
                    WriteLiteral("CDC");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7c5ee5c1d4ef258edcde1839737ecdaef2e3e7617902", async() => {
                    WriteLiteral("PDC");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("</select>\r\n                            <input type=\"text\" id=\"PaymentValue\" name=\"PaymentValue\" class=\"form-control hidden\" placeholder=\"Enter\" value=\"0\">\r\n");
                WriteLiteral(@"                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""card-body table-responsive p-0"">
                            <table class=""table table-hover text-nowrap classpo"" id=""tblpo"">
                                <thead>
                                    <tr>
                                        <th>Sr No</th>
                                        <th>Item Name</th>
                                        <th>Qty</th>
                                        <th>Unit</th>
                                        <th>Basic Rate</th>
                                        <th>Freight / addl cost</th>
                                        <th>CDC</th>
                                        <th>Discount 1</th>
                                        <th>Scheme</th>
                                        <th>Scheme(%)</th>
                                        <th>Scheme(Amt)</th>
                            ");
                WriteLiteral(@"            <th>GST Rate</th>
                                        <th>NLC</th>
                                        <th>Remark</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody id=""tbody"">
                                    <tr>
                                        <td> <input type=""text"" id=""SrNo"" class=""form-control"" placeholder=""SrNo""></td>
                                        <td>
");
                WriteLiteral("                                            ");
#nullable restore
#line 130 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
                                       Write(Html.DropDownListFor(model => model.ItemId, ViewBag.Item as SelectList, "Select Address", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                        </td>
                                        <td><input type=""text"" id=""Qty"" name=""Qty"" class=""form-control"" placeholder=""Qty""></td>
                                        <td><input type=""text"" id=""Unit"" name=""Unit"" class=""form-control"" placeholder=""Unit""></td>
                                        <td><input type=""text"" id=""BasicRate"" name=""BasicRate"" class=""form-control"" placeholder=""BasicRate""></td>
                                        <td><input type=""text"" id=""AddCost"" name=""AddCost"" class=""form-control"" placeholder=""Freight""></td>
                                        <td><input type=""text"" id=""CDC"" name=""CDC"" class=""form-control"" placeholder=""CDC""></td>
                                        <td><input type=""text"" id=""Discount1"" name=""Discount1"" class=""form-control"" placeholder=""Discount""></td>
                                        <td><input type=""text"" id=""Scheme1"" name=""Scheme1"" class=""form-control"" placeholder=""Scheme1""></td>
          ");
                WriteLiteral(@"                              <td><input type=""text"" id=""Scheme2"" name=""scheme2"" class=""form-control"" placeholder=""Scheme2""></td>
                                        <td><input type=""text"" id=""SchemeAmt"" name=""SchemeAmt"" class=""form-control"" placeholder=""SchemeAmt""></td>
                                        <td><input type=""text"" id=""GSTRate"" name=""GSTRate"" class=""form-control"" placeholder=""GSTRate""></td>
                                        <td><input type=""text"" id=""NLC"" name=""NLC"" class=""form-control"" placeholder=""NLC""></td>
                                        <td><input type=""text"" id=""Remarks"" name=""Remarks"" class=""form-control"" placeholder=""Remarks""></td>
                                        <td id=""Action1"">
                                            <span class=""btn btn-sm btn-success btn_row_below_new"">Add New</span> |
                                            <span class=""btn btn-sm btn-danger btn_row_delete"">Delete</span>
                                        </td>
  ");
                WriteLiteral(@"                                  </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class=""row"">
                        <button type=""submit"" class=""btn btn-primary""><i class=""fas fa-save""></i>Save</button>
                    </div>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
</div>
<script type=""text/javascript"" src=""https://code.jquery.com/jquery-3.4.1.min.js""></script>
<script type=""text/javascript"">
    // jQuery button click event to add a row.
    $(document).ready(function ($) {
        alert(");
#nullable restore
#line 165 "G:\silverproject\Admin.Web\Views\PurchaseOrder\Index.cshtml"
         Write(ViewBag.LatestId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@");
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();
        today = dd + '/' + mm + '/' + yyyy;
       
        $(""#POId"").val('SIH' + new Date().getUTCMilliseconds());
        $(""#Date"").val(today);
        $('#PaymentTerm').change(function () {
            debugger
            if ($('#PaymentTerm').val() == ""PDC"") {
                $(""#PaymentValue"").show();
                $(""#PaymentValue"").val(30);
            }
            else {
                $(""#PaymentValue"").hide();
                $(""#PaymentValue"").val(0);
            }
        });
        $('#DelieveryPlace').change(function () {
            debugger
            if ($('#DelieveryPlace').val() == ""Delivery"") {
                $(""#divaddress"").show();
            }
            else {
                $(""#divaddress"").hide();
            }
        });
        $");
            WriteLiteral(@"(""#Remarks1"").blur(function () {
            var tableBody = $(document).find('.classpo').find(""tbody"");
            var trLast = tableBody.find(""tr:last"");
            trLast.after(
                '<td><input type=""text"" id=""SrNo"" class=""form-control"" placeholder=""SrNo""></td>' +
                '<td><input type=""text"" id=""ItemName1"" class=""form-control"" placeholder=""ItemName""></td>' +
                '<td><input type=""text"" id=""Qty1"" class=""form-control"" placeholder=""Qty""></td>' +
                '<td><input type=""text"" id=""Unit1"" class=""form-control"" placeholder=""Unit""></td>' +
                '<td><input type=""text"" id=""BasicRate1"" class=""form-control"" placeholder=""BasicRate""></td>' +
                '<td><input type=""text"" id=""Freight1"" class=""form-control"" placeholder=""Freight""></td>' +
                '<td><input type=""text"" id=""CDC1"" class=""form-control"" placeholder=""CDC""></td>' +
                '<td><input type=""text"" id=""Discount1"" class=""form-control"" placeholder=""Discount""></td>' +
  ");
            WriteLiteral(@"              '<td><input type=""text"" id=""Scheme1"" class=""form-control"" placeholder=""Scheme""></td>' +
                '<td><input type=""text"" id=""Scheme11"" class=""form-control"" placeholder=""Scheme""></td>' +
                '<td><input type=""text"" id=""GSTRate1"" class=""form-control"" placeholder=""GSTRate""></td>' +
                '<td><input type=""text"" id=""NLC1"" class=""form-control"" placeholder=""NLC""></td>' +
                '<td><input type=""text"" id=""Remarks1"" class=""form-control"" placeholder=""Remark"" ></td>' +
                '<td><span class=""btn btn-sm btn-danger btn_row_delete"">Delete</span></td>'
            );
            i++;
        });
        $(document).on('click', "".btn_row_below_new"", function (e) {
            alert(""add 1"");
            $('#tblpo tbody>tr:last').after(
                '<td><input type=""text"" id=""SrNo1"" class=""form-control"" placeholder=""SrNo""></td>' +
                '<td><input type=""text"" id=""ItemName1"" class=""form-control"" placeholder=""ItemName""></td>' +
       ");
            WriteLiteral(@"         '<td><input type=""text"" id=""Qty1"" class=""form-control"" placeholder=""Qty""></td>' +
                '<td><input type=""text"" id=""Unit1"" class=""form-control"" placeholder=""Unit""></td>' +
                '<td><input type=""text"" id=""BasicRate1"" class=""form-control"" placeholder=""BasicRate""></td>' +
                '<td><input type=""text"" id=""Freight1"" class=""form-control"" placeholder=""Freight""></td>' +
                '<td><input type=""text"" id=""CDC1"" class=""form-control"" placeholder=""CDC""></td>' +
                '<td><input type=""text"" id=""Discount1"" class=""form-control"" placeholder=""Discount""></td>' +
                '<td><input type=""text"" id=""Scheme1"" class=""form-control"" placeholder=""Scheme""></td>' +
                '<td><input type=""text"" id=""Scheme11"" class=""form-control"" placeholder=""Scheme""></td>' +
                '<td><input type=""text"" id=""GSTRate1"" class=""form-control"" placeholder=""GSTRate""></td>' +
                '<td><input type=""text"" id=""NLC1"" class=""form-control"" placeholder=""NLC");
            WriteLiteral(@"""></td>' +
                '<td><input type=""text"" id=""Remarks1"" class=""form-control"" placeholder=""Remark"" onblur=""AddRow(2);""></td>' +
                '<td><a id=""add""><span class=""btn btn-sm btn-success btn-add-row"">Add New</span></a> | <span class=""btn btn-sm btn-danger btn_row_delete"">Delete</span></td>'
            );
            return false;
        });
        ////--->current row > new > start
        //$(document).on('click',"".btn_row_below_new"", function(e)
        //{
        //  var r = $(this).closest('tr').clone();

        //  $.each(r.find('td'), function(i1,v1)
        //  {
        //        //clear all data/value in td/cell
        //        $(this).html('');
        //  });
        //  $(this).closest('tr').after(r);
        //});
        ////--->current row > new > end
        //--->current row > clone > start
        $(document).on('click', "".btn_row_below_clone"", function (e) {
            var r = $(this).closest('tr').clone();
            $(this).closest('tr').aft");
            WriteLiteral(@"er(r);
        });
        //--->current row > clone > end
        //--->current row > delete > start
        $(document).on('click', "".btn_row_delete"", function (e) {
            var r = $(this).closest('tr').remove();
        });
        //--->current row > delete > end
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Command.Entity1.BillMaster> Html { get; private set; }
    }
}
#pragma warning restore 1591
