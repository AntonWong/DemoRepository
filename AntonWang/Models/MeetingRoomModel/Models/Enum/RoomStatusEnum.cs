using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomModel.Models
{
    public enum RoomStatusEnum
    {
        //[Description("未使用")]
        //NotUsed = 0,

        [Description("已申请")]
        Applied = 1,

        [Description("使用中")]
        Useing = 2,

        [Description("使用完毕")]
        End = 3,//结束 
    }
}
