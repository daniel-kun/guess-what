using Io.GuessWhat.MainApp.Models;

namespace Io.GuessWhat.MainApp.ViewModels
{
    /**
    The ChecklistViewItem adds some view-data to the ChecklistItem.
    **/
    public class ChecklistViewItem
    {
        /**
        For hierarchical checklists: Contains the id of the parent ChecklistItem,
        if this is a child item. Is null or string.Empty when it is not a child item.
        **/
        public string ParentId
        {
            get;
            set;
        }

        /**
        The original ChecklistItem that these view-infos correspond to.
        Not null.
        **/
        public ChecklistItem Item
        {
            get;
            set;
        }

        /**
        The level of indentation. This is 0 for top-level ChecklistItems, 1 for
        the children of top-level ChecklistItems, 2 for the children of children, and so on.
        **/
        public int IndentationLevel
        {
            get;
            set;
        } = 0;
    }
}
