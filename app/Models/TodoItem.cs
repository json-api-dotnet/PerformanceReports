using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace app.Models
{
    public sealed class TodoItem : Identifiable
    {
        [Attr]
        public string Description { get; set; }

        [Attr]
        public TodoItemPriority Priority { get; set; }

        [Attr(Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort | AttrCapabilities.AllowView)]
        public DateTimeOffset CreatedAt { get; set; }

        [Attr(PublicName = "modifiedAt", Capabilities = AttrCapabilities.AllowFilter | AttrCapabilities.AllowSort | AttrCapabilities.AllowView)]
        public DateTimeOffset? LastModifiedAt { get; set; }

        [HasOne]
        public Person Owner { get; set; }

        [HasOne]
        public Person Assignee { get; set; }

        [NotMapped]
        [HasManyThrough(nameof(TodoItemTags))]
        public ISet<Tag> Tags { get; set; }

        public ISet<TodoItemTag> TodoItemTags { get; set; }
    }
}
