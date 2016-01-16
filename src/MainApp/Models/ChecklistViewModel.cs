namespace Io.GuessWhat.MainApp.Models
{
    /**
    Represents a trimmed down version of the ChecklistModel that is used in the New view.
    **/
    public class ChecklistViewModel
    {
        /**
        Maximum text length for the Items property.
        **/
        public static readonly int ItemsMaxLength = 4000;

        /// @see ChecklistModel.Title
        public string Title
        {
            get;
            set;
        }

        /// @see ChecklistModel.Description
        public string Description
        {
            get;
            set;
        }

        /*
        Stores a multi-line text representation of @see ChecklistModel.Items.
        See ChecklistController.CreateChecklistItemsFromText for the rules how this Items
        string will be converted to ChecklistModel.Items.
        */
        public string Items
        {
            get;
            set;
        }
    }
}
