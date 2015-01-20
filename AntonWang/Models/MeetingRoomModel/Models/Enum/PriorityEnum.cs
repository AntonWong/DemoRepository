using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomModel.Models.Enum
{
    public enum PriorityEnum
    {
        [Description("轻")]
        Light=1,

        [Description("重")]
        Heavy = 2

    }
}
