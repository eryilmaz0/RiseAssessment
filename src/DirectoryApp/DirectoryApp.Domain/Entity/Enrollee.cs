using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Domain.Entity
{
    public class Enrollee : Entity<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }


        public virtual ICollection<ContactInformation> ContactInformations { get; set; }

        public Enrollee()
        {
            this.ContactInformations = new List<ContactInformation>();
        }
    }
}
