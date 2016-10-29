using System;
using System.Text;

namespace Zadatak1.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
        }

        public void MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
            }
        }

       public override bool Equals(object obj)
       {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            TodoItem other = (TodoItem) obj;

            return Text == other.Text;
       }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Title:").AppendLine(Text);
            sb.Append("Created:").AppendLine(DateCreated.ToString());
            if (IsCompleted)
            {
                sb.AppendLine("Completed:").AppendLine(DateCompleted.ToString());
            }
            return sb.ToString();
        }

    }
}
