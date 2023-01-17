using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace VoteService_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceVote" in both code and config file together.

    // [ServiceContract(CallbackContract = typeof( IServiceVoteCallback))] // Даем севрису знать, что у него есть callback
    [ServiceContract]
    public interface IServiceVote // Описание фукнционала нашего сервиса (сервиса - голосования)
    {
        // У каждого метода в данном интерфейса, которыми потом сможет пользоваться клиент, должен быть аттрибут [OperationContract]
        // Данный аттрибут делает метод видимым для клиента.

        [OperationContract]
        int Connect(); // В дальнейшем, скорее всего, будет password для входа!

        [OperationContract]
        void Disconnect(int client_id);

        // На данный момент - ДЛЯ ТЕСТИРОВАНИЯ!
        [OperationContract(IsOneWay = true)]
        void SendAnswerMessage(string message, int client_id);

        [OperationContract(IsOneWay = true)]
        void SendMessage();

        #region SomeInfo
        /*
         * Пост-скриптум: 
         * 
         * Если метод, который здесь описан, не возвращет никакого значения (void), то наш клиент все равно будет заблокирован
         * до того момента, пока сервер ему не ответит и клиент не получит такой ответ. 
         * Но нам, в некоторых, случаях, такой ответ ждать не требуется - нам нужно лишь просто послать что-то на сервер и на этом все.
         * 
         * В случае вызова метода Connect() мы ОБЯЗАТЕЛЬНО должны дождаться ответа сервера, потому что результатом выполнения данного метода
         * будет получения id, которое будет присвоено пользователю сервером.
         * 
         * Если нам не нужно дожидаться ответа сервера, то в аттрибут добавляется (IsOneWay = true). Данный аттрубут есть у всех методов по
         * умолчанию, но со значением false. 
         */
        #endregion
    }

    //// Данный интерфес будет использован для того, чтобы со стороны сервера разослать всем пользователям что-либо. 
    //public interface IServiceVoteCallback
    //{
    //    // Также добавили параметр, гворящий о том, что не нужно дожидаться ответа, так как сервер 
    //    // должен просто сделать рассылку, а не ждать, пока ему ответить отключившийся пользователь...
    //    [OperationContract(IsOneWay = true)]
    //    void VoteCallBack(string message);  // С помощью данного метода сервер посылает сообщения нашим клиентам.
    //}
}
