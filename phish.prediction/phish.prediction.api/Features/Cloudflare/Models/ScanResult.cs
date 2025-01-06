using System;
using System.Collections.Generic;

public class ScanResult
{
    public bool Success { get; set; }
    public List<Message> Messages { get; set; }
    public ResultData Result { get; set; }
    public List<Error> Errors { get; set; }

    public class Message
    {
        public string message { get; set; }
    }

    public class ResultData
    {
        public string Visibility { get; set; }
        public string Uuid { get; set; }
        public string Url { get; set; }
        public string EffectiveUrl { get; set; }
        public string Status { get; set; }
        public DateTime? Time { get; set; }
        public string ClientType { get; set; }
        public List<Task> Tasks { get; set; }
    }

    public class Task
    {
        public string Uuid { get; set; }
        public string Url { get; set; }
        public string EffectiveUrl { get; set; }
        public string Status { get; set; }
        public DateTime? Time { get; set; }
        public string Visibility { get; set; }
        public string ClientType { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Name { get; set; }
    }
}