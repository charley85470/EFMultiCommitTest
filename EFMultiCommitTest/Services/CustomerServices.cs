using EFMultiCommitTest.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMultiCommitTest.Services
{
    public class CustomerServices : IDisposable
    {
        private DbContext baseContext { get; set; }

        public CustomerServices()
        {
            baseContext = new ProjDBEntities();
        }

        /// <summary>
        /// 建立資料
        /// </summary>
        /// <param name="instance">table entity object</param>
        public void Create(Customer instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            this.baseContext.Set<Customer>().Add(instance);
        }

        /// <summary>
        /// DbContext's SaveChanges
        /// </summary>
        public void SaveChanges()
        {
            this.baseContext.SaveChanges();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overwrite Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.baseContext != null)
                {
                    this.baseContext.Dispose();
                    this.baseContext = null;
                }
            }
        }
    }
}
