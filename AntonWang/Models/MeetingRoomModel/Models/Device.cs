using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomModel.Models
{
    public class Device : CommonBusinessModelBase<int>
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        /// <summary>
        /// 关联会议室设备
        /// </summary>
        public virtual ICollection<RoomDevice> RoomDevices { get; set; }

    }
}
