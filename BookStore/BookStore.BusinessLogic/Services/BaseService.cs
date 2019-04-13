using System;
using System.Collections.Generic;
using System.Text;
using BookStore.DataAccess.UnitOfWork;

namespace BookStore.BusinessLogic.Services
{
    public class BaseService: IDisposable
    {
        protected readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
