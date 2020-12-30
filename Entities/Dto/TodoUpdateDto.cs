using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class TodoUpdateDto
    {
        public int todoId { get; set; }
        public int userId { get; set; }
        public string header { get; set; }
        public string content { get; set; }
    }
}
