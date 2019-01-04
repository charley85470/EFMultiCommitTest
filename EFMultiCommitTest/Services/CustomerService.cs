using EFMultiCommitTest.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMultiCommitTest.Services
{
    public class CustomerService : IDisposable
    {
        private DbContext baseContext { get; set; }

        public CustomerService()
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
        /// 取得所有資料
        /// </summary>
        /// <returns>table entity object collection</returns>
        public IQueryable<Customer> GetDataAll()
        {
            return this.baseContext.Set<Customer>().AsQueryable();
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
