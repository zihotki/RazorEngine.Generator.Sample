//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Templates.Views.Welcome {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using Templates.Models;
    using Templates;
    
    
    public class ViewPage1 : Templates.TestTemplateBase2<HelloViewModel> {
        
#line hidden
        
        public ViewPage1() {
        }
        
        public override void Execute() {
  
    Layout = "~/Views/Shared/_DefaultLayout.cshtml";

WriteLiteral("\r\n\r\n<p>Hello ");

    Write(Model.FirstName);

WriteLiteral(" ");

                     Write(Model.LastName);

WriteLiteral(" from view page</p>\r\n\r\n");

Write(HelloFromTemplate());

        }
    }
}
