using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InmutableCollectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<InvoiceLine> lines = new List<InvoiceLine>
            {
                new InvoiceLine(1, 5),
                new InvoiceLine(1, 15)
            };
            Invoice invoice = new Invoice(lines, StateKind.incomplete);
            Console.WriteLine(invoice.Total);
            var invoiceWithNewItem = invoice.AddLine(new InvoiceLine(1, 20));
            Console.WriteLine($"older total -> {invoice.Total}");
            Console.WriteLine($"new total -> {invoiceWithNewItem.Total}");

			Customer customer = new Customer(500);
            var invoiceCanceled = invoice.Cancel();
            var customerWithInvoiceCanceledProcessed = customer.CancelInvoice(invoiceCanceled);
            Console.WriteLine($"Balance before cancel invoice -> {customer.Balance}");
            Console.WriteLine($"Balance after cancel invoice -> {customerWithInvoiceCanceledProcessed.Balance}");
            Console.ReadKey();
        }
    }

    enum StateKind
    {
        incomplete,
        canceled
    }

    class InvoiceLine
    {
        public InvoiceLine(int quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Total
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }

    class Invoice
    {
        private ImmutableList<InvoiceLine> lines;

        public Invoice(IEnumerable<InvoiceLine> lines, StateKind state)
        {
            this.lines = lines.ToImmutableList();
            State = state;
        }

        public StateKind State { get; private set; }

        public decimal Total 
        { 
            get { return lines.Sum(l => l.Total); } 
        }

        public IEnumerable<InvoiceLine> Lines 
        {
            get { return lines; } 
        }

        private Invoice WithLines(IEnumerable<InvoiceLine> value)
        {
            return Object.ReferenceEquals(lines, value)
                ? this
                : new Invoice(value, State);
        }

        public Invoice AddLine(InvoiceLine newInvoiceLine)
        {
            return WithLines(lines.Add(newInvoiceLine));
        }

        public Invoice RemoveLine(InvoiceLine toDelete)
        {
            return WithLines(lines.Remove(toDelete));
        }

        public Invoice Cancel()
        {
            return (State == StateKind.canceled)
                ? this
                : new Invoice(Lines, StateKind.canceled);
        }
    }

    class Customer
    {
        public Customer(decimal balance)
        {
            this.Balance = balance;
        }

        public decimal Balance { get; private set; }
        public Customer CancelInvoice(Invoice invoice)
        {
            return (invoice.State == StateKind.canceled)
                ? AddToBalance(invoice.Total)
                    : this;
        }

        private Customer AddToBalance(decimal total)
        {
            return new Customer(Balance + total);
        }
    }
}
