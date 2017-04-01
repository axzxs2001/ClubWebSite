using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubWebSite.Model.Entity
{
    /// <summary>
    /// 实体对象
    /// </summary>
    public abstract class EntityObject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID
        { get; set; }
    }
}
