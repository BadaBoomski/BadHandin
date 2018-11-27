using System;

namespace DAB3_2del1.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepo Persons { get; }
        int Complete();
    }
}