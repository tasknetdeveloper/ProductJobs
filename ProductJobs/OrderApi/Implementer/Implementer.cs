using Model;

namespace OrderApi
{
    public class Implementer
    {
        private IImplementer[] implementers = new IImplementer[1];
        private IConfigurationRoot? config;
        public Implementer() {

            var settings = GetSettins();
            implementers[0] = new AcceptingPaymentsOnOrderImplementation(settings);
            
            Array.Resize(ref implementers, 1);
            implementers[1] = new CreateOrderImplementation(settings);
            
            Array.Resize(ref implementers, 1);
            implementers[2] = new PutProductToReserveImplementation(settings);

            //Список можно продолжать, каждый новый класс типа операции обязательно
            //должен реализовывать интерфейс IIplemener
            //...
        }

        private IWebApiSettings? GetSettins()
        {

            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (config != null)
            {
                var settings = config.GetRequiredSection("SystemSettings").Get<SystemSettings>();

                if (settings != null)
                    return settings;
            }
            return null;
        }

        /// <summary>
        /// Выполнение запросов pipline в случае, когда сами pipline образуют последовательную цепочку
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PipelineItemResponse[]? Exec (PipleneItemRequest[]? request)
        {
            PipelineItemResponse[]? result = new PipelineItemResponse[1];
            if (request == null) return result;
            var i = 0;
            //выполнение pipeline происходит в порядке, который определен с помощью OrderNumber
            foreach (var item in request.OrderBy(x=>x.OrderNumber))
            {                
                var r = implementers.Where(x => x.OrderOperationType == item.OrderOperationType);
                if (r != null && r.FirstOrDefault() != null)
                {
                    result[i] = r.First().Exec();
                    Array.Resize(ref result, 1);
                    i++;
                }
            }
            return result;
        }

        /// <summary>
        /// Выполнение запросов pipline в случае, когда сами pipline образуют  непоследовательную цепочку
        /// в request есть поля, NextOperationWhenSuccessResponse,  NextOperationWhenErrorResponse
        /// они обозначают то, каким будет след.шаг, если текущий PipelineItemResponse имеет Status ошибки или успехав
        /// </summary>
        /// <param name="request">pipeline запросов</param>
        /// <returns></returns>
        public PipelineItemResponse[]? ExecWithRules(PipleneItemRequest[]? request)
        {
            PipelineItemResponse[]? result = new PipelineItemResponse[1];
            if (request == null) return result;
            var i = 0;
            var r = request.OrderBy(x => x.OrderNumber);//берем первый request 

            if (r != null && r.FirstOrDefault() != null)
            {
                IEnumerable<IImplementer>? imp = null;               

                while (i< request.Length) {

                    if (i == 0)
                    {
                        //ищем соотве-ее выполнение (по типу, который указан на старте) в implementers
                        imp = implementers.Where(x => x.OrderOperationType == r.First().OrderOperationType);
                    }
                    else {
                        if (result[i].Status == OperationStatus.SuccessResult)
                        {
                            imp = implementers.Where(x => x.OrderOperationType == request[i].NextOperationWhenSuccessResponse);
                        }
                        else if (result[i].Status == OperationStatus.ErrorResult)
                        {
                            imp = implementers.Where(x => x.OrderOperationType == request[i].NextOperationWhenErrorResponse);
                        }
                    }                    

                    //выполнение запроса из pipline
                    if(imp!=null)
                         result[i] = imp.First().Exec();
                    
                    i++;
                    Array.Resize(ref result, 1);
                }
            }
          
            return result;
        }
    }
}
