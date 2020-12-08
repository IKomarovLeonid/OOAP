using ListModel;
using FluentAssertions;
using Xbehave;

namespace ModelsTests
{
    public class ListModelScenarios
    {
        [Scenario]
        public void When_ListModelIsCreatedCursor_IsNot_Set()
        {
            ListModel<int> list = null;

            "When list model has been created"
                .x(() =>
                {
                    list = new ListModel<int>();
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
            ListModel<int> list = null;

            "Given not empty list model"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new ListModel<int>();
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
            ListModel<int> list = null;

            "Given not empty list model"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new ListModel<int>();
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
            ListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given empty list model"
                .x(() =>
                {
                    list = new ListModel<int>();

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
            ListModel<int> list = null;

            "Given not empty list model with items 1 2 3 4 5"
                .x(() =>
                {
                    list = new ListModel<int>();

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
                    var size = list.Size();

                    size.Should().Be(6);
                });
        }
    }
}
