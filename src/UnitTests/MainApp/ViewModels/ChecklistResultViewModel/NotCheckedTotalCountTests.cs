using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.ViewModels.ChecklistResultViewModel
{
    public class NotCheckedTotalCountTests
    {
        [Fact]
        public void ShouldCountItemsThatAreNotChecked()
        {
            var sut = new GuessWhat.MainApp.ViewModels.ChecklistResultViewModel()
            {
                Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                }).ToList()
            };

            int result = sut.NotCheckedTotalCount;
            Assert.Equal(3, result);
        }

        [Fact]
        public void ShouldNotCountItemsThatAreOkOrNotOk()
        {
            var sut = new GuessWhat.MainApp.ViewModels.ChecklistResultViewModel()
            {
                Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                }).ToList()
            };

            int result = sut.NotCheckedTotalCount;
            Assert.Equal(2, result);
        }

        [Fact]
        public void ShouldCountChildItemsOnlyWhenParentItemIsOk()
        {
            var sut = new GuessWhat.MainApp.ViewModels.ChecklistResultViewModel()
            {
                Items = new List<GuessWhat.MainApp.ViewModels.ChecklistResultViewItem>
                {
                    new GuessWhat.MainApp.ViewModels.ChecklistResultViewItem ()
                    {
                        ResultItem = new GuessWhat.MainApp.Models.ChecklistResultItem ()
                        {
                           Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                        },
                        Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                            GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                        }).ToList()
                    },
                    new GuessWhat.MainApp.ViewModels.ChecklistResultViewItem ()
                    {
                        ResultItem = new GuessWhat.MainApp.Models.ChecklistResultItem ()
                        {
                           Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk
                        },
                        Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                            GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                        }).ToList()
                    },
                    new GuessWhat.MainApp.ViewModels.ChecklistResultViewItem ()
                    {
                        ResultItem = new GuessWhat.MainApp.Models.ChecklistResultItem ()
                        {
                           Result = GuessWhat.MainApp.Models.ChecklistResult.NotChecked // This should not be possible
                        },
                        Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                            GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                        }).ToList()
                    },
                    new GuessWhat.MainApp.ViewModels.ChecklistResultViewItem ()
                    {
                        ResultItem = new GuessWhat.MainApp.Models.ChecklistResultItem ()
                        {
                           Result = GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk
                        },
                        Items = CheckedAndOkTotalCountTests.BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                            GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                        }).ToList()
                    },
                }
            };

            int result = sut.NotCheckedTotalCount;
            Assert.Equal(2, result);
        }

    }
}
