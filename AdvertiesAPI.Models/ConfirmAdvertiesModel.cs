using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertiesAPI.Models
{
    public class ConfirmAdvertiesModel
    {
        public string ID { get; set; }
        public AdvertiesStatus Status { get; set; }
        public string FilePath { get; set; }
    }
}
