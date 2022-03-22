using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Domain.Entity
{
    public interface IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}
