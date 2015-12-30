using System.Collections.Generic;

namespace guess_what2.Models
{
    /**
    @brief The ChecklistModel contains every info regarding a single checklist template.
    This is:
    - The checklists's title
    - The author, time published, etc.
    - The checklist items (ChecklistItem)
    **/
    public class ChecklistModel
    {
        public string Id
        {
            get;
            set;
        }

        /**
        This checklist's title / headline.
        The title is displayed on top of the checklist in a separate heading and should
        be a very short description of this checklist's purpose.
        **/
        public string Title
        {
            get;
            set;
        }

        /**
        This checklist's detailed description.
        The description is displayed below the Title and should include detailed information
        about when, why and how this checklist is to be filled out.
        **/
        public string Description
        {
            get;
            set;
        }

        /**
        This checklist's checkable items.
        **/
        public List<ChecklistItem> Items
        {
            get;
            set;
        }
    }
}
