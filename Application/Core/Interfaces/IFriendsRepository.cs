using Core.Enums;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IFriendsRepository
    {
        void FriendRequestSent(string fromEmail, int toId);
        void FriendRequestResponded(string fromEmail, int toId, FriendRequestResponse response);
        UsersRelationship GetRelationshipStatus(int firstUserId, int id);
    }
}
