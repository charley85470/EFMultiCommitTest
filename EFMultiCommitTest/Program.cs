﻿using EFMultiCommitTest.Entities;
using EFMultiCommitTest.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EFMultiCommitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch processTime = new Stopwatch();
            processTime.Start();
            processTime.Stop();
            processTime.Reset();
            Console.WriteLine(processTime.Elapsed.TotalMilliseconds.ToString());

            var total = 100000;    // 10W筆資料

            #region 每1000筆Save一次(同一個Context)
            //{
            //    Console.WriteLine("每1000筆Save一次(使用同一個Context)");
            //    processTime.Start();
            //    var per = 1000;
            //    var times = total / per;
            //    Console.Write("times ");
            //    using (var srv = new CustomerService())
            //    {
            //        for (int i = 0; i < times; i++)
            //        {
            //            for (int j = 0; j < per; j++)
            //            {
            //                srv.Create(new Customer());
            //            }
            //            srv.SaveChanges();

            //            Console.Write(" " + i);
            //        }
            //    }
            //    Console.WriteLine();
            //    processTime.Stop();
            //    Console.WriteLine("共建立 " + total + " 筆資料，耗時 " + processTime.Elapsed.TotalMilliseconds.ToString() + "ms");
            //    processTime.Reset();
            //}
            #endregion

            #region 每1000筆Save一次並Clear Context
            {
                Console.WriteLine("每1000筆Save一次並ClearContext");
                processTime.Start();
                var per = 1000;
                var times = total / per;
                Console.Write("times ");
                for (int i = 0; i < times; i++)
                {
                    using (var srv = new CustomerService())
                    {
                        for (int j = 0; j < per; j++)
                        {
                            srv.Create(new Customer());
                        }
                        srv.SaveChanges();
                    }
                    Console.Write(" " + i);
                }
                Console.WriteLine();
                processTime.Stop();
                Console.WriteLine("共建立 " + total + " 筆資料，耗時 " + processTime.Elapsed.TotalMilliseconds.ToString() + "ms");
                processTime.Reset();
            }
            #endregion

            #region 每1000筆Save一次並Clear Context(維持同一筆交易)
            {
                Console.WriteLine("每1000筆Save一次並ClearContext(維持同一筆交易)");
                processTime.Start();
                var per = 1000;
                var times = total / per;
                Console.Write("times ");
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 10, 0)))
                {
                    for (int i = 0; i < times; i++)
                    {
                        using (var srv = new CustomerService())
                        {
                            for (int j = 0; j < per; j++)
                            {
                                srv.Create(new Customer());
                            }
                            srv.SaveChanges();
                        }
                        Console.Write(" " + i);
                    }

                    ts.Complete();
                    Console.WriteLine();
                }
                processTime.Stop();
                Console.WriteLine("共建立 " + total + " 筆資料，耗時 " + processTime.Elapsed.TotalMilliseconds.ToString() + "ms");
                processTime.Reset();
            }
            #endregion

            Console.Read();
        }
    }
}
