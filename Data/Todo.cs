﻿namespace QFamilyForum.Data
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get;set;}
        public DateTime EndTime { get;set;}

        public bool IsDone { get;set;}
    }
}
