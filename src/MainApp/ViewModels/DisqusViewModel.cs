namespace Io.GuessWhat.MainApp.ViewModels
{
    /**
    The DisqusViewModel includes config variables that are required to
    integrate Disqus on a page.
    **/
    public class DisqusViewModel
    {
        public DisqusViewModel(string url, string id)
        {
            Url = url;
            Id = id;
        }

        /**
        The canonical URL of the page that Disqus is being integrated in.
        **/
        public string Url
        {
            get;
            set;
        }

        /**
        A unique id for the site. 
        **/
        public string Id
        {
            get;
            set;
        }
    }
}
