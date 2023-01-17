using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace VoteService_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceVote" in both code and config file together.

    // Данный аттрибут позволяет задать повеведение нашего сервиса. 
    // В данном случае, мы задаем поведение содержимого нашего сервиса как единый для всех. 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServiceVote : IServiceVote // Имплиментация нашего интерфейса (задание реализации):
    {
        List<ServerClient> clients = new List<ServerClient>();
        // Генерация идентификаторов: 
        private static int nextId = 1;

        public int Connect()
        {
            // Создание экземляра класса клиента:
            ServerClient client = new ServerClient()
            {
                // Задание значений его полям:
                ID = nextId,
                OperationContext = OperationContext.Current // Получение информации о текущем подключении.
            };
            // Увеличиваем значение идентификатора для следующего пользователя:
            nextId++;

            // Отправка сообщения, которое будет видно всем клиентам, которы уже подключены:
            // SendMessage(" | " + client.Name + " | Has been connected!", 0); 

            // Добавление созданного клиента в общий список клиентов:
            clients.Add(client);

            Debug.WriteLine(client.ID);

            // Возвращаем ID присоединившегося пользователя:
            return client.ID;
        }

        // Метод для отключения клиента от сервера:
        public void Disconnect(int client_id)
        {
            // Использование линкью (почитать об этом)!
            // Осуществляется поиск пользователя с интерисующим нас id из списка всех клиентов:
            //var server_client = clients.FirstOrDefault(client => client.ID == client_id);

            //if (server_client != null)
            //{
            //    // Удаление клинта из списка:
            //    clients.Remove(server_client);

            //    // Отправка сообщения на сервер:
            //    //  SendMessage(" | " + server_client.Name + " | Leave from server!", 0); 
            //}

            foreach (var cl in clients)
            {
                if(cl.ID == client_id)
                {
                    Debug.WriteLine($"Cleint with id {cl.ID} was removed");
                    clients.Remove(cl);
                }
            }
            
        }

        // Метод для отправки сообщений на сервер:
        public void SendAnswerMessage(string message, int client_id)
        {
            // Цикл для выборки клиента из общего списка клиентов, который отправил сообщение:
            foreach (var item in clients)
            {
                // Получение текущего времени в формате HH:MM:
                string answer = DateTime.Now.ToShortTimeString();

                // Получение клиента из общего списка с определенным id:
                var server_client = clients.FirstOrDefault(client => client.ID == client_id);

                // Если полученный клиент не равен null:
                if (server_client != null)
                {
                    // К ответу (сообщению), которое на данный момент содержит лишь текущее время, 
                    // добавляем имя клиента, а также определенные отступы: 
                     answer += " | " + server_client.ID + " |=> "; 

                }

                // Дабавление к ответу текста сообщения, которое было переданно данному методу:
                answer += message;

                using (StreamWriter sw = new StreamWriter(@"..\..\WorkingInfo.txt", true))
                {
                    sw.WriteLine(answer);
                }

                // Использование свойства класса client - operationContext:
                // item.OperationContext.GetCallbackChannel<IServiceVoteCallback>().VoteCallBack(answer);

                #region OperationContex_and_GetCallbackChannel_info
                // GetCallBackChannel<T>() - это метод, который возвращает канал
                // экземпляру клиента, вызывавшему текущую опреацию,
                // где Т - это тип канала для обратного вызоыва клиента.
                // Данный метод возврщает ЭКЗЕМПЛЯР КАНАЛА к клиенту, вызывавшего операцию указанному типа. 
                // У полученного экземпляра указанного типа (через интерфейс IServiceVoteCallBack) если метод
                // VoteCallBack(string message), который мы и вызаем с переданным текстом сообщения. 

                // Таким образом, мы берем канал, котрый использует наш пользователь для подключения к сервису
                // и вызваем у него метод VoteCallBack(). Тем самым, мы получается оправляем сообщения на сервер
                // через канал, который испльзует клинт для подключения к серверу.
                #endregion
            }
        }

        public void SendMessage()
        {
            using (StreamWriter sw = new StreamWriter(@"../../WorkinInfo.txt", true))
            {
                sw.WriteLine(new string('=', 30));
            }
        }
        #region SomeInfo
        /* 
         * По умолчанию, когда клиент обращается к северу, то для каждого клиента (для каждого подключения) 
         * создается свой экземляр сервиса. 
         * Нам же это не нужно, так как наш сервис должен знать о всех своих клиентах и уметь посылать что-либо всем клиентам
         * сразу, которы подключились к нашему сервису. 
         * Для того, чтобы это сделать есть два способа: 
         * 1. (не хороший) создать список наших клиентов, которые подключаются к нашему сервусу в статическом списке (так как
         * поле со списоком будет STATIC то оно будет общее для всех сервисов).
         * 2. Использовать специальный сервис, который предоставляет WC - аттрубут [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]. 
         * 
         */
        #endregion
    }
}
