using System;
using Templates;
using Templates.Models;
using Templates.Views.Welcome;

namespace RazorEngineGeneratorSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var templateService = new NotificationTemplateService();
            templateService.RegisterRazorEngineViews(typeof(ViewPage1).Assembly, typeof(ViewPage1).Assembly.GetName().Name);

            var content = templateService.Run("~/Views/Welcome/ViewPage1.cshtml", new HelloViewModel
            {
                FirstName = "Peter",
                LastName = "Omaz"
            }, null);

            Console.WriteLine(content);
            Console.ReadKey();
        }
    }
}
