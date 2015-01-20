using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MeetingRoomModel.Models
{
    /// <summary>
    /// 通用业务模型基类
    /// </summary>
    [Description("通用业务模型基类")]
    public class CommonBusinessModelBase<TKey>
    {
        public CommonBusinessModelBase()
        {
            CreateTime = DateTimeOffset.Now;
            Deleted = false;
        }
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [Display(Name = "主键Id")]
        public TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        public DateTimeOffset? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        public bool Deleted { get; set; }
    }
}
