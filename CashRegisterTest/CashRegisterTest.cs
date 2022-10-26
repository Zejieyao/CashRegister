namespace CashRegisterTest
{
	using CashRegister;
    using Moq;
    using Xunit;

	public class CashRegisterTest
	{
/*		[Fact]
		public void Should_process_execute_printing()
		{
			//given
			SpyPrinter printer = new SpyPrinter();
			var cashRegister = new CashRegister(printer);
			var purchase = new Purchase();
			//when
			cashRegister.Process(purchase);
			//then
			//verify that cashRegister.process will trigger print
			Assert.True(printer.HasPrinted);
		}
*/

		[Fact]
		public void Should_call_print_when_execute_cashRegister_process()
		{
			//given
			var spyPrinter = new Mock<Printer>();
			var cashRegister = new CashRegister(spyPrinter.Object);
			var purchase = new Purchase();
			//when
			cashRegister.Process(purchase);
			//then
			//verify that cashRegister.process will trigger print
			spyPrinter.Verify(_ => _.Print(It.IsAny<string>()));
		}

		[Fact]
		public void Should_print_purchase_content_when_run_process_given_stub_purchase()
		{
			//given
			var spyPrinter = new Mock<Printer>();
			var cashRegister = new CashRegister(spyPrinter.Object);
			var stubPurchase = new Mock<Purchase>();

			stubPurchase.Setup(_ => _.AsString()).Returns("I am returned");
			//when
			cashRegister.Process(stubPurchase.Object);
			//then
			//verify that cashRegister.process will trigger print
			spyPrinter.Verify(_ => _.Print("I am returned"));
		}

		[Fact]
		public void Should_throw_HardwareException_when_run_process()
		{
			//given
			var stubPrinter = new Mock<Printer>();

			stubPrinter.Setup(_ => _.Print(It.IsAny<string>())).Throws(new PrinterOutOfPaperException());

			var cashRegister = new CashRegister(stubPrinter.Object);

			//when
			//cashRegister.Process(stubPurchase.Object);
			//then
			//verify that cashRegister.process will trigger print
			Assert.Throws<HardwareException>(() => cashRegister.Process(new Purchase()));
		}
	}
}
