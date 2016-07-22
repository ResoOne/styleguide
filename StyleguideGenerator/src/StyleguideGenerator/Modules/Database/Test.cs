//using System;
//using System.Collections.Generic;
//using CoordData.Domain.Constants;

//namespace CoordData.Domain.Orders
//{
//    /// <summary>
//    /// Фабрика статуса заявки на студию
//    /// </summary>
//    public class OrderStudioStatusFactory
//    {
//        private readonly BaseStudioOrder _studioOrder;

//        private static readonly Dictionary<StudioOrderStatusCode, Type> Statuses;

//        static OrderStudioStatusFactory()
//        {
//            Statuses = new Dictionary<StudioOrderStatusCode, Type>()
//            {
//                {StudioOrderStatusCode.Unknown, typeof(UnknownOrderStudioStatus)},
//                {StudioOrderStatusCode.New, typeof (NewOrderStudioStatus)},
//                {StudioOrderStatusCode.Canceled, typeof (CancelOrderStudioStatus)},
//                {StudioOrderStatusCode.Change, typeof (ChangeOrderStudioStatus)},
//            };
//        }
//        /// <summary>
//        /// Инициализация фабрики для объекта заявки на студию
//        /// </summary>
//        /// <param name="studioOrder">Заявка на студию</param>
//        public OrderStudioStatusFactory(BaseStudioOrder studioOrder)
//        {
//            _studioOrder = studioOrder;
//        }

//        /// <summary>
//        /// Статус заявки на студию по перечислению статуса
//        /// </summary>
//        /// <param name="StudioOrderStatusCode">Значение статуса</param>
//        /// <returns>Статус соответствующий перечислению</returns>
//        public OrderStudioStatus GetStudioOrderStatus(StudioOrderStatusCode StudioOrderStatusCode)
//        {
//            return (OrderStudioStatus)Activator.CreateInstance(Statuses[StudioOrderStatusCode], _studioOrder);
//        }

//        /// <summary>
//        /// Статус заявки на студию по коду
//        /// </summary>
//        /// <param name="code">Код статуса заявки на студию</param>
//        /// <returns>Статус по коду перечесления статусов заявки на студию</returns>
//        public OrderStudioStatus GetStudioOrderStatus(int code)
//        {
//            if (!Enum.IsDefined(typeof(StudioOrderStatusCode), code))
//                throw new ArgumentOutOfRangeException(nameof(code));
//            return GetStudioOrderStatus((StudioOrderStatusCode)code);
//        }
//    }
//}
