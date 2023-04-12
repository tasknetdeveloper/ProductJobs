using Model;
using LoggerSpace;
using MySqlConnector;
using RedisWorkOMSpace;
using Redis.OM;
using System.Linq.Expressions;

namespace DBWork
{
    public class ReposiotoryWork
    {        
        private Log log = new(true);
        private string connection = "";
        private int maxRetryDelay = 1;
        private int limitSelectRowsFromDb = 100;

        private RedisAbstract<Product>? redisProduct = null;
        private RedisAbstract<OrderItem>? redisOrder = null;
        public ReposiotoryWork(RedisConnectionProvider provider, Log log,
            IEmulatorSettings iemusettings)
        {
            this.log = log;
            this.limitSelectRowsFromDb = iemusettings.LimitSelectRowsFromDb;
            this.maxRetryDelay = iemusettings.MaxRetryDelay;
            this.redisProduct = new (log, provider, "Product");
            this.redisOrder = new(log, provider, "Order");
            this.connection = iemusettings.ConnectionDB;
            Ini();
        }

        private void Ini() {
            var r = GetOrderList();
            if(redisOrder != null && r!=null)
                this.redisOrder.LoadData(r);
        }

        #region Get
        public bool HealthCheck()
        {
            var result = true;
           
            Product? data = null;
            try
            {
                using (var r = new ApplicationContext(this.connection, this.maxRetryDelay))
                {
                    if (r != null && r.Product != null)
                        data = r.Product.Where(x => x.Id == 0).FirstOrDefault();
                }                
            }
            catch (Exception)
            {
                result = false;
            }
            return result;           
        }

        public OrderItem[]? GetOrderList(Expression<Func<OrderItem, bool>>? expression = null)
        {
            OrderItem[]? result = null;
            try
            {

                using (var r = new ApplicationContext(this.connection,this.maxRetryDelay))
                {
                    if (r != null && r.Order != null)
                    {
                        result = (expression != null)
                            ? r.Order.Where(expression).ToArray()
                            : r.Order.Take(this.limitSelectRowsFromDb).ToArray();
                    }
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"GetOrderList/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }

        public OrderItem? GetOrder(int id)
        {
            OrderItem? result = null;
            try
            {
                using (var r = new ApplicationContext(this.connection,this.maxRetryDelay))
                {
                    if (r != null && r.Order != null)
                        result = r.Order.Where(x => x.Id == id).FirstOrDefault();
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"GetOrder/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }

        public Product[]? GetProductList(Expression<Func<Product, bool>>? expression = null)
        {
            Product[]? result = null;
            try
            {

                using (var r = new ApplicationContext(this.connection, this.maxRetryDelay))
                {
                    if (r != null && r.Product != null)
                    {                       
                        result = (expression != null)
                             ? r.Product.Where(expression).ToArray()
                             : r.Product.Take(this.limitSelectRowsFromDb).ToArray();
                    }
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"GetProductList/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }

        public Product? GetProduct(int id)
        {
            Product? result = null;
            try
            {
                using (var r = new ApplicationContext(this.connection, this.maxRetryDelay))
                {
                    if (r != null && r.Product != null)
                        result = r.Product.Where(x => x.Id == id).FirstOrDefault();
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"GetProduct/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }
        #endregion

        #region Add/Update       
        public bool AddUpdate_Order(OrderItem model)
        {
            var result = false;
            if (model == null) return result;
            try
            {
                using (var r = new ApplicationContext(this.connection,this.maxRetryDelay))
                {
                    if (r != null && r.Order != null)
                    {
                        var v = r.Order.Where(x => x.Id == model.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v = model;
                            r.SaveChanges();

                            //Update in redis
                            if (redisOrder != null)
                            {
                                redisOrder.Update(model, x=>x.Id==model.Id);
                            }
                        }
                        else
                        {
                            r.Order.Add(model);
                            r.SaveChanges();

                            //Add to redis
                            if (redisOrder != null && !redisOrder.isExist(model, x => x.Id == model.Id))
                            {
                                redisOrder.Add(model);
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"AddUpdate_Order/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }

        public bool AddUpdate_Product(Product model)
        {
            var result = false;
            if (model == null) return result;
            try
            {
                using (var r = new ApplicationContext(this.connection, this.maxRetryDelay))
                {
                    if (r != null && r.Product != null)
                    {
                        var v = r.Product.Where(x => x.Id == model.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v = model;
                            r.SaveChanges();

                            //Update in redis
                            if (redisProduct != null)
                            {
                                redisProduct.Update(model, x => x.Id == model.Id);
                            }
                        }
                        else
                        {
                            r.Product.Add(model);
                            r.SaveChanges();

                            if (redisProduct != null && !redisProduct.isExist(model, x => x.Id == model.Id))
                            {
                                redisProduct.Add(model);
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (MySqlException exp)
            {
                log.Error($"AddUpdate_Product/Message={exp.Message} InnerException={exp.InnerException}");
            }
            return result;
        }
        #endregion       
    }
}
