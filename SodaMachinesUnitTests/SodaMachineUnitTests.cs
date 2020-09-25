using NUnit.Framework;
using ConsoleApplication;
using System;

namespace Tests
{
    public class Tests
    {
        private SodaMachine _testMachine;
        private Soda[] _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new[] {
                new Soda { Name = "coke", Nr = 2, Cost = 20 },
                new Soda { Name = "sprite", Nr = 3, Cost = 15 },
                new Soda { Name = "fanta", Nr = 3, Cost = 15 }
            };

            _testMachine = new SodaMachine(_inventory);

        }

        [Test]
        public void Insert_valid_amount_of_money()
        {
            _testMachine.InsertMoney(50);
            Assert.That(_testMachine.GetMoney() == 50);
        }

        [Test]
        public void Insert_invalid_amounts_of_money()
        {
            _testMachine.InsertMoney(-1);
            Assert.That(_testMachine.GetMoney() == 0);
        }

        [Test]
        public void Recall_money_and_check_money()
        {
            _testMachine.InsertMoney(50);
            _testMachine.RecallMoney();
            Assert.That(_testMachine.GetMoney() == 0);
        }

        [Test]
        public void Recall_money_and_check_invalid_money()
        {
            _testMachine.InsertMoney(-1);
            _testMachine.RecallMoney();
            Assert.That(_testMachine.GetMoney() == 0);
        }


        [Test]
        public void Order_soda_from_machine_with_enough_money()
        {
            _testMachine.InsertMoney(50);
            _testMachine.Order("coke");
            Assert.Less(_inventory[0].Nr, 2);
        }

        [Test]
        public void Order_soda_from_machine_withdraw_money()
        {
            _testMachine.InsertMoney(50);
            _testMachine.Order("coke");
            Assert.That(_testMachine.GetMoney() == 0);
        }

        [Test]
        public void Order_capital_letter_soda_from_machine_with_enough_money()
        {
            _testMachine.InsertMoney(50);
            _testMachine.Order("FanTa");
            Assert.Less(_inventory[2].Nr, 3);
        }

        [Test]
        public void Order_soda_from_machine_with_insufficient_money()
        {
            _testMachine.InsertMoney(15);
            _testMachine.Order("coke");
            Assert.That(_testMachine.GetMoney() == 15);
        }

        [Test]
        public void Order_soda_from_machine_with_sms()
        {
            _testMachine.InsertMoney(20);
            var currentMoney = _testMachine.GetMoney();
            _testMachine.Order("coke",true);
            Assert.That(_testMachine.GetMoney() == currentMoney);
        }

        [Test]
        public void Add_soda_to_system()
        {
            _inventory = new[] {
                new Soda { Name = "coke", Nr = 2, Cost = 20 },
                new Soda { Name = "sprite", Nr = 3, Cost = 15 },
                new Soda { Name = "fanta", Nr = 3, Cost = 15 },
                new Soda { Name = "pepsi", Nr = 10, Cost = 20 }
            };

            _testMachine = new SodaMachine(_inventory);

            _testMachine.Order("pepsi", true);

            Assert.Less(_inventory[3].Nr, 10);
        }
    }
}