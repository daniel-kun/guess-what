using System;
using System.Collections.Generic;
using System.Linq;

namespace Io.GuessWhat.MainApp.Models
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

        public static readonly int TitleMaxLength = 4000;
        public static readonly int DescriptionMaxLength = 4000;

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

        /**
        Creates a ChecklistModel object from a ChecklistViewModel. The multi-line text in
        viewModel.Items will be converted to a list of ChecklistItems in the resulting 
        ChecklistModel's Items.
        **/
        public static ChecklistModel FromViewModel(ChecklistViewModel viewModel)
        {
            return new ChecklistModel()
            {
                Title = Tools.Web.FormInput.PrepareText(viewModel.Title, ChecklistModel.TitleMaxLength),
                Description = Tools.Web.FormInput.PrepareText(viewModel.Description, ChecklistModel.DescriptionMaxLength),
                Items = ChecklistItem.FromText(viewModel.Items)
            };
        }

    }
}
