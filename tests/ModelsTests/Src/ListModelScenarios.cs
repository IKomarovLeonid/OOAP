using System.Collections.Generic;
using FluentAssertions;
using ListModel;
using Xbehave;

namespace ModelsTests
{
    public class ListModelScenarios
    {
        // remove operation has changed

        [Scenario]
        public void CanRemoveFirstItem()
        {
            ListModel<int> list = null;

            "Given non empty single model list"
                .x(() =>
                {
                    list = new ListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor in head"
                .x(() =>
                {
                    list.CursorToHead();
                });

            "When user tries to remove head item"
                .x(() =>
                {
                    list.RemoveItem();
                });

            "Then system must return head item and set cursor to new head"
                .x(() =>
                {
                    var items = list.GetItems();
                    var isSet = list.IsCursorSet();
                    var item = list.GetItem();

                    var expected = new List<int>() { 2, 3, 4, 5 };

                    isSet.Should().BeTrue();
                    item.Should().Be(2);
                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void CanRemoveMiddleItem()
        {

            ListModel<int> list = null;

            "Given non empty single model list"
                .x(() =>
                {
                    list = new ListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor set in item 4"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(4);
                });

            "When user tries to remove current item"
                .x(() =>
                {
                    list.RemoveItem();
                });

            "Then system must return head item and set cursor to new head"
                .x(() =>
                {
                    var items = list.GetItems();
                    var isSet = list.IsCursorSet();
                    var item = list.GetItem();

                    var expected = new List<int>() { 1, 2, 3, 5 };

                    isSet.Should().BeTrue();
                    item.Should().Be(5);
                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void CanRemoveLastItem()
        {

            ListModel<int> list = null;

            "Given non empty single model list"
                .x(() =>
                {
                    list = new ListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor set in item 5"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(5);
                });

            "When user tries to remove current item"
                .x(() =>
                {
                    list.RemoveItem();
                });

            "Then system must return head item and set cursor to new head"
                .x(() =>
                {
                    var items = list.GetItems();
                    var isSet = list.IsCursorSet();
                    var item = list.GetItem();

                    var expected = new List<int>() { 1, 2, 3, 4 };

                    isSet.Should().BeTrue();
                    item.Should().Be(4);
                    items.Should().BeEquivalentTo(expected);
                });
        }
    }
}
