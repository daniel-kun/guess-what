using System;

namespace Io.GuessWhat.MainApp.Models
{
    /**
    A trimmed down version of ChecklistModel which does only contain the
    properties that are needed in the browse page.
    **/
    public class ChecklistBrowseItem
    {
        /// @see ChecklistModel.Id
        public string Id
        {
            get;
            set;
        }

        /// @see ChecklistModel.Title
        public string Title
        {
            get;
            set;
        }

        /// @see ChecklistModel.CreationTime
        public DateTime CreationTime
        {
            get;
            set;
        }

    }
}
