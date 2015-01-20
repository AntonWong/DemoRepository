using System.Collections.Generic;

namespace MeetingRoomModel.Models
{
    /// <summary>
    /// 房间和设备的关系
    /// </summary>
    public class RoomDevice : CommonBusinessModelBase<int>
    {
        public int RoomId { get; set; }
        public virtual Room  Room { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

    }
}
