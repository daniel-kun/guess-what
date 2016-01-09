namespace Io.GuessWhat.MainApp.Models
{
    /**
    A ChecklistItem is an Item in the ChecklistModel (a checklist template).
    Currently, the ChecklistItem only consists of a description.
    **/
    public class ChecklistItem
    {
        public string Id
        {
            get;
            set;
        }

        /**
        This checklist item's title.
        The title is the text description of this checklist item.
        Currently there is no detailed description available, so it should be pretty
        explanatory about.
        **/
        public string Title
        {
            get;
            set;
        }
    }
}