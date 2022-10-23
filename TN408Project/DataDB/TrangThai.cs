using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            DonDats = new HashSet<DonDat>();
        }

        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }

        public virtual ICollection<DonDat> DonDats { get; set; }
    }
}
