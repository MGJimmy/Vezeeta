using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class GeneralAppService
    {
        IUnitOfWork TheUnitOfWork;
        public GeneralAppService(IUnitOfWork theUnitOfWork)
        {
            TheUnitOfWork = theUnitOfWork;
        }

        public void BeginTransaction()
        {
            TheUnitOfWork.BeginTransaction();
        }
        public void CommitTransaction()
        {
            TheUnitOfWork.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            TheUnitOfWork.RollbackTransaction();
        }
    }
}
