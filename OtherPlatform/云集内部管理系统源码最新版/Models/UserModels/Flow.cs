using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.SysModels;

namespace Models.UserModels
{
    public class Flow : DbSetBase
    {
        //用这个数据下一步生成图 可以看到项目该如何做

        public Flow()
        {
            Disable = false;
        }

        [MaxLength(100)]
        public string FlowName { get; set; } //流程名称

        [DataType("Gantt")]
        [MaxLength]
        public string FlowTask { get; set; } //流程内容


        public bool Disable { get; set; } //禁用

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}