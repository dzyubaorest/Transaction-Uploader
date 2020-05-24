using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransactionUploader.Common;

namespace TransactionUploader.Infrastructure.Entities
{
	public class Transaction
	{
		[StringLength(Constants.MaxTransactionIdLength)]
		public string Id { get; set; }

		[Column(TypeName = "money")]
		public decimal Amount { get; set; }

		[StringLength(3)]
		[Required]
		public string CurrencyCode { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public DateTime Date { get; set; }

		public TransactionStatus Status { get; set; }
	}
}