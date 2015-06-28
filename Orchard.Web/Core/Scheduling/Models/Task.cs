using System;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Tasks.Scheduling;

namespace Orchard.Core.Scheduling.Models
{
    /// <summary>
    /// ȥ���������ݹ�����صĴ��룬��Ҫ����Ч��
    /// </summary>
    public class Task : IScheduledTask
    {
        private readonly ScheduledTaskRecord _record;

        public Task(ScheduledTaskRecord record)
        {
            _record = record;
        }

        public string TaskType
        {
            get { return _record.TaskType; }
        }

        public DateTime? ScheduledUtc
        {
            get { return _record.ScheduledUtc; }
        }




      
    }
}