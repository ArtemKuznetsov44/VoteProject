using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; 

namespace VoteHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Собственно реализация нашего хоста: 

            // Используем конструкцию с using, чтобы реализовывался интерфейс Dispose у нашего объекта типа SercviceHost, когда он нам уже будет не нужен и все ресурсы освобождались. 

            // Создаем наш хост, которому в конструктор нужно передать тип нашего сервиса и параметры, если таковые имеются:
            using (var host = new ServiceHost(typeof(VoteService_WCF.ServiceVote)))
            {
                host.Open();
                
                Console.WriteLine("Host is starting!");

                // И так как это приложение консольное, то реализуем ожидание вводы с клавиатуры: 
                Console.ReadLine(); 
            }

            #region ServiceHost_Info
            /*
             * ServiceHost - это класс, который предоставляет возможность создать хост для сервисов. 
             * Данный хост будут использовать WCF сервисы. 
             * Данный класс следует использовать для настройки и публикации (дальйнешего использования)
             * сервиса клиентскими приложениями, когда мы не используем Internet Information Services (ISS -
             * информационные службы интернета) или Windows Activation Services (WAS - сервисы активации Windows) 
             * для выкатки/выставления/использования сервиса. 
             * И ISS, и WAS взаимодействую с объектом ServiceHost от вашего имени.
             * 
             * Чтобы предоставить службу для использования вызывыющими объектами, WCF требует
             * полного описания службы (представленного классом ServiceDescription). 
             * Класс ServiceHost создает ServiceDescription из информации о типе службы и конфигурации, 
             * а затем использует это описание для создания объектов ChannelDispatcher
             * для каждой конечной точки в описании.
             * 
             * Следует использовать ServiceHost для загрузки службы, настройки конечных точек, 
             * применения параметров безопасности и запуска прослушивателей для обработки входящих запросов.
             */
            #endregion
        }
    }
}
