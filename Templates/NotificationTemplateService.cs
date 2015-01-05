using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Razor.Parser;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace Templates
{
    public class NotificationTemplateService : ITemplateService
    {
        private readonly Dictionary<string, Type> _viewsRegistry = new Dictionary<string, Type>();
        private readonly IEncodedStringFactory _encodedStringFactory = new HtmlEncodedStringFactory();

        public void RegisterRazorEngineViews(Assembly assembly, string namespacePrefix)
        {
            // scanning the assembly for all public types derived from ITemplate and adding them into cache 
            var templateType = typeof(ITemplate);
            var views = assembly.GetExportedTypes().Where(x => templateType.IsAssignableFrom(x));

            namespacePrefix += ".";
            foreach (var view in views)
            {
                // full name of a class like Namespace.Prefix.Foo.Bar.Baz will be translated to foo_bar_baz which 
                // is the unique name for a template
                var name = ParserHelpers.SanitizeClassName(view.FullName.Replace(namespacePrefix, "")).ToLowerInvariant();
                _viewsRegistry.Add(name, view);
            }
        }

        public ITemplate Resolve(string templateUrl, object model)
        {
            var name = ConvertPathToName(templateUrl);
            if (_viewsRegistry.ContainsKey(name) == false)
            {
                var exception = new Exception(string.Format("Template not registered. Template url is '{0}', resolved name is '{1}'.",
                    templateUrl, name));
                throw exception;
            }

            var view = _viewsRegistry[name];
            
            var instance = (ITemplate)Activator.CreateInstance(view);
            instance.TemplateService = this;
            SetModel(instance, model);

            return instance;
        }

        public string Run(string templateUrl, object model, DynamicViewBag viewBag)
        {
            var template = Resolve(templateUrl, model);
            return template.Run(CreateExecuteContext(viewBag));
        }

        public ExecuteContext CreateExecuteContext(DynamicViewBag viewBag = null)
        {
            return new ExecuteContext(viewBag);
        }

        public IEncodedStringFactory EncodedStringFactory
        {
            get { return _encodedStringFactory; }
        }

        public void Dispose()
        {
            // nothing to do
        }

        #region Copied from RazorEngine's TemplateService

        private static void SetModel<T>(ITemplate template, T model)
        {
            if (model == null)
                return;

            var template1 = template as ITemplate<object>;
            if (template1 != null)
            {
                template1.Model = model;
            }
            else
            {
                var template2 = template as ITemplate<T>;
                if (template2 != null)
                {
                    template2.Model = model;
                }
                else
                {
                    SetModelExplicit(template, model);
                }
            }
        }

        private static void SetModelExplicit(ITemplate template, object model)
        {
            var property = template.GetType().GetProperty("Model");

            if (!(property != null))
                return;

            property.SetValue(template, model, null);
        }

        #endregion

        private string ConvertPathToName(string templatePath)
        {
            // this will translate path like ~/Foo/Bar/Baz.cshtml to foo_bar_baz which is the key we used to save view type in cache
            templatePath = templatePath
                .Replace("~/", "")
                .Replace("/", ".")
                .Replace(".cshtml", "");
            return ParserHelpers.SanitizeClassName(templatePath).ToLowerInvariant();
        }

        #region Stubs


        public void AddNamespace(string ns)
        {
            throw new NotImplementedException();
        }

        public void Compile(string razorTemplate, Type modelType, string cacheName)
        {
            throw new NotImplementedException();
        }

        public ITemplate CreateTemplate(string razorTemplate, Type templateType, object model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITemplate> CreateTemplates(IEnumerable<string> razorTemplates, IEnumerable<Type> templateTypes, IEnumerable<object> models, bool parallel = false)
        {
            throw new NotImplementedException();
        }

        public Type CreateTemplateType(string razorTemplate, Type modelType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> CreateTemplateTypes(IEnumerable<string> razorTemplates, IEnumerable<Type> modelTypes, bool parallel = false)
        {
            throw new NotImplementedException();
        }

        public ITemplate GetTemplate(string razorTemplate, object model, string cacheName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITemplate> GetTemplates(IEnumerable<string> razorTemplates, IEnumerable<object> models, IEnumerable<string> cacheNames, bool parallel = false)
        {
            throw new NotImplementedException();
        }

        public bool HasTemplate(string cacheName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTemplate(string cacheName)
        {
            throw new NotImplementedException();
        }

        public string Parse(string razorTemplate, object model, DynamicViewBag viewBag, string cacheName)
        {
            throw new NotImplementedException();
        }

        public string Parse<T>(string razorTemplate, object model, DynamicViewBag viewBag, string cacheName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ParseMany(IEnumerable<string> razorTemplates, IEnumerable<object> models, IEnumerable<DynamicViewBag> viewBags, IEnumerable<string> cacheNames,
            bool parallel)
        {
            throw new NotImplementedException();
        }


        public string Run(ITemplate template, DynamicViewBag viewBag)
        {
            throw new NotImplementedException();
        }

        public ITemplateServiceConfiguration Configuration
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}