namespace Templates
{
    public class TestTemplateBase<T> : RazorEngine.Templating.TemplateBase<T>
    {
        public string HelloFromTemplate()
        {
            return "hello from template base";
        }
    }

    public class TestTemplateBase : RazorEngine.Templating.TemplateBase
    {
        public string HelloFromTemplate()
        {
            return "hello from template base";
        }
    }
}