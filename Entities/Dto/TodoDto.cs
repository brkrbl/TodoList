using System;


namespace Entities.Dto
{
    public class TodoDto
    {
        public int TodoId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public Usertable User { get; set; }
        public DateTime Date { get; set; }
        public TodoDto( int u_todokey, string u_header, string u_content, Usertable u_user, DateTime u_time)
        {
            TodoId = u_todokey;
            Header = u_header;
            Content = u_content;
            User = u_user;
            Date = u_time;
        }
    }
}
