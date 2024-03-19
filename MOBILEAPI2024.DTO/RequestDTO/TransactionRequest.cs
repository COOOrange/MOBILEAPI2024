using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO
{
    public class Transaction
    {
        public int txnId { get; set; }
        public int dvcId { get; set; }
        public string dvcIP { get; set; }
        public int punchId { get; set; }
        public DateTime txnDateTime { get; set; }
        public string mode { get; set; }
    }

    public class TransactionRequest
    {
        public List<Transaction> trans { get; set; }
    }
}