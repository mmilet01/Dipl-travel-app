using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class UsersRelationship
    {
        public int RelationShip { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public int RelationshipStatus { get; set; }

        public virtual Users FirstUser { get; set; }
        public virtual UserRelationshipStatus RelationshipStatusNavigation { get; set; }
        public virtual Users SecondUser { get; set; }
    }
}
