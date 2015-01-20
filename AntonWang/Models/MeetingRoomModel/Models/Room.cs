using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomModel.Models
{
    /// <summary>
    /// 会议室实体
    /// </summary>
    public class Room : CommonBusinessModelBase<int>
    {
        /// <summary>
        /// 房间号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 面积大小
        /// </summary>
        public decimal AreaSize { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [MaxLength(100)]
        public string Position { get; set; }

        /// <summary>
        /// 关联申请会议室
        /// </summary>
        public virtual ICollection<ApplyMeetingRoom> ApplyMeetingRooms { get; set; }

        /// <summary>
        /// 关联会议室设备
        /// </summary>
        public virtual ICollection<RoomDevice> RoomDevices { get; set; }

        /// <summary>
        /// 申请记录
        /// </summary>
        public virtual ICollection<ApplyLog> ApplyLogs { get; set; }
    }
}
