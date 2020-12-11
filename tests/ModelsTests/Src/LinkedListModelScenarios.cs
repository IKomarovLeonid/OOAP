using System;
using System.Collections.Generic;
using ListModel;
using FluentAssertions;
using ListModel.Statuses;
using Xbehave;

namespace ModelsTests
{
    public class LinkedListModelScenarios
    {
        [Scenario]
        public void When_ListModelIsCreatedCursor_IsNot_Set()
        {
            LinkedListModel<int> list = null;

            "When list model has been created"
                .x(() =>
                {
                    list = new LinkedListModel<int>();
                });

            "Then cursor should not be set"
                .x(() =>
                {
                    var isSet = list.IsCursorSet();

                    isSet.Should().BeFalse();
                });
        }

        [Scenario]
        public void When_UserTriesToSetCursorToHeadOfNotEmptyList_Must_BeSet()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(10);
                });

            "When user tries to set cursor to head of such list"
                .x(() =>
                {
                    list.CursorToHead();
                });

            "Then cursor must be in head of list"
                .x(() =>
                {
                    var isInHead = list.IsCursorInHead();

                    isInHead.Should().BeTrue();
                });
        }

        [Scenario]
        public void When_ListIsEmptyCursor_CanNot_BeSetInHead()
        {
            LinkedListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new LinkedListModel<int>();
                });

            "When user tries to set cursor to head of such list"
                .x(() =>
                {
                    list.CursorToHead();
                });

            "Then cursor must be not in head and not be set"
                .x(() =>
                {
                    var isInHead = list.IsCursorInHead();
                    var isSet = list.IsCursorSet();

                    isInHead.Should().BeFalse();
                    isSet.Should().BeFalse();
                });
        }

        [Scenario]
        public void When_UserTriesToSetCursorToTailOfNotEmptyList_Must_BeSet()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(10);
                });

            "When user tries to set cursor to tail of such list"
                .x(() =>
                {
                    list.CursorToTail();
                });

            "Then cursor must be in tail of list"
                .x(() =>
                {
                    var isInHead = list.IsCursorInTail();

                    isInHead.Should().BeTrue();
                });
        }

        [Scenario]
        public void When_ListIsEmptyCursor_CanNot_BeSetInTail()
        {
            LinkedListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new LinkedListModel<int>();
                });

            "When user tries to set cursor to tail of such list"
                .x(() =>
                {
                    list.CursorToHead();
                });

            "Then cursor must be not in tail and not be set"
                .x(() =>
                {
                    var isInTail = list.IsCursorInTail();
                    var isSet = list.IsCursorSet();

                    isInTail.Should().BeFalse();
                    isSet.Should().BeFalse();
                });
        }

        [Scenario]
        public void When_ListNotEmptyAndCursorIsNotInTailMoveNext_SetCursoreToNextNode()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor is set in value 3"
                .x(() =>
                {
                    // set cursor and move to 3 item
                    list.CursorToHead();
                    list.SetToNextSameItem(3);
                });

            "When user tries to move cursor next"
                .x(() =>
                {
                    list.MoveCursorNext();
                });

            "Then cursor must be indicate to item with value 4"
                .x(() =>
                {
                    var item = list.GetItem();

                    item.Should().Be(4);
                });
        }

        [Scenario]
        public void When_CursorIsInTailMoveNext_DoNotMoveCursor()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor is set in tail"
                .x(() =>
                {
                    // set cursor to tail
                    list.CursorToTail();
                });

            "When user tries to move cursor next"
                .x(() =>
                {
                    list.MoveCursorNext();
                });

            "Then cursor must be still indicate to tail item because next node are not exists"
                .x(() =>
                {
                    var isInTail = list.IsCursorInTail();
                    var item = list.GetItem();

                    item.Should().Be(5);
                    isInTail.Should().BeTrue();
                });
        }

        [Scenario]
        public void When_CursorIsNotInTailAndMoveNextFuncTakeCursorToTailCursor_Must_BeInTail()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor is set in item with value 4"
                .x(() =>
                {
                    // set cursor to tail
                    list.CursorToHead();
                    list.SetToNextSameItem(4);
                });

            "When user tries to move cursor next"
                .x(() =>
                {
                    list.MoveCursorNext();
                });

            "Then cursor must indicates to value 5 and be in tail"
                .x(() =>
                {
                    var item = list.GetItem();
                    var isInTail = list.IsCursorInTail();

                    item.Should().Be(5);
                    isInTail.Should().BeTrue();
                });
        }

        [Scenario]
        public void When_ListIsEmptyAndCursorIsNotSetMoveNext_Not_ChangeCursor()
        {
            LinkedListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                });

            "When user tries to move cursor next"
                .x(() =>
                {
                    list.MoveCursorNext();
                });

            "Then no any affection should happened for cursor"
                .x(() =>
                {
                    var item = list.GetItem();
                    var isInHead = list.IsCursorInHead();
                    var isInTail = list.IsCursorInTail();
                    var hasItem = list.IsCursorSet();

                    item.Should().Be(default);
                    isInTail.Should().BeFalse();
                    isInHead.Should().BeFalse();
                    hasItem.Should().BeFalse();
                });
        }

        [Scenario]
        public void When_ListIsNotEmptyAndCursorNotInTailPlaceItemNext_Must_SetItem()
        {
            LinkedListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor is set on item 3"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(3);
                });

            "When user tries to place 10 after current cursor"
                .x(() =>
                {
                    list.PutNext(10);
                });

            "Then item must be placed after 3"
                .x(() =>
                {
                    var items = list.GetItems();

                    var expected = new List<int>() { 1, 2, 3, 10, 4, 5 };

                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void WhenAddNextInTail_Must_BeAddedToTail()
        {
            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor in tail"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(5);
                });

            "When user tries to add new item next"
                .x(() =>
                {
                    list.PutNext(10);
                });

            "Then system must place new item in tail"
                .x(() =>
                {
                    var items = list.GetItems();

                    var expected = new List<int>(){1,2,3,4,5,10};

                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void When_UserTriesToSetLeftFromHead_Must_BePlaced()
        {
            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

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

            "When user tries to add item before head"
                .x(() =>
                {
                    list.PutBefore(10);
                });

            "Then system must place new item before head"
                .x(() =>
                {
                    var items = list.GetItems();

                    var expected = new List<int>() {10 ,1, 2, 3, 4, 5};

                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void When_UserTriesToSetLeftBeforeNotHeadAndListIsNotEmptyMustBeAdded()
        {
            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor is in 3 item"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(3);
                });

            "When user tries to add item before head"
                .x(() =>
                {
                    list.PutBefore(10);
                });

            "Then system must place new item before item 3"
                .x(() =>
                {
                    var items = list.GetItems();

                    var expected = new List<int>() { 1, 2, 10, 3, 4, 5 };

                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void CanRemoveFirstItem()
        {

            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

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

            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

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

            LinkedListModel<int> list = null;

            "Given non empty model list"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

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

        [Scenario]
        public void CanRemoveFewItems()
        {
            LinkedListModel<int> list = null;

            "Given non empty model list with several items which are equal"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(5);
                    list.AddInTail(5);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "And cursor set in item first item"
                .x(() =>
                {
                    list.CursorToHead();
                    list.SetToNextSameItem(5);
                });

            "When user tries to remove all items equal to 5"
                .x(() =>
                {
                    list.RemoveAllSameItems(5);
                });

            "Then system must remove all items equal to 5 from list"
                .x(() =>
                {
                    var items = list.GetItems();
                    var isSet = list.IsCursorSet();
                    var item = list.GetItem();

                    var expected = new List<int>() { 1, 4 };

                    isSet.Should().BeTrue();
                    item.Should().Be(4);
                    items.Should().BeEquivalentTo(expected);
                });
        }

        [Scenario]
        public void CanRemoveAllItems()
        {
            LinkedListModel<int> list = null;

            "Given non empty model list with all items equal"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(5);
                    list.AddInTail(5);
                    list.AddInTail(5);
                    list.AddInTail(5);
                    list.AddInTail(5);
                });

            "And cursor set in head"
                .x(() =>
                {
                    list.CursorToHead();
                });

            "When user tries to remove all items equal to 5"
                .x(() =>
                {
                    list.RemoveAllSameItems(5);
                });

            "Then system must remove all items in list"
                .x(() =>
                {
                    var status = list.LastOperationStatus();
                    var items = list.GetItems();
                    var expected = new List<int>(){};

                    items.Should().BeEquivalentTo(expected);
                    status.Should().Be(OperationStatus.Ok);
                });
        }

        /// <summary>
        /// 0 -> move next
        /// 1 -> move prev
        /// 2 -> remove item
        /// 3 -> remove all items
        /// 4 -> set to next same item
        /// </summary>
        /// <param name="method"></param>
        [Scenario]
        [Example(0)]
        [Example(1)]
        [Example(2)]
        [Example(3)]
        [Example(4)]
        public void WhenCursorIsNotSetListCanNotBeModified(int method)
        {
            LinkedListModel<int> list = null;

            "Given non empty model list with items and user don't set cursor"
                .x(() =>
                {
                    list = new LinkedListModel<int>();

                    list.AddInTail(1);
                    list.AddInTail(2);
                    list.AddInTail(3);
                    list.AddInTail(4);
                    list.AddInTail(5);
                });

            "When user tries to produce any list method without specific set of cursor"
                .x(() =>
                {
                    switch (method)
                    {
                        case 0:
                            list.MoveCursorNext();
                            break;
                        case 1:
                            list.MoveCursorPrevious();
                            break;
                        case 2:
                            list.RemoveItem();
                            break;
                        case 3:
                            list.RemoveAllSameItems(4);
                            break;
                        case 4:
                            list.SetToNextSameItem(3); 
                            break;
                    }
                });

            "Then system must not change list and set last operation status to cursor not set"
                .x(() =>
                {
                    var items = list.GetItems();
                    var status = list.LastOperationStatus();

                    var expected = new List<int>() {1, 2, 3, 4, 5 };

                    items.Should().BeEquivalentTo(expected);
                    status.Should().Be(OperationStatus.CursorNotSet);
                });
        }
    }
}
