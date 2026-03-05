using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Types;

public class BankTransaction
{
    public decimal Amount { get; set; }
    public DateTime TransactionalDate { get; set; }
}
