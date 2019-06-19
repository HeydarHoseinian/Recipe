using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Recipe.DataStructures
{
    [Serializable]
    public class RecipeModel : BindableBase
    {
        private Guid id;
        private string name;
        private string description;
        private Guid ownerId;
        private DateTime createDate;
        private Guid documentId;
        private bool isPublic;
        private List<RecipeMaterialModel> materials;
        public List<RecipeTodoItemModel> todoList;
        [DataMember]
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }
        [DataMember]
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        [DataMember]
        public string Description { get { return description; } set { SetProperty(ref description, value); } }
        [DataMember]
        public Guid OwnerId { get { return ownerId; } set { SetProperty(ref ownerId, value); } }
        [DataMember]
        public DateTime CreateDate { get { return createDate; } set { SetProperty(ref createDate, value); } }
        [DataMember]
        public Guid DocumentId { get { return documentId; } set { SetProperty(ref documentId, value); } }
        [DataMember]
        public bool IsPublic { get { return isPublic; } set { SetProperty(ref isPublic, value); } }
        [DataMember]
        public List<RecipeMaterialModel> Materials
        {
            get { return materials; }
            set
            {
                SetProperty(ref materials, value);
            }
        }
        public List<RecipeTodoItemModel> TodoList
        {
            get { return todoList; }
            set
            {
                SetProperty(ref todoList, value);
            }
        }
    }
    [Serializable]
    public class RecipeMaterialModel : BindableBase
    {
        private Guid id;
        private string name;
        private string description;
        private int displayOrderId;
        private Guid documentId;
        private decimal? weight;
        private string unit;
        [DataMember]
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }

        [DataMember]
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
        [DataMember]

        public string Description { get { return description; } set { SetProperty(ref description, value); } }

        [DataMember]
        public int DisplayOrderId { get { return displayOrderId; } set { SetProperty(ref displayOrderId, value); } }
        [DataMember]

        public Guid DocumentId { get { return documentId; } set { SetProperty(ref documentId, value); } }
        [DataMember]
        public decimal? Weight { get { return weight; } set { SetProperty(ref weight, value); } }
        [DataMember]
        public string Unit { get { return unit; } set { SetProperty(ref unit, value); } }

    }
    [Serializable]
    public class RecipeTodoItemModel : BindableBase
    {
        private Guid id;
        private string action;
        private int displayOrderId;
        private Guid documentId;
        [DataMember]
        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }
        [DataMember]
        public string Action { get { return action; } set { SetProperty(ref action, value); } }
        [DataMember]
        public int DisplayOrderId { get { return displayOrderId; } set { SetProperty(ref displayOrderId, value); } }
        [DataMember]
        public Guid DocumentId { get { return documentId; } set { SetProperty(ref documentId, value); } }

    }

}
