using System;
namespace sampledotnetcoreapi.Models
{
    public class ToDoItem
    {
        #region Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        #endregion
    }
}
