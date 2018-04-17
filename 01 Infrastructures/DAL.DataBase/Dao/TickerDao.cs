﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using Domain.Common.DataBaseModels;

namespace DAL.DataBase.Dao
{
    public class TickerDao
    {
        readonly string conStr = SQLiteHelper.SQLiteHelper.LocalDbConnectionString;

        /// <summary>
        ///  TickerTable
        /// </summary>
        /// <param name="TickerTable">TickerTable实体对象</param>
        public void Insert(TickerTable obj)
        {
            try
            {
                string sql = "insert into Tickers(Pair,AskCount,BidCount, Coin, Currency,Ask,Bid,CreateTime,LastChangeTime) values(@Pair,@AskCount,@BidCount,@Coin, @Currency,@Ask,@Bid,datetime('now', 'localtime'),datetime('now', 'localtime'))";

                using (SQLiteCommand cmd = new SQLiteCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Ask", obj.Ask);
                    cmd.Parameters.AddWithValue("@AskCount", obj.AskCount);
                    cmd.Parameters.AddWithValue("@BidCount", obj.BidCount);
                    cmd.Parameters.AddWithValue("@Bid", obj.Bid);
                    cmd.Parameters.AddWithValue("@Coin", obj.Coin);
                    cmd.Parameters.AddWithValue("@Currency", obj.Currency);
                    cmd.Parameters.AddWithValue("@Pair", obj.Pair);

                    SQLiteHelper.SQLiteHelper.ExecuteNonQuery(conStr, cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("调用TickerTable时，访问Insert时出错", ex);
            }
        }
        /// <summary>
        /// TickerTable
        /// </summary>
        /// <param name="TickerTable">TickerTable</param>
        /// <returns>状态代码</returns>
        public int Update(TickerTable obj)
        {
            try
            {
                string sql = "UPDATE Tickers set Ask=@Ask,AskCount=@AskCount, Bid=@Bid,BidCount=@BidCount, Coin=@Coin,Currency=@Currency,Pair=@Pair,LastChangeTime=datetime('now', 'localtime') where id=@ID";

                using (SQLiteCommand cmd = new SQLiteCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@ID", obj.ID);
                    cmd.Parameters.AddWithValue("@AskCount", obj.AskCount);
                    cmd.Parameters.AddWithValue("@BidCount", obj.BidCount);
                    cmd.Parameters.AddWithValue("@Ask", obj.Ask);
                    cmd.Parameters.AddWithValue("@Bid", obj.Bid);
                    cmd.Parameters.AddWithValue("@Coin", obj.Coin);
                    cmd.Parameters.AddWithValue("@Currency", obj.Currency);
                    cmd.Parameters.AddWithValue("@Pair", obj.Pair);

                    return SQLiteHelper.SQLiteHelper.ExecuteNonQuery(conStr, cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("调用TickerTable时，访问Update时出错", ex);
            }
        }


        /// <summary>
        /// TickerTable
        /// </summary>
        /// <param name="BalanceTable">TickerTable</param>
        /// <returns>状态代码</returns>
        public int Delete(TickerTable obj)
        {
            try
            {
                string sql = "Delect from Tickers where id=@ID";

                using (SQLiteCommand cmd = new SQLiteCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@ID", obj.ID);
                    
                    return SQLiteHelper.SQLiteHelper.ExecuteNonQuery(conStr, cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("调用TickerTable 时，访问Delete时出错", ex);
            }
        }

        /// <summary>
        /// TickerTable
        /// </summary>
        /// <param name="TickerTable">TickerTable</param>
        /// <returns>状态代码</returns>
        public List<TickerTable> Select()
        {
            try
            {
                string sql = "select ID,Pair,AskCount,BidCount, Coin, Currency,Ask,Bid,CreateTime,LastChangeTime from Tickers";

                var results = new List<TickerTable>();
                using (SQLiteCommand cmd = new SQLiteCommand(sql))
                {
                    var reader = SQLiteHelper.SQLiteHelper.ExecuteReader(conStr, cmd);
                    while (reader.Read())
                    {
                        results.Add(new TickerTable()
                        {
                            ID = long.Parse(reader["id"].ToString()),
                            Pair = reader["Pair"].ToString(),
                            Ask = double.Parse(reader["Ask"].ToString()),
                            Bid = double.Parse(reader["Bid"].ToString()),
                            AskCount = double.Parse(reader["AskCount"].ToString()),
                            BidCount = double.Parse(reader["BidCount"].ToString()),
                            Coin = reader["Coin"].ToString(),
                            Currency=reader["Currency"].ToString(),
                            CreateTime = DateTime.Parse(reader["CreateTime"].ToString()),
                            LastChangeTime = DateTime.Parse(reader["LastChangeTime"].ToString()),
                        });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("调用TickerTable 时，访问Select时出错", ex);
            }
        }

        /// <summary>
        /// TickerTable
        /// </summary>
        /// <param name="TickerTable">TickerTable</param>
        /// <returns>状态代码</returns>
        public List<TickerTable> Select(string Coin)
        {
            try
            {
                string sql = "select ID,Pair,AskCount,BidCount, Coin, Currency,Ask,Bid,CreateTime,LastChangeTime from Tickers where coin=@coin";

                List<TickerTable> results = new List<TickerTable>();
                using (SQLiteCommand cmd = new SQLiteCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Coin", Coin);
                    var reader = SQLiteHelper.SQLiteHelper.ExecuteReader(conStr, cmd);
                    while (reader.Read())
                    {
                        results.Add(new TickerTable()
                        {
                            ID = long.Parse(reader["id"].ToString()),
                            Pair = reader["Pair"].ToString(),
                            Ask = double.Parse(reader["Ask"].ToString()),
                            Bid = double.Parse(reader["Bid"].ToString()),
                            AskCount = double.Parse(reader["AskCount"].ToString()),
                            BidCount = double.Parse(reader["BidCount"].ToString()),
                            Coin = reader["Coin"].ToString(),
                            Currency = reader["Currency"].ToString(),
                            CreateTime = DateTime.Parse(reader["CreateTime"].ToString()),
                            LastChangeTime = DateTime.Parse(reader["LastChangeTime"].ToString()),
                        });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("调用TickerTable 时，访问Select时出错", ex);
            }
        }

        public void InsertOrUpdate(List<TickerTable> objs)
        {
            foreach (var obj in objs)
            {
                var list = Select(obj.Coin);
                if (list.Exists(o=>o.Currency==obj.Currency))
                {
                    var ex = list.Find(o => o.Currency == obj.Currency);
                    obj.ID = ex.ID;
                    obj.CreateTime = ex.CreateTime;
                    Update(obj);
                }
                else
                {
                    Insert(obj);
                }
            }
        }
    }
}