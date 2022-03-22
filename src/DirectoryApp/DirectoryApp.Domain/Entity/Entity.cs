using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;

namespace DirectoryApp.Domain.Entity
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }


        public Entity()
        {
            this.Created = DateTime.Now;
            this.IsActive = true;
        }
    }
}
