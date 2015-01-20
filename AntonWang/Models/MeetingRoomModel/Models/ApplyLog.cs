using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomModel.Models
{
    public class ApplyLog : CommonBusinessModelBase<int>
    {
        /// <summary>
        /// 申请人
        /// </summary>
        [MaxLength(50)]
        public string Applicant { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        [MaxLength(50)]
        public string  Master { get; set; }

        /// <summary>
        /// 人员
        /// </summary>
        [MaxLength(200)]
        public string Member { get; set; }

        public int RoomId { get; set; }
        /// <summary>
        /// 会议室
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// 会议主题
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        /// <summary>
        /// 会议说明
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

       
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
    }
}
