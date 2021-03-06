﻿using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.ViewModels.ChecklistViewModel
{
    public class FromModelTests
    {
        [Fact]
        public void ShouldPreserveTitleAndDescription()
        {
            var test = new GuessWhat.MainApp.Models.ChecklistModel() {
                Title = "asdf-ghijkl",
                Description = @"C arrays are less safe, and have no 
advantages over array and vector. For a fixed-length array, use 
std::array, which does not degenerate to a pointer when passed to a function and 
does know its size. For a variable-length array, use std::vector, which additionally 
can change its size and handles memory allocation.",
            };
            var result = GuessWhat.MainApp.ViewModels.ChecklistViewModel.FromModel(test);
            Assert.Equal(test.Title, result.Title);
            Assert.Equal(test.Description, result.Description);
        }

        [Fact]
        public void ShouldPreserveItemsAsMultiLineText()
        {
            var test = new GuessWhat.MainApp.Models.ChecklistModel()
            {
                Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                {
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "Some checklist item",
                    },
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "Some other checklist item",
                    },
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "Yup, another checklist item",
                    },
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "And the last checklist item",
                    },
                },
            };
            var result = GuessWhat.MainApp.ViewModels.ChecklistViewModel.FromModel(test);
            var expected = new StringBuilder();
            expected.AppendLine(test.Items[0].Title);
            expected.AppendLine(test.Items[1].Title);
            expected.AppendLine(test.Items[2].Title);
            expected.AppendLine(test.Items[3].Title);
            Assert.Equal(result.Items, expected.ToString());
        }

        [Fact]
        public void ShouldAddNewLineAfterLastItem()
        {
            var test = new GuessWhat.MainApp.Models.ChecklistModel()
            {
                Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                {
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "Last checklist item",
                    },
                },
            };
            var result = GuessWhat.MainApp.ViewModels.ChecklistViewModel.FromModel(test);
            Assert.Equal(result.Items, test.Items[0].Title + "\r\n");
        }

        [Fact]
        public void ShouldPreserveChildItemsWithOneSpaceIndent()
        {
            var test = new GuessWhat.MainApp.Models.ChecklistModel()
            {
                Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                {
                    new GuessWhat.MainApp.Models.ChecklistItem()
                    {
                        Title = "Last checklist item",
                        Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                        {
                            new GuessWhat.MainApp.Models.ChecklistItem ()
                            {
                                Title = "Child Item #1",
                            },
                            new GuessWhat.MainApp.Models.ChecklistItem ()
                            {
                                Title = "Child Item #2",
                                Items = new List<GuessWhat.MainApp.Models.ChecklistItem>
                                {
                                    new GuessWhat.MainApp.Models.ChecklistItem ()
                                    {
                                        Title = "Child Item #2.1"
                                    }
                                }
                            },
                        }
                    },
                },
            };

            var result = GuessWhat.MainApp.ViewModels.ChecklistViewModel.FromModel(test);

            Assert.Equal(@"Last checklist item
 Child Item #1
 Child Item #2
  Child Item #2.1
", result.Items);
        }

        [Fact]
        public void ItemsShouldBeCompletelyEmptyStringWhenThereAreNoItems()
        {
            var test = new GuessWhat.MainApp.Models.ChecklistModel()
            {
                Items = new List<GuessWhat.MainApp.Models.ChecklistItem>()
            };

            var result = GuessWhat.MainApp.ViewModels.ChecklistViewModel.FromModel(test);

            Assert.Equal(string.Empty, result.Items);
        }
    }
}
