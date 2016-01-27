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
        public ChecklistItem CloneWithMutator (Func<ChecklistItem, ChecklistItem> mutator)
        {
            var result = new ChecklistItem();
            result.Id = Id;
            result.Title = Title;
            if (Items != null)
            {
                result.Items = Items.Select(item => item.CloneWithMutator(mutator)).ToList();
            }
            mutator(result);
            return result;
        }

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
        If Items is non-null and not empty, this ChecklistItem is a "parent" item that
        can only be "applicable" (ChecklistResult.CheckedAndOk) or "not applicable" (ChecklistResult.NotChecked).
        If this item is applicable, then it's sub-`Items` are expanded and can be checked
        to any of the three states (that means, they can be ChecklistResult.CheckedAndNotOk, too,
        like 'normal' ChecklistItems).
        **/
        public List<ChecklistItem> Items
        {
            get;
            set;
        }

        private struct ItemHierarchy
        {
            public int indent;
            public ChecklistItem item;
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
            var indents = new Stack<ItemHierarchy>();
            var rootItem = new ItemHierarchy
            {
                indent = 0,
                item = new ChecklistItem()
                {
                    Items = new List<ChecklistItem>()
                }
            };
            indents.Push(rootItem);
            FromTextImpl(lines, 0, indents);
            return rootItem.item.Items;
        }

        private static void FromTextImpl(string[] lines, int startIndex, Stack<ItemHierarchy> indents)
        {
            for (int i = startIndex; i < lines.Length; ++i)
            {
                string line = lines[i];
                ItemHierarchy currentItem = indents.Peek();
                int currentIndent = currentItem.indent;
                int lineIndent = FindFirstNonWhitespace(line);
                if (lineIndent == currentIndent || 
                    (indents.Count == 1 && 
                    (currentItem.item.Items == null || currentItem.item.Items.Count == 0)))
                {
                    // Same indentation depths, or there are no lines before this one:
                    string trimmed = line.Trim();
                    if (trimmed.Length > 0)
                    {
                        if (currentItem.item.Items == null)
                        {
                            currentItem.item.Items = new List<ChecklistItem>();
                        }
                        currentItem.item.Items.Add(new ChecklistItem() { Title = trimmed });
                    }
                }
                else if (lineIndent > currentIndent)
                {
                    // Create child nodes:
                    var newIndentItem = new ItemHierarchy()
                    {
                        indent = lineIndent,
                        item = currentItem.item.Items[currentItem.item.Items.Count - 1]
                    };
                    indents.Push(newIndentItem);
                    FromTextImpl(lines, i, indents);
                    break;
                }
                else {
                    // Indentation is less deep than previous line -
                    // find the corresponding parent item this line should be
                    // a child node of.
                    ItemHierarchy newIndentItem;
                    do
                    {
                        if (indents.Count == 1)
                        {
                            newIndentItem = indents.Peek();
                        }
                        else
                        {
                            newIndentItem = indents.Pop();
                        }
                    } while (indents.Count > 1);
                    FromTextImpl(lines, i, indents);
                    break;
                }
            }
        }

        private static int FindFirstNonWhitespace(string line)
        {
            for (int i = 0; i < line.Length; ++i)
            {
                if (line [i] != ' ')
                {
                    return i;
                }
            }
            return line.Length;
        }
    }
}