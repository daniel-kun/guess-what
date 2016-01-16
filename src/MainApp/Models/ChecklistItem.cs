using System;
using System.Collections.Generic;
using System.Linq;

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

        /**
        @brief Converts a string with 0-n lines of text into a list of ChecklistItems.
        The text in each line will be used as the ChecklistItem's titles.

        Lines that contain only whitespace will be ignored. Newline delimiter are \r and \n (in
        any order and combination).

        @param items The multiline text that will be converted to a list of ChecklistItems.
        @return A list of ChecklistItems with the Titles specified in items.
        **/
        public static List<ChecklistItem> FromText(string items)
        {
            string[] lines = items.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var result = lines.Select(
                line =>
                {
                    if (line.Count(c => !Char.IsWhiteSpace(c)) == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return new ChecklistItem() { Title = line };
                    }
                });
            var list = result.Where(line => line != null).ToList();
            return list;
        }

    }
}