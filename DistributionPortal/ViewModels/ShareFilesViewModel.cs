using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.ViewModels
{
    public class ShareFilesViewModel
    {
        public long Id { get; set; }
        public string Topic { get; set; }
        public string RoleName { get; set; }
        public string Author { get; set; }
        public string Abstract { get; set; }
        public string Link { get; set; }
        public int Downloads { get; set; }
        public int Status { get; set; }
    }
}
