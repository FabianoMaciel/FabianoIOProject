using Microsoft.EntityFrameworkCore;
using FabianoIO.Core.Data;
using FabianoIO.ManagementPayments.Business;

namespace FabianoIO.ManagementPayments.Data.Repository;

public class PaymentRepository(PaymentsContext context) : IPaymentRepository
{
    private readonly DbSet<Payment> _dbSet = context.Set<Payment>();
    public IUnitOfWork UnitOfWork => context;
    public void Add(Payment pagamento)
    {
        _dbSet.Add(pagamento);
    }

    public void AddTransaction(BusinessTransaction transaction)
    {
        context.Set<BusinessTransaction>().Add(transaction);
    }

    public void Dispose()
    {
       context.Dispose();
    }
}