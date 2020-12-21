using System.Collections.Generic;
using FluentAssertions;
using NativeDictionary;
using NativeDictionary.Enums;
using Xbehave;

namespace ModelsTests
{
    public class DictionaryModelScenarios
    {
        [Scenario]
        public void PutAndRemoveOperationsOfNotInitializedDictionaryReturnsOperationCodeNotInitialized()
        {
            NativeDictionaryModel<int> dict = null;
            OperationCode putStatus = OperationCode.Ok, removeStatus = OperationCode.Ok;

            "Given not initialized dictionary"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<int>();
                }));

            "When user tries to put new item and remove"
                .x(() =>
                {
                    dict.Put("Item", 2);
                    putStatus = dict.LastPutStatus();

                    dict.Remove("Item");
                    removeStatus = dict.LastRemoveStatus();
                });

            "Then last put and remove statuses should be 'Not initialized'"
                .x(() =>
                {
                    putStatus.Should().Be(OperationCode.NotInitialized);
                    removeStatus.Should().Be(OperationCode.NotInitialized);
                });
        }


        [Scenario]
        public void User_CanAddItemWithUniqueKey()
        {
            NativeDictionaryModel<int> dict = null;

            "Given initialized dictionary"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<int>();
                    dict.Initialize(20);
                }));

            "When user tries to put new item with unique key"
                .x(() =>
                {
                    dict.Put("Item", 2);
                });

            "Then new item should be added"
                .x(() =>
                {
                    var items = dict.GetItems();
                    items.Should().BeEquivalentTo(new List<int>() {2});
                    // status
                    var status = dict.LastPutStatus();
                    status.Should().Be(OperationCode.Ok);
                });
        }

        [Scenario]
        public void User_CanNotAddItemWithNotUniqueKey()
        {
            NativeDictionaryModel<int> dict = null;

            "Given initialized dictionary with key = 'key' and item 10"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<int>();
                    dict.Initialize(20);
                    dict.Put("key", 10);
                }));

            "When user tries to put new item with same key 'key'"
                .x(() =>
                {
                    dict.Put("key", 20);
                });

            "Then new item should not be added and status should be 'key already exists'"
                .x(() =>
                {
                    var items = dict.GetItems();
                    items.Should().BeEquivalentTo(new List<int>() { 10 });
                    // status
                    var status = dict.LastPutStatus();
                    status.Should().Be(OperationCode.KeyAlreadyExists);
                });
        }

        [Scenario]
        public void User_CanGetItemByExistsKey()
        {
            NativeDictionaryModel<int> dict = null;
            var item = 0;

            "Given initialized dictionary with key = 'key' and item 10"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<int>();
                    dict.Initialize(20);
                    dict.Put("key", 10);
                }));

            "When user tries to get item by key 'key'"
                .x(() =>
                {
                    item = dict.GetItem("key");
                });

            "Then dictionary must return item 10"
                .x(() =>
                {
                    item.Should().Be(10);
                });
        }

        [Scenario]
        public void User_ReceivesDefaultValueWhenKeyIsNotExists()
        {
            NativeDictionaryModel<string> dict = null;
            string item = null;

            "Given initialized dictionary with key = 'key' and item 10"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                }));

            "When user tries to get item by key 'key 2'"
                .x(() =>
                {
                    item = dict.GetItem("key 2");
                });

            "Then dictionary must return default item"
                .x(() =>
                {
                    item.Should().Be(default);
                });
        }

        [Scenario]
        public void User_CanUpdateItemByExistedKey()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with key = 'key' and item 10"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                }));

            "When user tries to update item with key 'key' to 'Updated item'"
                .x(() =>
                {
                    dict.Update("key", "Updated item");
                });

            "Then dictionary must have value 'Updated item' with key 'key'"
                .x(() =>
                {
                    var item = dict.GetItem("key");

                    item.Should().Be("Updated item");

                    var status = dict.LastUpdateStatus();
                    status.Should().Be(OperationCode.Ok);
                });
        }

        [Scenario]
        public void User_CanNotUpdateItemByNotExistedKey()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with key = 'key' and item 10"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                }));

            "When user tries to update item with key 'key 2' to 'Updated item'"
                .x(() =>
                {
                    dict.Update("key 2", "Updated item");
                });

            "Then dictionary must not provide any changes and set last update status to 'key not found'"
                .x(() =>
                {
                    var status = dict.LastUpdateStatus();
                    status.Should().Be(OperationCode.KeyNotFound);
                });
        }

        [Scenario]
        public void When_UserUpdatesItemSizeDoesNotChanges()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with items count equal to 3"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                    dict.Put("key 2", "10");
                    dict.Put("key 3", "10");
                }));

            "When user tries to update item with key 'key 2' to 'Updated item'"
                .x(() =>
                {
                    dict.Update("key 2", "Updated item");
                });

            "Then dictionary must still have 3 items count"
                .x(() =>
                {
                    var size = dict.Size();
                    var items = dict.GetItems();

                    size.Should().Be(3);
                    items.Should().BeEquivalentTo(new List<string>(){"10", "Updated item", "10"});
                });
        }

        [Scenario]
        public void When_UserAddsNewUniqueKeyAndItemSizeCountIncreasesByOne()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with items count equal to 3"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                    dict.Put("key 2", "10");
                    dict.Put("key 3", "10");
                }));

            "When user add new unique key with item"
                .x(() =>
                {
                    dict.Put("key 4", "Updated item 5");
                });

            "Then dictionary must have 4 items count"
                .x(() =>
                {
                    var size = dict.Size();
                    var items = dict.GetItems();

                    size.Should().Be(4);
                    items.Should().BeEquivalentTo(new List<string>() { "10", "10", "10", "Updated item 5" });
                });
        }

        [Scenario]
        public void When_UserRemovesExistedKeySizeMustDecreaseByOne()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with items count equal to 3"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                    dict.Put("key 2", "10");
                    dict.Put("key 3", "10");
                }));

            "When user removes item with key 'key 2'"
                .x(() =>
                {
                    dict.Remove("key 2");
                });

            "Then dictionary must have 2 items count"
                .x(() =>
                {
                    var size = dict.Size();
                    var items = dict.GetItems();

                    size.Should().Be(2);
                    items.Should().BeEquivalentTo(new List<string>() { "10", "10"});
                });
        }

        [Scenario]
        public void When_UserRemovesItemDict_NoLongerContainsSuchKey()
        {
            NativeDictionaryModel<string> dict = null;
            string item = null;

            "Given initialized dictionary with items count equal to 3"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(20);
                    dict.Put("key", "10");
                    dict.Put("key 2", "10");
                    dict.Put("key 3", "10");
                }));

            "When user removes item with key 'key 2'"
                .x(() =>
                {
                    dict.Remove("key 2");
                });
            "And tries to get item with key equal to 'key 2'"
                .x(() =>
                {
                    item = dict.GetItem("key 2");
                });

            "Then received item should be equal to default value"
                .x(() =>
                {
                    item.Should().Be(default);
                });
        }

        [Scenario]
        public void When_DictHasNoLongerSpaceItCanResize()
        {
            NativeDictionaryModel<string> dict = null;

            "Given initialized dictionary with capacity 3 and items count 2"
                .x((() =>
                {
                    dict = new NativeDictionaryModel<string>();
                    dict.Initialize(3);
                    dict.Put("key", "10");
                    dict.Put("key 2", "10");
                }));

            "When user adds new 15 items"
                .x(() =>
                {
                    int index = 0;
                    while (index < 15)
                    {
                        dict.Put(index.ToString(), index + "item");
                        index++;
                    }
                });

            "Then dictionary should be resized and has items count equal to 17"
                .x(() =>
                {
                    var count = dict.Size();
                    var item = dict.GetItem("14");

                    count.Should().Be(17);
                    item.Should().Be("14item");
                });
        }

        
    }
}
