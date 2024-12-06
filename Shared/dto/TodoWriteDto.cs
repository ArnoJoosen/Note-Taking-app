using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.dto {
    public class TodoWriteDto {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Detline { get; set; }
        public bool HasDetline { get; set; }
        public bool IsCompleted { get; set; }
    }
}
