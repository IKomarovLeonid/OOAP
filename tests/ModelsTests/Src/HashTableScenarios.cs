using System.Security.Cryptography;
using FluentAssertions;
using HashTable;
using HashTable.Enums;
using Xbehave;

namespace ModelsTests
{
    public class HashTableScenarios
    {
        [Scenario]
        public void Can_CalculateSlot()
        {
            HashTableModel<int> model = null;

            "Given initialized hashtable model"
                .x(() =>
                {
                    model = new HashTableModel<int>();
                    model.Initialize(19);
                });
            "When user invokes calculate hash function for item 15"
                .x(() =>
                {
                    model.HashFunc(15);
                });
            "And tries to place such item"
                .x(() =>
                {
                    model.Put(15);
                });
            "Then hash model should add new unique item 15"
                .x(() =>
                {
                    var code = model.Find(15);

                    code.Should().Be(OperationCode.Exists);
                });
        }

        [Scenario]
        public void User_CanNotAddAlreadyExistsItem()
        {
            HashTableModel<string> model = null;

            "Given initialized hashtable model with element equal 'Next'"
                .x(() =>
                {
                    model = new HashTableModel<string>();
                    model.Initialize(19);
                    model.HashFunc("Next");
                    model.Put("Next");
                });
            "When user tries to add same item 'Next' again to new slot"
                .x(() =>
                {
                    model.HashFunc("Next");
                    model.Put("Next");
                });
            "Then last put status should be error"
                .x(() =>
                {
                    var code = model.LastInsertStatus();

                    code.Should().Be(OperationCode.Error);
                });
        }

        [Scenario]
        public void Size_NotChangeWhenNewItemIsSameAsExists()
        {
            HashTableModel<string> model = null;

            "Given initialized hashtable model with size equal to 1"
                .x(() =>
                {
                    model = new HashTableModel<string>();
                    model.Initialize(19);
                    model.HashFunc("Next");
                    model.Put("Next");
                });
            "When user tries to add same item to hashtable"
                .x(() =>
                {
                    model.HashFunc("Next");
                    model.Put("Next");
                });
            "Then size should not be changed"
                .x(() =>
                {
                    var size = model.Size();

                    size.Should().Be(1);
                });
        }

    }
}
