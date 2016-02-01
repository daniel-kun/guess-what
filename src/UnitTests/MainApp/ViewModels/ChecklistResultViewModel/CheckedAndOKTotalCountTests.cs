using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Io.GuessWhat.UnitTests.MainApp.ViewModels.ChecklistResultViewModel
{
    public class CheckedAndOkTotalCountTests
    {
        [Fact]
        public void ShouldCountItemsThatAreCheckedAndOk()
        {
            var sut = new GuessWhat.MainApp.ViewModels.ChecklistResultViewModel()
            {
                Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                }).ToList()
            };

            int result = sut.CheckedAndOkTotalCount;
            Assert.Equal(3, result);
        }

        [Fact]
        public void ShouldNotCountItemsThatAreNotCheckedOrNotOk()
        {
            var sut = new GuessWhat.MainApp.ViewModels.ChecklistResultViewModel()
            {
                Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                    GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                    GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                }).ToList()
            };

            int result = sut.CheckedAndOkTotalCount;
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
                        Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
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
                        Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
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
                        Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
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
                        Items = BuildDemoData(new List<GuessWhat.MainApp.Models.ChecklistResult>{
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndOk,
                            GuessWhat.MainApp.Models.ChecklistResult.CheckedAndNotOk,
                            GuessWhat.MainApp.Models.ChecklistResult.NotChecked,
                        }).ToList()
                    },
                }
            };

            int result = sut.CheckedAndOkTotalCount;
            Assert.Equal(2, result);
        }

        public static IEnumerable<GuessWhat.MainApp.ViewModels.ChecklistResultViewItem> BuildDemoData(IEnumerable<GuessWhat.MainApp.Models.ChecklistResult> results)
        {
            foreach (var result in results)
            {
                yield return new GuessWhat.MainApp.ViewModels.ChecklistResultViewItem()
                {
                    ResultItem = new GuessWhat.MainApp.Models.ChecklistResultItem()
                    {
                        Result = result
                    }
                };
            }
        }

    }
}
