using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {
        // unit test code  
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }


        //unit test method  
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown."); 
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 20.00;
            double debitAmount = 200.00;
            BankAccount account = new BankAccount("Mr. Patrick Star", beginningBalance);

            //act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void Credit_AccountFrozen_ShouldThrowException() 
        {
            double beginningBalance = 200.00;
            double creditAmount = 20;
            BankAccount account = new BankAccount("Mr. Sylvester Stalone", beginningBalance);
            account.ToggleFreeze();

            //act
            try
            {
                account.Credit(creditAmount);
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount.AccountFrozenMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 200.00;
            double creditAmount = -20.00;
            BankAccount account = new BankAccount("Ms. Patricia Fowler", beginningBalance);

            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown");
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 200.00;
            double creditAmount = 5000.99;
            double expected = 5200.99;
            BankAccount account = new BankAccount("Mr. Walter White", beginningBalance);

            // act  
            account.Credit(creditAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

    }
}
